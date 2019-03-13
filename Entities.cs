namespace Maksim.SeleniumTests
{
    public class UserEntity
    {
        public string DisplayName;
        public string UserName;
        public string Password;

        public UserEntity(string UserName, string Password, string DisplayName = "")
        {
            this.UserName = UserName;
            this.Password = Password;
            this.DisplayName = DisplayName;
        }
    }
}