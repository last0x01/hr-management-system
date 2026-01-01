using Back_End.Models;
using Business_Layer.Interfaces;

namespace Business_Layer
{
    public class UserSession : IUserSession
    {
        private static readonly IUserSession _UserSession = new UserSession();

        public static IUserSession UserInstance => _UserSession;
        public clsUser? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;



        public void Login(clsUser user)
        {
            CurrentUser = user;

        }
        public void Logout()
        {
            CurrentUser = null;

        }

    }
}
