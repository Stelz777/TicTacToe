using System.Collections.Generic;
using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult GetGame(int? id, string bot)
        {
            
            var (gameId, game) = gameAPI.GetGame(id, bot);
            var history = game.History;
            var boards = history.GetBoardsForEachTurn();
            
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
                    ticPlayer = ConstructPlayerData(game.TicPlayer),
                    tacPlayer = ConstructPlayerData(game.TacPlayer)
                };
            });

            return Ok(
                result
            );
        }

        [HttpPost]
        public void AddPlayer(Player player)
        {
            var lobby = gameAPI.GetLobby();

            lobby.AddPlayer(player.Name);
        }

        private static object ConstructPlayerData(Player player)
        {
            return new
            {
                name = player.Name,
                isBot = player.IsBot
            };
        }
    }
}