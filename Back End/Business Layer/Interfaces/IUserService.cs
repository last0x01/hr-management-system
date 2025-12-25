using Back_End.Models;

namespace Business_Layer.Interfaces
{
    public interface IUserService
    {

        clsUser? GetUserByID(int UserId);
        List<clsUser> GetAllUsers();
        bool AddUser(clsUser User);
        bool UpdateUser(clsUser User);
        bool DeleteUser(int UserId);

    }
}
