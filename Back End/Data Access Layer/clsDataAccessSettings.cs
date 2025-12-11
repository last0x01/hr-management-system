using System.Configuration;

namespace Data_Access_Layer
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
    }
}
