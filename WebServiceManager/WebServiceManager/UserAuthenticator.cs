namespace WebServiceManagementSystem
{
    public class UserAuthenticator
    {
        private string[,] userCredentials;

        public UserAuthenticator()
        {
            userCredentials = new string[,]
            {
                { "admin", "admin123" },
                { "user1", "password1" },
                { "user2", "password2" }
            };
        }

        public bool Authenticate(string username, string password)
        {
            for (int i = 0; i < userCredentials.GetLength(0); i++)
            {
                if (userCredentials[i, 0] == username && userCredentials[i, 1] == password) //([i, j] - i - row, j - column)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
