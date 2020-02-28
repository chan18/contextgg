using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace interview
{
    public class Program
    {
        Match match;
        public int numberOfTeams;

        public Program(int numberOfteams = 2)
        {
            match = new Match();
            numberOfTeams = numberOfteams;
        }

        static void Main(string[] args)
        {       
         
            var lorem = new Bogus.DataSets.Lorem("en");            
            var testUser = new Faker<PlayerDetails>()
                                .StrictMode(true)
                                .RuleFor(o => o.UserName, f => lorem.Word())
                                .RuleFor(o => o.Rating, f => f.Random.Number(300,3000));
           var players =  testUser.Generate(6);

           Program program = new Program(2);

           // sorting
            players  = players.OrderBy(x => x.Rating).ToList();

            for (int i = 0; i < program.numberOfTeams; i++)
            {
                program.match.teams.Add(BuildTeam(players,program.numberOfTeams-i));
            }

            Console.WriteLine(program.match);
        }

        static List<PlayerDetails> BuildTeam(List<PlayerDetails> players,int numberOfTeams) 
        {
            List<PlayerDetails> temp = new List<PlayerDetails>();
            var numberOfplayersPerTeam = players.Count/numberOfTeams;

            foreach (var item in players.ToList())
            {
                var index = players.IndexOf(item);

                if (numberOfplayersPerTeam == index)
                {
                    return temp;
                }

                if (players.ElementAtOrDefault(index) != null)
                {
                    temp.Add(item);
                    players.RemoveAt(index);

                    if (players.ElementAtOrDefault(players.Count() - 1) != null)
                    {
                        temp.Add(players.ElementAt(players.Count() - 1));
                        players.RemoveAt(players.Count() - 1);
                    }
                }
            }

            return temp;
        }
    }

    internal class Match
    {
        public List<List<PlayerDetails>> teams {get; set;}
    }

    internal class PlayerDetails
    {
        public string UserName {get; set;}
        public int Rating {get;set;}
    }
}
        