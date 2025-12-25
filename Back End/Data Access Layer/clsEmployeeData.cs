using Back_End.Models;
using Npgsql;

namespace Data_Access_Layer
{
    public class clsEmployeeData
    {

        public static clsEmployee? GetEmployeeByID(int employeeID)
        {
            const string query = @"select * from Employees where EmployeeID = @EmployeeID";

            using (NpgsqlConnection connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EmployeeID", employeeID);

                try
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        clsEmployee employee = new clsEmployee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            PersonID = (int)reader["PersonID"],
                            JobPosition = (string)reader["job_position"],
                            Salary = (decimal)reader["Salary"],
                            DepartmentID = reader["DepartmentID"] == DBNull.Value
                                ? null
                                : (int)reader["DepartmentID"]
                        };

                        employee.Person = new clsPerson
                        {
                            PersonID = (int)reader["PersonID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Age = (int)reader["Age"],
                            Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"],
                            Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"],
                            Gender = (string)reader["Gender"],
                            Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"]
                        };

                        return employee;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }


        public static int AddNewEmployee(clsEmployee Employee)
        {
            int NewEmployeeID = 0;

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {

                NpgsqlCommand Command = new NpgsqlCommand("select add_employee(@FirstName, @LastName, @Age,@Phone ,@Email, @Gender, @Address,@DepartmentID, @Salary,@job_position)", Connection);



                Command.Parameters.AddWithValue("@FirstName", Employee.Person.FirstName);
                Command.Parameters.AddWithValue("@LastName", Employee.Person.LastName);
                Command.Parameters.AddWithValue("@Age", Employee.Person.Age);
                Command.Parameters.AddWithValue("@Gender", Employee.Person.Gender);

                Command.Parameters.AddWithValue("@Address", Employee.Person.Address ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@Phone", Employee.Person.Phone ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@Email", Employee.Person.Email ?? (object)DBNull.Value);

                //Employee Columns

                Command.Parameters.AddWithValue("@Salary", Employee.Salary);
                Command.Parameters.AddWithValue("@DepartmentID", Employee.DepartmentID ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@job_position", Employee.JobPosition);


                try
                {
                    Connection.Open();


                    object? result = Command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        NewEmployeeID = InsertedID;
                    }
                }
                catch
                {

                }
            }

            return NewEmployeeID;
        }

        public static bool UpdateEmployee(clsEmployee Employee)
        {
            bool IsUpdated = false;

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand("call update_employee(@PersonID, @FirstName, @LastName, @Age,@Phone ,@Email, @Address,@DepartmentID, @Salary,@job_position)", Connection);



                Command.Parameters.AddWithValue("@FirstName", Employee.Person.FirstName);
                Command.Parameters.AddWithValue("@LastName", Employee.Person.LastName);
                Command.Parameters.AddWithValue("@Age", Employee.Person.Age);
                Command.Parameters.AddWithValue("@Gender", Employee.Person.Gender);

                Command.Parameters.AddWithValue("@Address", Employee.Person.Address ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@Phone", Employee.Person.Phone ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@Email", Employee.Person.Email ?? (object)DBNull.Value);

                Command.Parameters.AddWithValue("@PersonID", Employee.PersonID);
                Command.Parameters.AddWithValue("@Salary", Employee.Salary);
                Command.Parameters.AddWithValue("@DepartmentID", Employee.DepartmentID ?? (object)DBNull.Value);
                Command.Parameters.AddWithValue("@job_position", Employee.JobPosition);



                try
                {
                    Connection.Open();

                    object? result = Command.ExecuteScalar();

                    // Safely read the OUT parameter:
                    // - Checks that the value is a non-null boolean.
                    // - If so, assigns it to 'updated' and returns its value.
                    // - If null or not a bool → result is false.

                    IsUpdated = result is bool updated && updated;


                }
                catch
                {

                }


            }

            return IsUpdated;
        }

        public static bool DeleteEmployee(int employeeID)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString);

            using NpgsqlCommand Command = new NpgsqlCommand("SELECT delete_employee(@EmployeeID)", connection);

            Command.Parameters.AddWithValue("@EmployeeID", employeeID);


            try
            {
                connection.Open();

                object? result = Command.ExecuteScalar();

                return result is bool deleted && deleted;
            }
            catch
            {

            }

            return false;
        }



        public static List<clsEmployee> GetAllEmployees()
        {
            List<clsEmployee> Employees = new List<clsEmployee>();

            using (NpgsqlConnection Connection = new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand("select * from get_all_Employees()", Connection);



                try
                {
                    Connection.Open();

                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        clsEmployee Employee = new clsEmployee
                        {
                            EmployeeID = (int)reader["EmployeeID"],
                            PersonID = (int)reader["PersonID"],
                            JobPosition = (string)reader["job_position"],
                            Salary = (decimal)reader["Salary"],
                            DepartmentID = reader["DepartmentID"] == DBNull.Value ? null : (int)reader["DepartmentID"]
                        };

                        clsPerson Person = new clsPerson
                        {
                            PersonID = (int)reader["PersonID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Age = (int)reader["Age"],
                            Phone = reader["Phone"] == DBNull.Value ? null : (string)reader["Phone"],
                            Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"],
                            Gender = (string)reader["Gender"],
                            Address = reader["Address"] == DBNull.Value ? null : (string)reader["Address"]
                        };

                        Employee.Person = Person;

                        Employees.Add(Employee);
                    }
                }
                catch
                {

                }
            }

            return Employees;
        }

    }
}
