﻿using ASP.NETCoreTicTacToe.Infrastructure.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class DatabaseWorker
    {
        private TicTacToeContext database;
        private IMapper mapper;

        public DatabaseWorker(TicTacToeContext database, IMapper mapper)
        {
            this.database = database;
            this.mapper = mapper;
        }

        public Game GetGameFromDatabase(int? id)
        {
            var game = mapper.Map<Game>(database.Games
                .Include(game => game.History)
                .ThenInclude(history => history.Turns)
                .Include(game => game.Board)
                .FirstOrDefault(game => game.ID == id.Value));
            if (game != null)
            {
                var turns = database.Turns.Where(turn => turn.HistoryId == game.History.ID).ToList();
                foreach (var turn in turns)
                {
                    game.History.Turns.Add(mapper.Map<Turn>(turn));
                }
            }
            return game;
        }

        public int AddGameToDatabase(Game newGame)
        {
            if (newGame != null)
            {

                var gameDataTransferObject = mapper.Map<GameDataTransferObject>(newGame);
                database.Games.Add(gameDataTransferObject);
                gameDataTransferObject.History.Turns[0].HistoryId = gameDataTransferObject.History.Id;
                database.SaveChanges();
                return gameDataTransferObject.ID;
            }
            return -1;
        }

        public Guid GetHistoryId(int gameId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TicTacToeContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            using (var context = new TicTacToeContext(optionsBuilder.Options))
            {
                var query = context.Games
                    .Where(game => game.ID == gameId)
                    .Include(game => game.History)
                    .FirstOrDefault<GameDataTransferObject>();
                return query.History.Id;
            }
        }

        public void AddTurnsToDatabase(Game game, int gameId)
        {
            var historyId = GetHistoryId(gameId);
            for (int i = 1; i < game.History.Turns.Count; i++)
            {
                var turn = game.History.Turns[i];
                var turnDataTransferObject = mapper.Map<TurnDataTransferObject>(turn);
                turnDataTransferObject.HistoryId = historyId;
                
                database.Turns.Add(turnDataTransferObject);
            }
            database.SaveChanges();
        }

        public int GetNewId()
        {
            if (database.Games.Any())
            {
                return database.Games.Max(entry => entry.ID) + 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
