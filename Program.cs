using System;
using System.Collections.Generic;
using System.Linq;

namespace interview
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PlayerDetails> players = new  List<PlayerDetails>();

            // this is just random data that you shared
            players.Add(new PlayerDetails{
                UserName = "player1",
                Rating = 1700
            });

            players.Add(new PlayerDetails{
                UserName = "player2",
                Rating = 1420
            });

            players.Add(new PlayerDetails{
                UserName = "player3",
                Rating = 1280
            });

            players.Add(new PlayerDetails{
                UserName = "player4",
                Rating =  1500
            });

            players.Add(new PlayerDetails{
                UserName = "player5",
                Rating = 1100
            });

            players.Add(new PlayerDetails{
                UserName = "player6",
                Rating = 1300
            });

            var numberOfTeams = 2;
            var teamTotalScores =  players.Select(x => x.Rating).Sum();
            var averageTeamScore = teamTotalScores / numberOfTeams;

            // sorting
            players  = players.OrderBy(x => x.Rating).ToList();

            // possibility of team formmation
            List<PlayerDetails> teamA = new List<PlayerDetails>(
                new PlayerDetails[] {
                    players[0],
                    players[players.Count()/2],
                    players[players.Count() - 1]
                }
            );

            List<PlayerDetails> teamB = new List<PlayerDetails>(
                new PlayerDetails[] {
                    players[1],
                    players[(players.Count()/2) - 1],
                    players[players.Count() - 2]
                }
            );
        }

        internal class PlayerDetails
        {
            public string UserName {get; set;}
            public int Rating {get;set;}
        }

    }
}
