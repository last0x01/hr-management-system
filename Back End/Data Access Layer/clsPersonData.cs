using Back_End.Models;
//using System.Data.SqlClient;
using Npgsql;


namespace Data_Access_Layer
{
    public class clsPersonData
    {

        public static clsPerson GetPersonByID(int PersonID)
        {
            string Query = @" Select * from People where PersonID = @PersonID";

            clsPerson Person = new clsPerson();

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(Query, Connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    Connection.Open();

                    NpgsqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Person = new clsPerson
                        {
                            ID = PersonID,
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

                }

            }

            return Person;
        }









        // Using Stored Procedure
        //public static clsPerson GetPersonByID(int PersonID)
        //{


        //    clsPerson Person = new clsPerson();

        //    using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
        //    {
        //        NpgsqlCommand command = new NpgsqlCommand("People.GetPersonByID", Connection);

        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        command.Parameters.Add("@PersonID", System.Data.SqlDbType.Int).Value = PersonID;

        //        try
        //        {
        //            Connection.Open();

        //            SqlDataReader reader = command.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                Person = new clsPerson
        //                {
        //                    ID = PersonID,
        //                    FirstName = (string)reader["FirstName"],
        //                    LastName = (string)reader["LastName"],
        //                    Age = (int)reader["Age"],
        //                    Phone = (string)reader["Phone"],
        //                    Email = (string)reader["Email"],
        //                    Gender = (string)reader["Gender"],
        //                    Address = (string)reader["Address"]

        //                };
        //            }

        //            reader.Close();

        //        }
        //        catch
        //        {

        //        }

        //    }

        //    return Person;
        //}
    }
}
