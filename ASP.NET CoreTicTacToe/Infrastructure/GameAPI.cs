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
        private readonly GameFarm gameFarm;
        private readonly Lobby lobby;
        

        public GameAPI(TicTacToeContext context, GameFarm gameFarm, IMapper mapper, Lobby lobby)
        {
            gameRepository = new GameDbRepository(context, mapper);
            this.gameFarm = gameFarm;
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
                var game = gameFarm.GetGame(id.Value);
                if (game != null)
                {
                    InitBot(game, id.Value, bot);
                    return (id.Value, game);
                }
                game = GetGameFromDatabase(id);
                if (game == null)
                {
                    throw new InvalidOperationException("A game not found by specified id in game farm or db");
                }
                InitBot(game, id.Value, bot);
                return (id.Value, game);
            }

            var newGame = new Game();                
            var newId = AddGame(newGame);
            gameFarm.AddGame(newId, newGame);
            InitBot(newGame, newId, bot);
            return (newId, newGame);
        }

        public Lobby GetLobby()
        {
            return lobby;
        }


        public void UpdateGame(Game game, int gameId)
        {
            if (game != null)
            {
                if (game.CanContinue())
                {
                    return;
                }
                if (gameFarm.Games.ContainsKey(gameId))
                {
                    gameRepository.UpdateGameInDatabase(game, gameId);
                    gameFarm.ExcludeGame(gameId);
                }
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
            if (game != null && bot != null)
            {
                if (bot == "X")
                {
                    game.TicPlayer.Bot = new SimpleBot(game, Side.Tic);
                    game.TicPlayer.Name = "S1mpleX";
                    game.TicPlayer.Bot.MakeAutoMove();
                }
                else if (bot == "O")
                {
                    game.TacPlayer.Bot = new SimpleBot(game, Side.Tac);
                    game.TacPlayer.Name = "S1mpleO";
                }
                else if (bot == "XO")
                {
                    game.TicPlayer.Bot = new SimpleBot(game, Side.Tic);
                    game.TacPlayer.Bot = new SimpleBot(game, Side.Tac);
                    game.TicPlayer.Name = "S1mpleX";
                    game.TacPlayer.Name = "S1mpleO";
                    while (game.CanContinue())
                    {
                        game.TicPlayer.Bot.MakeAutoMove();
                        if (game.CanContinue())
                        {
                            game.TacPlayer.Bot.MakeAutoMove();
                        }
                    }
                    UpdateGame(game, gameId);
                }
            }
        }
    }
}
