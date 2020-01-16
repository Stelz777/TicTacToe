using ASP.NET_CoreTicTacToe.Controllers;
using ASP.NET_CoreTicTacToe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestTicTacToe
{
    public class UnitTestBoard
    {
        [Fact]
        public void SetSquareFact()
        {
            var board = new Board();
            board.SetSquare(0, Cell.Cross);
            var expectedResult = new List<Cell>();
            expectedResult.AddRange(Enumerable.Repeat(Cell.Empty, 9));
            expectedResult[0] = Cell.Cross;
            Assert.Equal(expectedResult, board.Squares);
        }

        [Fact]
        public void SetSquaresFact()
        {
            var board = new Board();
            var squares = new List<Cell>();
            squares.Add(Cell.Cross);
            board.SetSquares(squares);
            var expectedResult = new List<Cell>();
            expectedResult.Add(Cell.Cross);
            Assert.Equal(expectedResult, board.Squares);
        }

        [Fact]
        public void GetEmptySquareIndexesFact()
        {
            var board = new Board();
            IEnumerable<int> result = board.GetEmptySquareIndexes();
            var expectedResult = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.Equal(expectedResult, result);
        }
    }

    public class UnitTestBot
    {
        [Theory]
        [InlineData("Tac")]
        public void InitSideTheory(string expectedResult)
        {
            var game = new Game();
            var bot = new SimpleBot(game);
            bot.InitSide("Tac");
            Assert.Equal(expectedResult, bot.side);
        }
    }

    public class UnitTestGame
    {
        [Fact]
        public void MakeMoveFact()
        {
            var game = new Game();
            var turn = new Turn();
            bool result = game.MakeMove(turn);
            Assert.True(result);
        }

        [Fact]
        public void GetInvalidTurnFact()
        {
            var game = new Game();
            Turn result = game.GetInvalidTurn();
            var expectedResult = new Turn
            {
                CellNumber = -1,
                TicTurn = false
            };
            Assert.Equal(expectedResult.CellNumber, result.CellNumber);
            Assert.Equal(expectedResult.TicTurn, result.TicTurn);
        }
    }

    public class UnitTestTicTacToeRulesHelper
    {
        [Fact]
        public void HasWinnerFact()
        {
            var squares = new List<Cell>();
            squares.AddRange(Enumerable.Repeat(Cell.Empty, 9));
            bool result = TicTacToeRulesHelper.HasWinner(squares);
            Assert.False(result);
        }
    }

    public class UnitTestBotFarm
    {
        [Fact]
        public void CreateSimpleBotFact()
        {
            var game = new Game();
            var result = BotFarm.CreateSimpleBot(game);
            var expectedResult = new SimpleBot(game);
            Assert.Equal(expectedResult.game, result.game);
        }

        [Fact]
        public void AddBotToGroupFact()
        {
            var botFarm = new BotFarm();
            var game = new Game();
            var bot = new SimpleBot(game);
            botFarm.AddBotToPool(bot);
            var expectedResult = new List<SimpleBot>();
            expectedResult.Add(bot);
            Assert.Equal(expectedResult, botFarm.BotGroup);
        }
    }

    public class UnitTestFarmController : ControllerBase
    {
        [Fact]
        public void GetGameFact()
        {
            var farmController = new FarmController(GameFarm.Current);
            OkObjectResult result = farmController.GetGame(0) as OkObjectResult;
            var history = new History();
            var expectedResult = Ok(new
            {
                id = 0,
                history
            });
            Assert.Equal(expectedResult.Value.ToString(), result.Value.ToString());
        }
    }

    public class UnitTestGameController
    {
        [Fact]
        public void MakeTurnFact()
        {
            var loggerFactory = new LoggerFactory();
            var logger = new Logger<GameController>(loggerFactory);
            var gameController = new GameController(BotFarm.Current, GameFarm.Current, logger);
            var result = gameController.MakeTurn(0, new Turn());
            Assert.True(result);
        }
    }

    public class CustomUnitTests
    {
        [Fact]
        public void PlayBotVersusBot()
        {
            var game = new Game();
            var ticBot = new SimpleBot(game);
            ticBot.InitSide("Tic");
            var tacBot = new SimpleBot(game);
            tacBot.InitSide("Tac");
            while (!BoardIsFull(game.Board) && !TicTacToeRulesHelper.HasWinner(game.Board.Squares))
            {
                ticBot.MakeAutoMove();
                tacBot.MakeAutoMove();
            }
            Assert.True(true);
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
