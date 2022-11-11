namespace ASLET.Models;

public class ClassModel
{
    public byte Grade { get; }
    public char? Letter { get; }

    public ClassModel(byte grade = 0, char letter = ' ')
    {
        Grade = grade;
        Letter = letter;
    }
}