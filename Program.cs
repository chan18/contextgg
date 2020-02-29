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

           program.players =  testUser.Generate(100);

           program.players  = program.players.OrderBy(x => x.Rating).ToList();

           program.match.teams = new List<List<PlayerDetails>>();

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
                if (program.players.ElementAtOrDefault(index) != null)
                {
                    if (numberOfplayersPerTeam != temp.Count)
                    {
                        temp.Add(program.players[index]);
                        program.players.RemoveAt(index);
                    }
                    else
                    {
                        return temp;
                    }

                    if (numberOfplayersPerTeam != temp.Count)
                    {
                        if (program.players.ElementAtOrDefault(program.players.Count() - 1) != null)
                        {
                            temp.Add(program.players[program.players.Count() - 1]);
                            program.players.RemoveAt(program.players.Count() - 1);
                        }
                    }
                    else
                    {
                        return temp;
                    }
                }
            }

            // edge case
            if (program.players.Count != 0)
            {
                for  (int index = 0; index < program.players.Count; index++)
                {
                    temp.Add(program.players[index]);
                }

                program.players = new List<PlayerDetails>();;
            }

            return temp;
        }
    }

    public class Match
    {
        public List<List<PlayerDetails>> teams {get; set;}
    }

    public class PlayerDetails
    {
        public string UserName {get; set;}
        public int Rating {get;set;}
    }
}