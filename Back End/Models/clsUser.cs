namespace Back_End.Models
{
    public class clsUser
    {

        public int UserID { get; set; } = -1;

        public int PersonID { get; set; } = -1;

        public clsPerson Person { get; set; } = new clsPerson();

        public string Username { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;

        public clsUser()
        {
        }

        public clsUser(clsUser user)
        {
            UserID = user.UserID;
            PersonID = user.PersonID;
            Username = user.Username;
            Password = user.Password;
            Person = new clsPerson(user.Person);
        }
    }
}
