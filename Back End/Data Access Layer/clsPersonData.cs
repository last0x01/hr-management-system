using Back_End.Models;
//using System.Data.SqlClient;
using Npgsql;


namespace Data_Access_Layer
{
    public class clsPersonData
    {

        public static clsPerson? GetPersonByID(int PersonID)
        {


            const string Query = @"
                                    SELECT PersonID, FirstName, LastName, Age, Phone, Email, Gender, Address
                                    FROM People
                                    WHERE PersonID = @PersonID";



            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using NpgsqlCommand command = new NpgsqlCommand(Query, Connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    Connection.Open();

                    using NpgsqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new clsPerson
                        {
                            PersonID = PersonID,
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Age = (int)reader["Age"],
                            Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"],
                            Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"],
                            Gender = (string)reader["Gender"],
                            Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"]


                        };


                    }

                    reader.Close();

                }
                catch
                {
                    return null;
                }

            }

            return null;
        }







    }
}