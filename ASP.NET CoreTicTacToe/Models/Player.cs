namespace ASP.NETCoreTicTacToe.Models
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Side Side { get; set; }

        public bool IsBot { get; set; }

        public string Difficulty { get; set; }
    }
}
