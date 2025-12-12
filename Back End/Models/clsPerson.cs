namespace Back_End.Models
{
    public class clsPerson
    {
        public int PersonID { get; set; } = -1;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string? Address { get; set; }

        public clsPerson()
        {
        }

        public clsPerson(clsPerson person)
        {
            PersonID = person.PersonID;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = person.Age;
            Phone = person.Phone;
            Email = person.Email;
            Gender = person.Gender;
            Address = person.Address;
        }
    }
}
