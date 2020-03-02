using ASP.NETCoreTicTacToe.Models;
using ASP.NETCoreTicTacToe.Models.Bots;
using ASP.NETCoreTicTacToe.Models.Games;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace ASP.NETCoreTicTacToe.Infrastructure
{
    public class GameAPI
    {
        private readonly GameDbRepository gameRepository;
        
        public GameAPI(TicTacToeContext context, IMapper mapper)
        {
            gameRepository = new GameDbRepository(context, mapper);
        }

        public Dictionary<int, Game> GetAllGames()
        {
            return gameRepository.GetAllGamesFromDatabase();
        }

        public (int, Game) GetGame(int? id, string bot, string difficulty)
        {
            if (id.HasValue)
            {
                var game = GetGameFromDatabase(id);
                if (game == null)
                {
                    throw new InvalidOperationException("A game not found by specified id in game farm or db");
                }
                InitBot(game, bot, difficulty);
                
                return (id.Value, game);
            }

            var newGame = new Game();                
            var newId = AddGame(newGame);
            InitBot(newGame, bot, difficulty);
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

        private static void InitBot(Game game, string bot, string difficulty)
        {
            if (game == null || bot == null)
            {
                return;
            }

            bot = bot.ToUpperInvariant();
            MakeBot(game, bot, difficulty);
            if (bot.Equals("X", StringComparison.OrdinalIgnoreCase))
            {
                BotManager.MakeFirstXMove(game);
            }
        }

        private static void MakeBot(Game game, string bot, string difficulty)
        {
            if (difficulty == null)
            {
                return;
            }
            if (bot.Contains("X", StringComparison.OrdinalIgnoreCase))
            {
                if (difficulty.ToUpperInvariant() == "MINIMAX")
                {
                    game.TicPlayer.MakeMinimaxBot();
                    game.TicPlayer.Name = "M1n1m4xX";
                }
                else
                {
                    game.TicPlayer.MakeSimpleBot();
                    game.TicPlayer.Name = "S1mpleX";
                }
            }

            if (bot.Contains("O", StringComparison.OrdinalIgnoreCase))
            {
                if (difficulty.ToUpperInvariant() == "MINIMAX")
                {
                    game.TacPlayer.MakeMinimaxBot();
                    game.TacPlayer.Name = "M1n1m4xO";
                }
                else
                {
                    game.TacPlayer.MakeSimpleBot();
                    game.TacPlayer.Name = "S1mpleO";
                }
            }
        }
    }
}
