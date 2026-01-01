
using Back_End.Models;
using Data_Access_Layer;
using Npgsql;

namespace Back_End.Data_Access_Layer
{
    public class clsAttendanceData
    {
        public static bool IsEmployeePresentToday(int employeeID)
        {
            bool isPresent = false;

            string query = @"
                            SELECT 1 
                            FROM Attendances
                            WHERE EmployeeID = @EmployeeID
                              AND AttendanceDate = @Today
                            LIMIT 1;
            ";

            NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);
            NpgsqlCommand Command = new NpgsqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@EmployeeID", employeeID);
            Command.Parameters.AddWithValue("@Today", DateOnly.FromDateTime(DateTime.Today));
            Command.Parameters["@Today"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

            try
            {
                Connection.Open();
                NpgsqlDataReader reader = Command.ExecuteReader();
                isPresent = reader.HasRows;
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                Connection.Close();
            }

            return isPresent;
        }

        public static int GetTodayLateCount(TimeOnly lateTime)
        {
            int LateCount = 0;

            string query = @"
                            SELECT COUNT(*) 
                            FROM Attendances
                            WHERE AttendanceDate = @Today
                            AND checkintime > @LateTime
    ";

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand Command = new NpgsqlCommand(query, Connection))
            {
                Command.Parameters.AddWithValue("@Today", DateOnly.FromDateTime(DateTime.Today));
                Command.Parameters["@Today"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

                Command.Parameters.AddWithValue("@LateTime", lateTime);
                Command.Parameters["@LateTime"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Time;

                try
                {
                    Connection.Open();
                    object? result = Command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                        LateCount = count;
                }
                catch
                {

                }
            }

            return LateCount;
        }
        public static int GetTodayPresentCount()
        {
            int PresentCount = 0;

            string query = @"
                                SELECT COUNT(*) 
                                FROM Attendances
                                WHERE AttendanceDate = @Today
                              AND CheckInTime IS NOT NULL
    ";

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand Command = new NpgsqlCommand(query, Connection))
            {
                Command.Parameters.AddWithValue("@Today", DateOnly.FromDateTime(DateTime.Today));
                Command.Parameters["@Today"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

                try
                {
                    Connection.Open();
                    object? result = Command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int count))
                        PresentCount = count;
                }
                catch
                {

                }
            }

            return PresentCount;
        }

        public static clsAttendance? GetAttendanceByID(int attendanceID)
        {
            const string query =
                @"SELECT AttendanceID, EmployeeID, AttendanceDate,
                 CheckIn, CheckOut, CreatedByUserID
          FROM Attendances
          WHERE AttendanceID = @AttendanceID";

            using (NpgsqlConnection connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AttendanceID", attendanceID);

                try
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new clsAttendance
                        {
                            AttendanceID = (int)reader["AttendanceID"],
                            EmployeeID = (int)reader["EmployeeID"],
                            AttendanceDate = (DateTime)reader["AttendanceDate"],
                            CheckIn = reader["CheckIn"] == DBNull.Value
                                ? null
                                : (TimeOnly?)reader["CheckIn"],
                            CheckOut = reader["CheckOut"] == DBNull.Value
                                ? null
                                : (TimeOnly?)reader["CheckOut"],
                            CreatedByUserID = (int)reader["CreatedByUserID"],

                        };
                    }
                }
                catch
                {
                    return null;
                }
            }
        }


        public static int AddAttendance(clsAttendance Attendance)
        {
            int NewAttendanceID = 0;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "SELECT add_attendance(@EmployeeID, @CheckIn, @CheckOut, @Date , @CreatedBy)",
                    Connection);

                Command.Parameters.AddWithValue("@EmployeeID", Attendance.EmployeeID);

                Command.Parameters.AddWithValue("@Date", Attendance.AttendanceDate);
                Command.Parameters["@Date"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

                Command.Parameters.AddWithValue("@CheckIn", (object?)Attendance.CheckIn ?? DBNull.Value);
                Command.Parameters["@CheckIn"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Time;

                Command.Parameters.AddWithValue("@CheckOut", (object?)Attendance.CheckOut ?? DBNull.Value);
                Command.Parameters["@CheckOut"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Time;

                Command.Parameters.AddWithValue("@CreatedBy", Attendance.CreatedByUserID);


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
            int RowsAffected = 0;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    @"UPDATE attendances
                      SET checkouttime = @CheckOut
                      WHERE attendanceid = @AttendanceID", Connection);

                Command.Parameters.AddWithValue("@AttendanceID", Attendance.AttendanceID);

                Command.Parameters.AddWithValue("@CheckOut", (object?)Attendance.CheckOut ?? DBNull.Value);
                Command.Parameters["@CheckOut"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Time;



                try
                {
                    Connection.Open();


                    RowsAffected = Command.ExecuteNonQuery();
                }
                catch
                {
                }
            }

            return RowsAffected > 0;
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
                            AttendanceDate = ((DateOnly)Reader["AttendanceDate"]).ToDateTime(TimeOnly.MinValue),
                            CheckIn = Reader["CheckInTime"] == DBNull.Value ? null : (TimeOnly?)Reader["CheckInTime"],
                            CheckOut = Reader["CheckOutTime"] == DBNull.Value ? null : (TimeOnly?)Reader["CheckOutTime"],
                            CreatedByUserID = (int)Reader["CreatedByUserID"],

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
