using ASP.NETCoreTicTacToe.Controllers;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void GetInvalidTurnFact()
        {
            var game = new Game();
            Turn result = History.GetInvalidTurn();
            var expectedResult = new Turn
            {
                CellNumber = -1,
                Side = Side.Tac
            };
            Assert.Equal(expectedResult.CellNumber, result.CellNumber);
            Assert.Equal(expectedResult.Side, result.Side);
        }
    }

    public class UnitTestTicTacToeRulesHelper
    {
       
    }

    public class UnitTestBotFarm
    {
        [Fact]
        public void CreateSimpleBotFact()
        {
            var game = new Game();
            var result = BotFarm.CreateSimpleBot(game);
            var expectedResult = new SimpleBot(game, Side.Tac);
            Assert.Equal(expectedResult.Game, result.Game);
        }

        [Fact]
        public void AddBotToGroupFact()
        {
            var botFarm = new BotFarm();
            var game = new Game();
            var bot = new SimpleBot(game, Side.Tac);
            botFarm.AddBotToPool(bot);
            var expectedResult = new List<SimpleBot>();
            expectedResult.Add(bot);
            Assert.Equal(expectedResult, botFarm.BotGroup);
        }
    }

    public class CustomUnitTests
    {
        [Fact]
        public void PlayBotVersusBot()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(game, Side.Tic);
            var tacBot = new SimpleBot(game, Side.Tac);
            while (!BoardIsFull(game.Board) && !TicTacToeRulesHelper.HasWinner(game.Board.Squares))
            {
                ticBot.MakeAutoMove();
                tacBot.MakeAutoMove();
            }
        }

        [Fact]
        public void CheckIfTicBotAddsCrosses()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(game, Side.Tic);
            ticBot.MakeAutoMove();
            Assert.Equal(Side.Tic, game.History.LastTurn.Side);
        }

        [Fact]
        public void CheckIfTacBotAddsNoughts()
        {
            var game = new Game();
            game.InitHistory();
            game.InitBoard();
            var ticBot = new SimpleBot(game, Side.Tic);
            var tacBot = new SimpleBot(game, Side.Tac);
            ticBot.MakeAutoMove();
            tacBot.MakeAutoMove();
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
