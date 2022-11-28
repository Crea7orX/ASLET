using System.Security.Cryptography;
using System.Text;
using ASLET.Models;
using ASLET.Server.Context;
using ASLET.Server.DTOs;
using ASLET.Server.Services.Token;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASLET.Server.Controllers;

public class AuthController : ApiController
{
    private readonly DatabaseContext _context;
    private readonly ITokenService _tokenService;
    
    public AuthController(DatabaseContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;

    }
    
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginDto loginUser)
    {
        var databaseUser = await _context.AsletUsers.SingleOrDefaultAsync( user => user.Email.ToLower().Equals(loginUser.Email.ToLower()));

        if (databaseUser == null) return Unauthorized("Invalid email");

        using (var hmac = new HMACSHA256(databaseUser.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.Unicode.GetBytes(loginUser.Password));
            if (!computedHash.SequenceEqual(databaseUser.PasswordHash)) return Unauthorized("Invalid password");
        }

        return new LoginResponseDto
        {
            Firstname = databaseUser.Firstname,
            Lastname = databaseUser.Lastname,
            School = databaseUser.School,
            TokenExpires = DateTime.Now.AddDays(365),
            Token = _tokenService.CreateToken(databaseUser)
        };
    }

    #region Testing Authorization

    [Authorize]
    [HttpGet("me")]
    public string Me()
    {
        return "me hehehe";
    }

    #endregion
    
    
}