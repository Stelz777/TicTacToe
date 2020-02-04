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
        private readonly GameFarm gameFarm;

        public GameAPI(GameFarm gameFarm, TicTacToeContext database, IMapper mapper)
        {
            gameRepository = new GameDbRepository(database, mapper);
            this.gameFarm = gameFarm;
        }

        public (int, Game) GetGame(int? id, string bot)
        {
            if (id.HasValue)
            {
                var game = gameFarm.GetGame(id.Value);
                if (game != null)
                {
                    InitBot(game, bot);
                    return (id.Value, game);
                }
                game = GetGameFromDatabase(id);
                if (game == null)
                {
                    throw new InvalidOperationException("A game not found by specified id in game farm or db");
                }
                InitBot(game, bot);
                return (id.Value, game);
            }

            var newGame = new Game();                
            var newId = AddGame(newGame);
            gameFarm.AddGame(newId, newGame);
            InitBot(newGame, bot);
            return (newId, newGame);
        }

        

        public void UpdateGame(Game game, int gameId)
        {
            if (game != null)
            {
                if (game.CanContinue())
                {
                    return;
                }
                gameRepository.UpdateGameInDatabase(game, gameId);
                gameFarm.ExcludeGame(gameId);
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

        public static void InitBot(Game game, string bot)
        {
            if (bot != null)
            {
                if (bot.Equals("X"))
                {
                    game.TicPlayer.Bot = new SimpleBot(game, Side.Tic);
                    game.TicPlayer.Name = "S1mpleX";
                    game.TicPlayer.Bot.MakeAutoMove();
                }
                else if (bot.Equals("O"))
                {
                    game.TacPlayer.Bot = new SimpleBot(game, Side.Tac);
                    game.TacPlayer.Name = "S1mpleO";
                }
            }
        }
    }
}
