using eLearning.Results;

namespace eLearning.Manager.Interface
{
    public interface IAuthManager
    {
        Task<AuthResult> Login(string identity, string password);
        Task Logout();
    }
}