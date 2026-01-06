using Back_End.Models;
using Business_Layer.Interfaces.Services;
using Data_Access_Layer;

namespace Business_Layer.Services
{

    public class UserService : IUserService
    {

        public clsUser? GetUserByUsernameAndPassword(string Username, string Password)
        {

            return clsUserData.GetUserByUsernameAndPassword(Username, Password);
        }


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


        public int AddAndGetUserID(clsUser user)
        {
            return clsUserData.AddNewUser(user);
        }
    }
}
