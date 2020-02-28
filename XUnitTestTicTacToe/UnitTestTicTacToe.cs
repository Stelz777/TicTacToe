using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Models.Bots;
using ASP.NETCoreTicTacToe.Models.Games;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestTicTacToe
{
    public class UnitTestBoard
    {
        [Fact]
        public void GetEmptySquareIndexesFact()
        {
            var board = new Board();
            IEnumerable<int> result = board.GetEmptySquareIndexes();
            var expectedResult = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.Equal(expectedResult, result);
        }
    }

    public class UnitTestGame
    {
        [Fact]
        public void MakeMoveFact()
        {
            var game = new Game();
            var turn = new Turn();
            game.InitHistory();
            game.InitBoard();
            bool result = game.MakeMove(turn);
            Assert.True(result);
        }
    }

    public class UnitTestTicTacToeRulesHelper
    {
       
    }

    public class CustomUnitTests
    {
        [Fact]
        public void PlayBotVersusBot()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(Side.Tic);
            var tacBot = new SimpleBot(Side.Tac);
            while (!BoardIsFull(game.Board) && !TicTacToeRulesHelper.HasWinner(game.Board.Squares))
            {
                ticBot.MakeAutoMove(game);
                tacBot.MakeAutoMove(game);
            }
        }

        [Fact]
        public void CheckIfTicBotAddsCrosses()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(Side.Tic);
            ticBot.MakeAutoMove(game);
            Assert.Equal(Side.Tic, game.History.LastTurn.Side);
        }

        [Fact]
        public void CheckIfTacBotAddsNoughts()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(Side.Tic);
            var tacBot = new SimpleBot(Side.Tac);
            ticBot.MakeAutoMove(game);
            tacBot.MakeAutoMove(game);
            Assert.Equal(Side.Tac, game.History.LastTurn.Side);
        }

        public bool BoardIsFull(Board board)
        {
            

            foreach (var square in board.Squares)
            {
                if (square == Cell.Empty)
                {
                    return false;
                }
            }
            return true;
        }
    }




}
