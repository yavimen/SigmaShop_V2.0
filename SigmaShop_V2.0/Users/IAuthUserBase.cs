using SigmaShop.StringParser.UserParser;

namespace SigmaShop.Users
{
    interface IAuthUserBase
    {
        void AddUser(CAuthorizedUser user);

        void DeleteUser(CAuthorizedUser user);

        bool IsContain(string login);

        void FillFromFile(IUserParser parser, string path);

        void FillFile(string path);

        CAuthorizedUser this[int index] { get; }

        int Length { get; }

        public string ShowUsers();

        public CAuthorizedUser GetUser(string login);
    }
}
