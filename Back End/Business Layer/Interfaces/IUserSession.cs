using Back_End.Models;

namespace Business_Layer.Interfaces
{
    public interface IUserSession
    {

        clsUser? CurrentUser { get; }
        void Login(clsUser User);
        void Logout();
    }
}
