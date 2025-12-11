namespace Back_End.Models
{
    public class clsPerson
    {
        /*
         Phone allows null && Email allows null && Address allows null
         */
        private int _ID;
        private string _FirstName;
        private string _LastName;
        private int _Age;
        private string? _Phone;
        private string? _Email;
        private string _Gender;
        private string? _Address;

        public clsPerson()
        {
            _ID = -1;
            _FirstName = "";
            _LastName = "";
            _Age = 0;
            _Phone = null;
            _Email = null;
            _Gender = "";
            _Address = null;
        }

        public int ID
        {
            get => _ID;
            set { _ID = value; }
        }

        public string FirstName
        {
            get => _FirstName;
            set { _FirstName = value; }
        }

        public string LastName
        {
            get => _LastName;
            set { _LastName = value; }
        }

        public int Age
        {
            get => _Age;
            set { _Age = value; }
        }

        public string? Phone
        {
            get => _Phone;
            set { _Phone = value; }
        }

        public string? Email
        {
            get => _Email;
            set { _Email = value; }
        }

        public string Gender
        {
            get => _Gender;
            set { _Gender = value; }
        }

        public string? Address
        {
            get => _Address;
            set { _Address = value; }
        }


    }
}
