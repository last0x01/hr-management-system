using Back_End.Models;
using Data_Access_Layer;

namespace Business_Layer
{
    public class PersonService
    {


        public static clsPerson? GetPersonByID(int PersonID)
        {
            return clsPersonData.GetPersonByID(PersonID);

        }

    }
}
