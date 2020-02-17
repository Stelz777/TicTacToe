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

        public Lobby GetLobby()
        {
            return lobby;
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
            if (game != null && bot != null)
            {
                if (CheckBotEqualsItsSymbol(bot, "X"))
                {
                    game.TicPlayer.Bot = new SimpleBot(Side.Tic);
                    game.TicPlayer.Name = "S1mpleX";
                    game.TicPlayer.Bot.MakeAutoMove(game);
                }
                else if (CheckBotEqualsItsSymbol(bot, "O"))
                {
                    game.TacPlayer.Bot = new SimpleBot(Side.Tac);
                    game.TacPlayer.Name = "S1mpleO";
                }
                else if (CheckBotEqualsItsSymbol(bot, "XO"))
                {
                    game.TicPlayer.Bot = new SimpleBot(Side.Tic);
                    game.TacPlayer.Bot = new SimpleBot(Side.Tac);
                    game.TicPlayer.Name = "S1mpleX";
                    game.TacPlayer.Name = "S1mpleO";
                    while (game.CanContinue())
                    {
                        game.TicPlayer.Bot.MakeAutoMove(game);
                        if (game.CanContinue())
                        {
                            game.TacPlayer.Bot.MakeAutoMove(game);
                        }
                    }
                    UpdateGame(game, gameId);
                }
            }
        }

        private bool CheckBotEqualsItsSymbol(string bot, string symbol)
        {
            return bot == symbol || bot == symbol.ToLower();
        }
    }
}
