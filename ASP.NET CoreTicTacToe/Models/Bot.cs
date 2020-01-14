using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class Bot
    {
        public bool isActive;
        public string side;
        public Game game;

        public Bot(Game game)
        {
            this.game = game;
        }

        public void InitSide()
        {
            side = "Tac";
        }

        public Turn MakeAutoMove(Board board)
        {
            var ticTacToeRulesHelper = new TicTacToeRulesHelper();
            var possibleTurns = new List<int>(board.GetEmptySquareIndexes());
            

            var random = new Random();
            int randomTurn = random.Next(0, possibleTurns.Count);

            if (possibleTurns.Count > 0)
            {
                if (!board.HasWinner)
                {
                    board.SetSquare(Convert.ToInt32(possibleTurns[randomTurn]), Cell.Nought);
                    game.History.Turns.Add(board);
                    game.Board = board;
                    return new Turn
                    {
                        CellNumber = Convert.ToInt32(possibleTurns[randomTurn]),
                        TicTurn = false
                    };
                }
                else
                {
                    return game.GetInvalidTurn();
                }
            }
            else
            {
                return game.GetInvalidTurn();
            }
        }
    }
}
