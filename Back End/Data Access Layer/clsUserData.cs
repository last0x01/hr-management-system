using Back_End.Models;
using Npgsql;

namespace Data_Access_Layer
{
    public class clsUserData
    {
        public static int AddNewUser(clsUser user)
        {
            int newUserID = 0;

            using NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);
            using NpgsqlCommand Command = new NpgsqlCommand(
                "SELECT add_user(@FirstName, @LastName, @Age, @Phone, @Email, @Gender, @Address, @Username, @Password)",
                Connection);

            Command.Parameters.AddWithValue("@FirstName", user.Person.FirstName);
            Command.Parameters.AddWithValue("@LastName", user.Person.LastName);
            Command.Parameters.AddWithValue("@Age", user.Person.Age);
            Command.Parameters.AddWithValue("@Phone", (object?)user.Person.Phone ?? DBNull.Value);
            Command.Parameters.AddWithValue("@Email", (object?)user.Person.Email ?? DBNull.Value);
            Command.Parameters.AddWithValue("@Gender", user.Person.Gender);
            Command.Parameters.AddWithValue("@Address", (object?)user.Person.Address ?? DBNull.Value);

            Command.Parameters.AddWithValue("@Username", user.Username);
            Command.Parameters.AddWithValue("@Password", user.Password);

            try
            {
                Connection.Open();
                object? result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    newUserID = insertedID;
            }
            catch
            {
            }

            return newUserID;
        }

        public static bool UpdateUser(clsUser user)
        {
            bool IsUpdated = false;

            using NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);
            using NpgsqlCommand Command = new NpgsqlCommand(
                "CALL update_user(@UserID, @FirstName, @LastName, @Age, @Phone, @Email, @Gender, @Address, @Username, @Password, @Updated)",
                Connection);

            Command.Parameters.AddWithValue("@UserID", user.UserID);
            Command.Parameters.AddWithValue("@FirstName", user.Person.FirstName);
            Command.Parameters.AddWithValue("@LastName", user.Person.LastName);
            Command.Parameters.AddWithValue("@Age", user.Person.Age);
            Command.Parameters.AddWithValue("@Phone", (object?)user.Person.Phone ?? DBNull.Value);
            Command.Parameters.AddWithValue("@Email", (object?)user.Person.Email ?? DBNull.Value);
            Command.Parameters.AddWithValue("@Gender", user.Person.Gender);
            Command.Parameters.AddWithValue("@Address", (object?)user.Person.Address ?? DBNull.Value);
            Command.Parameters.AddWithValue("@Username", user.Username);
            Command.Parameters.AddWithValue("@Password", user.Password);

            NpgsqlParameter outParam = new NpgsqlParameter("@Updated", NpgsqlTypes.NpgsqlDbType.Boolean)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            Command.Parameters.Add(outParam);

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();

                IsUpdated = outParam.Value is bool updated && updated;
            }
            catch
            {
            }

            return IsUpdated;
        }

        public static bool DeleteUser(int userID)
        {
            bool isDeleted = false;

            using NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);
            using NpgsqlCommand Command = new NpgsqlCommand("CALL delete_user(@UserID, @Deleted)", Connection);

            Command.Parameters.AddWithValue("@UserID", userID);

            NpgsqlParameter outParam = new NpgsqlParameter("@Deleted", NpgsqlTypes.NpgsqlDbType.Boolean)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            Command.Parameters.Add(outParam);

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();

                isDeleted = outParam.Value is bool deleted && deleted;
            }
            catch
            {
            }

            return isDeleted;
        }

        public static List<clsUser> GetAllUsers()
        {
            List<clsUser> Users = new();

            using NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);
            using NpgsqlCommand Command = new NpgsqlCommand("SELECT * FROM get_all_users()", Connection);

            try
            {
                Connection.Open();
                var reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    clsUser User = new clsUser
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        Password = reader.GetString(reader.GetOrdinal("Password"))
                    };

                    clsPerson Person = new clsPerson
                    {
                        ID = User.PersonID,
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Age = reader.GetInt32(reader.GetOrdinal("Age")),
                        Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"],
                        Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"],
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"]
                    };

                    User.Person = Person;
                    Users.Add(User);
                }
            }
            catch
            {
            }

            return Users;
        }
    }
}
