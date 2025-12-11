using Back_End.Models;
using Npgsql;

namespace Data_Access_Layer
{
    public class clsDepartmentData
    {
        public static int AddNewDepartment(clsDepartment Department)
        {
            int NewDepartmentID = 0;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "SELECT add_department(@Name, @Description, @IsActive)",
                    Connection);

                Command.Parameters.AddWithValue("@Name", Department.DepartmentName);
                Command.Parameters.AddWithValue("@Description", Department.Description);
                Command.Parameters.AddWithValue("@IsActive", Department.IsActive);

                try
                {
                    Connection.Open();
                    object? Result = Command.ExecuteScalar();

                    if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                        NewDepartmentID = InsertedID;
                }
                catch
                {
                }
            }

            return NewDepartmentID;
        }


        public static bool UpdateDepartment(clsDepartment Department)
        {
            bool IsUpdated = false;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "CALL update_department(@DeptID, @Name, @Description, @IsActive, @Updated)",
                    Connection);

                Command.Parameters.AddWithValue("@DeptID", Department.DepartmentID);
                Command.Parameters.AddWithValue("@Name", Department.DepartmentName);
                Command.Parameters.AddWithValue("@Description", Department.Description);
                Command.Parameters.AddWithValue("@IsActive", Department.IsActive);

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


        public static bool DeleteDepartment(int DepartmentID)
        {
            bool IsDeleted = false;

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "CALL delete_department(@DeptID, @Deleted)",
                    Connection);

                Command.Parameters.AddWithValue("@DeptID", DepartmentID);

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


        public static List<clsDepartment> GetAllDepartments()
        {
            List<clsDepartment> Departments = new List<clsDepartment>();

            using (NpgsqlConnection Connection =
                   new NpgsqlConnection(clsDataAccessSettings.ConnectionString))
            {
                NpgsqlCommand Command = new NpgsqlCommand(
                    "SELECT * FROM get_all_departments()",
                    Connection);

                try
                {
                    Connection.Open();
                    NpgsqlDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        clsDepartment Department = new clsDepartment
                        {
                            DepartmentID = (int)Reader["DepartmentID"],
                            DepartmentName = (string)Reader["DepartmentName"],
                            Description = (string)Reader["Description"],
                            IsActive = (bool)Reader["IsActive"]
                        };

                        Departments.Add(Department);
                    }
                }
                catch
                {
                }
            }

            return Departments;
        }
    }
}
