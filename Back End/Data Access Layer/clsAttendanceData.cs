
using Back_End.Models;
using Data_Access_Layer;
using Npgsql;

namespace Back_End.Data_Access_Layer
{
    public class clsAttendanceData
    {
        public static int AddAttendance(clsAttendance Attendance)
        {
            int NewAttendanceID = 0;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "SELECT add_attendance(@EmployeeID, @CheckIn, @CheckOut, @Date , @CreatedBy, @Status)",
                    Connection);

                Command.Parameters.AddWithValue("@EmployeeID", Attendance.EmployeeID);
                Command.Parameters.AddWithValue("@Date", Attendance.AttendanceDate);
                Command.Parameters.AddWithValue("@CheckIn", (object?)Attendance.CheckIn ?? DBNull.Value);
                Command.Parameters.AddWithValue("@CheckOut", (object?)Attendance.CheckOut ?? DBNull.Value);
                Command.Parameters.AddWithValue("@CreatedBy", Attendance.CreatedByUserID);
                Command.Parameters.AddWithValue("@Status", (object?)Attendance.Status ?? DBNull.Value);

                try
                {
                    Connection.Open();
                    object? Result = Command.ExecuteScalar();

                    if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                        NewAttendanceID = InsertedID;
                }
                catch
                {
                }
            }

            return NewAttendanceID;
        }


        public static bool UpdateAttendance(clsAttendance Attendance)
        {
            bool IsUpdated = false;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "CALL update_attendance(@AttendanceID, @EmployeeID, @CheckIn, @CheckOut, @Date, @CreatedBy, @Status, @Updated)",
                    Connection);

                Command.Parameters.AddWithValue("@AttendanceID", Attendance.AttendanceID);
                Command.Parameters.AddWithValue("@EmployeeID", Attendance.EmployeeID);
                Command.Parameters.AddWithValue("@Date", Attendance.AttendanceDate);
                Command.Parameters.AddWithValue("@CheckIn", (object?)Attendance.CheckIn ?? DBNull.Value);
                Command.Parameters.AddWithValue("@CheckOut", (object?)Attendance.CheckOut ?? DBNull.Value);
                Command.Parameters.AddWithValue("@CreatedBy", Attendance.CreatedByUserID);
                Command.Parameters.AddWithValue("@Status", (object?)Attendance.Status ?? DBNull.Value);

                NpgsqlParameter OutParameter = new NpgsqlParameter("@Updated", NpgsqlTypes.NpgsqlDbType.Boolean)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                Command.Parameters.Add(OutParameter);

                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();

                    IsUpdated = OutParameter.Value is bool Updated && Updated;
                }
                catch
                {
                }
            }

            return IsUpdated;
        }


        public static bool DeleteAttendance(int AttendanceID)
        {
            bool IsDeleted = false;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "CALL delete_attendance(@AttendanceID, @Deleted)",
                    Connection);

                Command.Parameters.AddWithValue("@AttendanceID", AttendanceID);

                NpgsqlParameter OutParameter = new NpgsqlParameter("@Deleted", NpgsqlTypes.NpgsqlDbType.Boolean)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                Command.Parameters.Add(OutParameter);

                try
                {
                    Connection.Open();
                    Command.ExecuteNonQuery();

                    IsDeleted = OutParameter.Value is bool Deleted && Deleted;
                }
                catch
                {
                }
            }

            return IsDeleted;
        }


        public static List<clsAttendance> GetAllAttendances()
        {
            List<clsAttendance> Attendances = new List<clsAttendance>();

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "SELECT * FROM get_all_attendances()",
                    Connection);

                try
                {
                    Connection.Open();
                    NpgsqlDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        clsAttendance Attendance = new clsAttendance
                        {
                            AttendanceID = (int)Reader["AttendanceID"],
                            EmployeeID = (int)Reader["EmployeeID"],
                            AttendanceDate = (DateTime)Reader["AttendanceDate"],
                            CheckIn = Reader["CheckInTime"] == DBNull.Value ? null : (TimeSpan?)Reader["CheckInTime"],
                            CheckOut = Reader["CheckOutTime"] == DBNull.Value ? null : (TimeSpan?)Reader["CheckOutTime"],
                            CreatedByUserID = (int)Reader["CreatedByUserID"],
                            Status = Reader["Status"] == DBNull.Value ? null : (string)Reader["Status"]
                        };

                        Attendances.Add(Attendance);
                    }
                }
                catch
                {
                }
            }

            return Attendances;
        }
    }
}
