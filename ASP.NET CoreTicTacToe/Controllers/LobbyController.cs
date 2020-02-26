using System.Collections.Generic;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ASP.NETCoreTicTacToe.Infrastructure;

namespace ASP.NETCoreTicTacToe.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private GameAPI gameAPI;

        public LobbyController(GameAPI gameAPI)
        {
            this.gameAPI = gameAPI;
        }

        [HttpGet("{id?}")]
        public IActionResult Game(int? id, string bot)
        {
            
            var (gameId, game) = gameAPI.GetGame(id, bot);
            var history = game.History;
            var boards = history.GetBoardsForEachTurn();
            gameAPI.UpdateGame(game, gameId);
            return Ok(new 
            {
                id = gameId, 
                boards
            });
        }

        [HttpGet]
        public IActionResult AllGames()
        {
            var games = gameAPI.GetAllGames();
            var ids = games.Keys.ToList();
            var ticPlayers = games.Values.Select(item => item.TicPlayer);
            var tacPlayers = games.Values.Select(item => item.TacPlayer);
            var result = games.Select(item =>
            {
                var (gameId, game) = item;
                return new
                {
                    id = gameId,
                    ticPlayer = ConstructPlayerData(game.TicPlayer, gameId),
                    tacPlayer = ConstructPlayerData(game.TacPlayer, gameId)
                };
            });

            return Ok(
                result
            );
        }

        private object ConstructPlayerData(Player player, int gameId)
        {
            if (player.Name == null)
            {
                var (_, game) = gameAPI.GetGame(gameId, null);
                player.Name = player.Side == Side.Tic ? game.TicPlayer.Name : game.TacPlayer.Name;
            }
            return new
            {
                name = player.Name,
                isBot = player.IsBot
            };
        }
    }
}