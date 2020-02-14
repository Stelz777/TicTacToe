using ASP.NETCoreTicTacToe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Lobby
    {
        private List<Player> players = new List<Player>();

        public IReadOnlyList<Player> Players => players;

       

        
        public void AddPlayer(string name)
        {
            var player = new Player();
            player.Name = name;
            players.Add(player);
        }
    }
}
