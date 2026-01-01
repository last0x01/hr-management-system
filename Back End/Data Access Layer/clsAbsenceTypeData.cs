using Back_End.Models;
using Data_Access_Layer;
using Npgsql;

namespace Back_End.Data_Access_Layer
{
    public class clsAbsenceTypeData
    {
        public static clsAbsenceType? GetAbsenceTypeByID(int typeID)
        {
            clsAbsenceType? result = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT typeid, name FROM absence_types WHERE typeid = @ID", connection))
            {
                command.Parameters.AddWithValue("@ID", typeID);

                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new clsAbsenceType
                            {
                                AbsenceTypeID = (int)reader["typeid"],
                                Name = (string)reader["name"]
                            };
                        }
                    }
                }
                catch
                {

                }
            }

            return result;
        }

        public static List<clsAbsenceType> GetAllAbsenceTypes()
        {
            List<clsAbsenceType> types = new List<clsAbsenceType>();

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                "SELECT typeid, name FROM absence_types ORDER BY name", connection))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsAbsenceType type = new clsAbsenceType
                            {
                                AbsenceTypeID = (int)reader["typeid"],
                                Name = (string)reader["name"]
                            };
                            types.Add(type);
                        }
                    }
                }
                catch
                {

                }
            }

            return types;
        }

        public static int AddAbsenceType(clsAbsenceType type)
        {
            int newID = -1;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                "INSERT INTO absence_types(name) VALUES(@Name) RETURNING typeid", connection))
            {
                command.Parameters.AddWithValue("@Name", type.Name);

                try
                {
                    connection.Open();
                    object? result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newID = id;
                    }
                }
                catch
                {

                }
            }

            return newID;
        }

        public static bool UpdateAbsenceType(clsAbsenceType type)
        {
            int rowsAffected = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                "UPDATE absence_types SET name = @Name WHERE typeid = @ID", connection))
            {
                command.Parameters.AddWithValue("@Name", type.Name);
                command.Parameters.AddWithValue("@ID", type.AbsenceTypeID);

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

        public static bool DeleteAbsenceType(int typeID)
        {
            int rowsAffected = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(
                "DELETE FROM absence_types WHERE typeid = @ID", connection))
            {
                command.Parameters.AddWithValue("@ID", typeID);

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
    }
}
