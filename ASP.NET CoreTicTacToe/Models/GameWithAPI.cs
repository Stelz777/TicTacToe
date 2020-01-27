using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class GameWithAPI
    {
        public Game Game { get; set; }
        public GameAPI GameAPI { get; set; }

        public GameWithAPI(Game game, GameAPI gameAPI)
        {
            this.Game = game;
            this.GameAPI = gameAPI;
        }
    }
}
