using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameAPI
    {
        private readonly GameDbRepository gameRepository;
        private readonly Lobby lobby;
        

        public GameAPI(TicTacToeContext context, IMapper mapper, Lobby lobby)
        {
            gameRepository = new GameDbRepository(context, mapper);
            this.lobby = lobby;
        }

        public Dictionary<int, Game> GetAllGames()
        {
            return gameRepository.GetAllGamesFromDatabase();
        }

        public (int, Game) GetGame(int? id, string bot)
        {
            if (id.HasValue)
            {
                var game = GetGameFromDatabase(id);
                if (game == null)
                {
                    throw new InvalidOperationException("A game not found by specified id in game farm or db");
                }
                InitBot(game, id.Value, bot);
                return (id.Value, game);
            }

            var newGame = new Game();                
            var newId = AddGame(newGame);
            InitBot(newGame, newId, bot);
            return (newId, newGame);
        }

        public void UpdateGame(Game game, int gameId)
        {
            if (game != null)
            {
                gameRepository.UpdateGameInDatabase(game, gameId);   
            }
        }

        private Game GetGameFromDatabase(int? id)
        {
            return gameRepository.GetGameFromDatabase(id);
        }

        private int AddGame(Game game)
        {
            return gameRepository.AddGameToDatabase(game);
        }

        

        private void InitBot(Game game, int gameId, string bot)
        {
            if (game == null || bot == null)
            {
                return;
            }

            bot = bot.ToUpperInvariant();
            if (bot.Contains("X"))
            {
                SimpleBot.Create(game.TicPlayer, "S1mpleX");
            }

            if (bot.Contains("O"))
            {
                SimpleBot.Create(game.TacPlayer, "S1mpleO");
            }
            if (bot.Equals("X"))
            {
                BotManager.MakeFirstXMove(game);
            }
            else if (bot.Equals("XO"))
            {
                BotManager.PlayBotVsBot(game);
                UpdateGame(game, gameId);
            }
        }
    }
}
