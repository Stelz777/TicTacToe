namespace ASP.NETCoreTicTacToe.Models
{
    public class Player
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Side Side { get; set; }
        public IBot Bot { get; set; }
        public bool IsBot => Bot != null;

        public void MakeSimpleBot()
        {
            Bot = new SimpleBot(Side);
            Name = "Simple";
        }
    }
}
