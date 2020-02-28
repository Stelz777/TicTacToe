using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASP.NETCoreTicTacToe.Models.Users
{
    public class User
    {
        public int Id { get; set; }

        [BindRequired]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
