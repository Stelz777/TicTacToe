namespace ASP.NET_CoreTicTacToe.Models
{
    public class Game
    {
        public History History { get; private set; }
        public Board Board { get; private set; }

        private RealPlayer player;

        private BotFarm botFarm = new BotFarm();

        public Game()
        {
            History = new History();
            Board = History.RestoreBoardByTurnNumber(0);
            player = new RealPlayer();
        }

        public bool MakeMove(Turn turn)
        {
            Turn lastTurn = History.LastTurn;
            if (lastTurn.WhichTurn == turn.WhichTurn)
            {
                return false;
            }
            else
            {
                if (!Board.HasWinner)
                {
                    if (Board.Squares[turn.CellNumber] == Cell.Empty)
                    {
                        Board newBoard = new Board();
                        newBoard.SetSquares(Board.Squares);
                        newBoard.SetSquare(turn.CellNumber, Board.GetCellBySide(turn.WhichTurn));
                        History.Turns.Add(turn);
                        Board = newBoard;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        

        
    }
}
