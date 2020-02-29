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

        public List<PlayerDetails> players;

        public Program(int numberOfteams = 2)
        {
            match = new Match();
            numberOfTeams = numberOfteams;
            players = new List<PlayerDetails>();
        }

        static void Main(string[] args)
        {
            Program program = new Program(2);

            var lorem = new Bogus.DataSets.Lorem("en");            
            var testUser = new Faker<PlayerDetails>()
                                .StrictMode(true)
                                .RuleFor(o => o.UserName, f => lorem.Word())
                                .RuleFor(o => o.Rating, f => f.Random.Number(300,3000));

           program.players =  testUser.Generate(6);

           program.players  = program.players.OrderBy(x => x.Rating).ToList();

           for (int i = 0; i < program.numberOfTeams; i++)
           {
               program.match.teams.Add(BuildTeam(program,program.numberOfTeams - i));
           }

           Console.WriteLine(program.match);
        }

        static List<PlayerDetails> BuildTeam(Program program,int numberOfTeams) 
        {
            List<PlayerDetails> temp = new List<PlayerDetails>();
            var numberOfplayersPerTeam = program.players.Count/numberOfTeams;

            for (int index = 0; index < program.players.Count; index++)
            {
                if (numberOfplayersPerTeam ==  (index + 1))
                {
                    return temp;
                }

                if (program.players.ElementAtOrDefault(index) != null)
                {
                    temp.Add(program.players[index]);
                    program.players.RemoveAt(index);

                    if (program.players.ElementAtOrDefault(program.players.Count() - 1) != null)
                    {
                        temp.Add(program.players[program.players.Count() - 1]);
                        program.players.RemoveAt(program.players.Count() - 1);
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
        