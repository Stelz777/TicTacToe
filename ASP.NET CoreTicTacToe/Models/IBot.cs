namespace ASP.NETCoreTicTacToe.Models
{
    public interface IBot
    {
        Turn MakeAutoMove(DatabaseWorker databaseWorker);
    }
}
