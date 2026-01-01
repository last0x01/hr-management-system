using Data_Access_Layer;
using Models;
using Npgsql;

namespace Back_End.Data_Access_Layer
{
    public class clsAbsenceData
    {
        public static bool IsEmployeeAbsentToday(int employeeID)
        {
            bool isAbsent = false;

            string query = @"
                                SELECT 1
                                FROM Absences
                                WHERE EmployeeID = @EmployeeID
                                  AND absence_date = @Today
                                LIMIT 1;
    ";
            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", employeeID);
                command.Parameters.AddWithValue("@Today", DateOnly.FromDateTime(DateTime.Today));
                command.Parameters["@Today"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        isAbsent = reader.HasRows;
                    }
                }
                catch
                {

                }
            }

            return isAbsent;
        }

        public static int GetTodayAbsenceCount()
        {
            int AbsenceCount = 0;
            string query = @"
                            SELECT COUNT(*) 
                            FROM Absences
                            WHERE absence_date = @Today
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
                        AbsenceCount = count;
                }
                catch
                {

                }
            }

            return AbsenceCount;
        }

        public static clsAbsence? GetAbsenceByID(int absenceID)
        {
            const string query =
                @"SELECT absenceid, employeeid, absence_date, absencetypeid, reason, createdbyuserid
                  FROM absences
                  WHERE absenceid = @AbsenceID";

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AbsenceID", absenceID);

                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        return new clsAbsence
                        {
                            AbsenceID = (int)reader["absenceid"],
                            EmployeeID = (int)reader["employeeid"],
                            AbsenceDate = (DateTime)reader["absence_date"],
                            AbsenceTypeID = (int)reader["absencetypeid"],
                            Reason = reader["reason"] == DBNull.Value ? null : (string?)reader["reason"],
                            CreatedByUserID = (int)reader["createdbyuserid"]
                        };
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static int AddAbsence(clsAbsence absence)
        {
            int newAbsenceID = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                @"INSERT INTO absences (employeeid, absence_date, absencetypeid, reason, createdbyuserid)
                  VALUES (@EmployeeID, @Date, @AbsenceTypeID, @Reason, @CreatedBy)
                  RETURNING absenceid;", connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", absence.EmployeeID);
                command.Parameters.AddWithValue("@Date", absence.AbsenceDate);
                command.Parameters["@Date"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                command.Parameters.AddWithValue("@AbsenceTypeID", absence.AbsenceTypeID);
                command.Parameters.AddWithValue("@Reason", (object?)absence.Reason ?? DBNull.Value);
                command.Parameters.AddWithValue("@CreatedBy", absence.CreatedByUserID);

                try
                {
                    connection.Open();
                    object? result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        newAbsenceID = insertedID;
                }
                catch
                {
                }
            }

            return newAbsenceID;
        }

        public static bool UpdateAbsence(clsAbsence absence)
        {
            int rowsAffected = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                @"UPDATE absences
                  SET absence_date = @Date,
                      absencetypeid = @AbsenceTypeID,
                      reason = @Reason
                  WHERE absenceid = @AbsenceID", connection))
            {
                command.Parameters.AddWithValue("@AbsenceID", absence.AbsenceID);
                command.Parameters.AddWithValue("@Date", absence.AbsenceDate);
                command.Parameters["@Date"].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                command.Parameters.AddWithValue("@AbsenceTypeID", absence.AbsenceTypeID);
                command.Parameters.AddWithValue("@Reason", (object?)absence.Reason ?? DBNull.Value);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                }
            }

            return rowsAffected > 0;
        }

        public static bool DeleteAbsence(int absenceID)
        {
            int rowsAffected = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                @"DELETE FROM absences WHERE absenceid = @AbsenceID", connection))
            {
                command.Parameters.AddWithValue("@AbsenceID", absenceID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch
                {
                }
            }

            return rowsAffected > 0;
        }

        public static List<clsAbsence> GetAllAbsences()
        {
            List<clsAbsence> absences = new List<clsAbsence>();

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM absences ORDER BY absence_date DESC", connection))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            absences.Add(new clsAbsence
                            {
                                AbsenceID = (int)reader["absenceid"],
                                EmployeeID = (int)reader["employeeid"],
                                AbsenceDate = ((DateOnly)reader["absence_date"]).ToDateTime(TimeOnly.MinValue),
                                AbsenceTypeID = (int)reader["absencetypeid"],
                                Reason = reader["reason"] == DBNull.Value ? null : (string?)reader["reason"],
                                CreatedByUserID = (int)reader["createdbyuserid"]
                            });
                        }
                    }
                }
                catch
                {
                }
            }

            return absences;
        }
    }
}
