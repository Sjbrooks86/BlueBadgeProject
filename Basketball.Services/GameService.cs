﻿using Basketball.Data;
using Basketball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basketball.Services
{
    public class GameService
    {
        public bool CreateGame(CreateNewGame model)
        {
            var entity = new Game()
            {
                HomeTeamId = model.HomeTeamId,
                AwayTeamId = model.AwayTeamId,
                Date = model.Date,
                HomeTeamScore = model.HomeTeamScore,
                AwayTeamScore = model.AwayTeamScore,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Games.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public GameDetails GetGameById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Games
                   .Single(g => g.GameId == id);

                return new GameDetails()
                {
                    GameId = entity.GameId,
                    HomeTeamName = entity.HomeTeam.Name,
                    AwayTeamName = entity.AwayTeam.Name,
                    Location = entity.Location,
                    Date = entity.Date,
                    HomeTeamScore = entity.HomeTeamScore,
                    AwayTeamScore = entity.AwayTeamScore,
                    WinningTeamName = entity.Winner,
                    LosingTeamName = entity.Loser
                };

            }
        }
        public IEnumerable<GameList> GetGames()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Games.Select(g =>
                    new GameList()
                    {
                        GameId = g.GameId,
                        Date = g.Date,
                        Location = g.Location,
                        HomeTeamName = g.HomeTeam.Name,
                        AwayTeamName = g.AwayTeam.Name
                    });
                return query.ToArray();
            }
        }
    }
}
