﻿using ASP.NETCoreTicTacToe.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class GameAPI
    {
        private DatabaseWorker databaseWorker;

        public GameAPI(TicTacToeContext database, IMapper mapper)
        {
            databaseWorker = new DatabaseWorker(database, mapper);
        }

        public int GetNewId()
        {
            return databaseWorker.GetNewId();
        }

        public Game GetGame(int? id)
        {
            return databaseWorker.GetGameFromDatabase(id);
        }

        public void AddGame(Game game)
        {
            databaseWorker.AddGameToDatabase(game);
        }

        public void AddTurn(Turn turn, History history)
        {
            databaseWorker.AddTurnToDatabase(turn, history);
        }
    }
}
