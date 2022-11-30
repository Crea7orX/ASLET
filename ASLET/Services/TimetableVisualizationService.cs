using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using ASLET.Models;
using Avalonia;

namespace ASLET.Services;

public class TimetableVisualizationService
{
    private const int ROOM_COLUMN_NUMBER = Constants.DAYS_NUM + 1;
    private const int ROOM_ROW_NUMBER = Constants.DAY_HOURS + 1;

    private static Dictionary<Point, TimetableSlotModel[]> GenerateTimeTable(Schedule solution,
        Dictionary<Point, int[]> slotTable)
    {
        var classes = solution.Classes;

        var timeTable = new Dictionary<Point, TimetableSlotModel[]>();
        foreach (var cc in classes.Keys)
        {
            var reservation = Reservation.GetReservation(classes[cc]);
            int dayId = reservation.Day + 1;
            int periodId = reservation.Time + 1;
            int roomId = reservation.Room;

            var key = new Point(periodId, roomId);
            var roomDuration = slotTable.ContainsKey(key) ? slotTable[key] : null;
            if (roomDuration == null)
            {
                roomDuration = new int[ROOM_COLUMN_NUMBER];
                slotTable[key] = roomDuration;
            }

            roomDuration[dayId] = cc.Duration;
            for (int m = 1; m < cc.Duration; ++m)
            {
                var nextRow = new Point(periodId + m, roomId);
                if (!slotTable.ContainsKey(nextRow))
                    slotTable.Add(nextRow, new int[ROOM_COLUMN_NUMBER]);
                if (slotTable[nextRow][dayId] < 1)
                    slotTable[nextRow][dayId] = -1;
            }

            var roomSchedule = timeTable.ContainsKey(key) ? timeTable[key] : null;
            if (roomSchedule == null)
            {
                roomSchedule = new TimetableSlotModel[ROOM_COLUMN_NUMBER];
                timeTable[key] = roomSchedule;
            }

            roomSchedule[dayId] = new TimetableSlotModel(
                string.Join(" / ", cc.Groups.Select(grp => grp.Name).ToArray()),
                cc.TeacherModel, cc);
        }

        return timeTable;
    }

    public static List<TimetableSlotModel>? GetResult(Schedule solution)
    {
        List<TimetableSlotModel> returnValue =
            new List<TimetableSlotModel>();

        StringBuilder sb = new StringBuilder();
        int numberOfRooms = solution.ConfigurationService.NumberOfRooms;

        var slotTable = new Dictionary<Point, int[]>();
        var timeTable = GenerateTimeTable(solution, slotTable); // Point.X = time, Point.Y = roomId
        if (slotTable.Count == 0 || timeTable.Count == 0)
            return null;

        for (int roomId = 0; roomId < numberOfRooms; ++roomId)
        {
            var room = solution.ConfigurationService.GetRoomById(roomId);
            for (int periodId = 0; periodId < ROOM_ROW_NUMBER; ++periodId)
            {
                Point key = new Point(periodId, roomId);
                var roomDuration = slotTable.ContainsKey(key) ? slotTable[key] : null;
                var roomSchedule = timeTable.ContainsKey(key) ? timeTable[key] : null;
                for (int i = 0; i < ROOM_COLUMN_NUMBER; ++i)
                {
                    if (roomSchedule == null && roomDuration == null)
                        continue;

                    TimetableSlotModel? content = roomSchedule?[i];
                    if (content != null)
                    {
                        for (int j = 0; j < content.Subject.Duration; j++)
                        {
                            content = new TimetableSlotModel(content.Class, content.Teacher, content.Subject)
                            {
                                Room = room,
                                Day = i,
                                Hour = periodId + j
                            };
                            returnValue.Add(content);
                        }
                    }
                }
            }
        }

        return returnValue;
    }
}