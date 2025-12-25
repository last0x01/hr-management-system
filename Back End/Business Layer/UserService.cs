using Back_End.Models;
using Business_Layer.Interfaces;
using Data_Access_Layer;

namespace Business_Layer
{

    public class UserService : IUserService
    {



        public clsUser? GetUserByID(int UserID)
        {

            return clsUserData.GetUserByID(UserID);
        }

        public List<clsUser> GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }



        public bool AddUser(clsUser User)
        {
            return clsUserData.AddNewUser(User) != -1;
        }

        public bool UpdateUser(clsUser User)
        {
            return clsUserData.UpdateUser(User);
        }

        public bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }



    }
}
