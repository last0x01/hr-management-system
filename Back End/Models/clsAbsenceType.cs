namespace Back_End.Models
{
    public class clsAbsenceType
    {
        public int AbsenceTypeID { get; set; } = -1;
        public string Name { get; set; } = string.Empty;

        public clsAbsenceType() { }

        public clsAbsenceType(clsAbsenceType type)
        {
            AbsenceTypeID = type.AbsenceTypeID;
            Name = type.Name;
        }
    }
}
