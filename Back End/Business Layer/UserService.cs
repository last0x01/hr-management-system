using Back_End.Models;
using Data_Access_Layer;

namespace Business_Layer
{
    public class UserService
    {
        private enum enMode { Add = 1, Update = 2 }
        private enMode _Mode = enMode.Add;

        private int _UserID;
        private int _PersonID;
        private clsPerson _Person;
        private string _Username;
        private string _Password;

        public UserService()
        {
            _UserID = -1;
            _PersonID = -1;
            _Person = new clsPerson();
            _Username = string.Empty;
            _Password = string.Empty;

            _Mode = enMode.Add;
        }

        public UserService(clsUser user)
        {
            _UserID = user.UserID;
            _PersonID = user.PersonID;
            _Person = new clsPerson(user.Person);
            _Username = user.Username;
            _Password = user.Password;

            _Mode = enMode.Update;
        }


        public static clsUser? GetUserByID(int userID)
        {


            return clsUserData.GetUserByID(userID);
        }


        private clsUser _BuildUser()
        {
            return new clsUser
            {
                UserID = _UserID,
                PersonID = _PersonID,
                Person = _Person,
                Username = _Username,
                Password = _Password
            };
        }

        private bool _AddUser()
        {
            return clsUserData.AddNewUser(_BuildUser()) != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(_BuildUser());
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.Add:
                    if (_AddUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }
    }
}
