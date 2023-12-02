using System.Text.RegularExpressions;

public class Day2
{
    private const string Red = "red";
    private const string Blue = "blue";
    private const string Green = "green";

    private const int MaxReds = 12;
    private const int MaxGreens = 13;
    private const int MaxBlues = 14;

    private class Game
    {
        public int Id { get; set; }

        public List<Set> Sets { get; set; } = new();

        public bool IsValid()
        {
            foreach (var set in Sets)
            {
                if (set.Reds > MaxReds || set.Blues > MaxBlues || set.Greens > MaxGreens)
                {
                    return false;
                }
            }

            return true;
        }

        public static Game Parse(string line)
        {
            //parse id
            var game = new Game();
            var initialPart = line.Split(":").First();
            var setsString = line.Split(":").Last();
            game.Id = Int32.Parse(Regex.Match(initialPart, @"\d+").Value);
            
            //parse sets
            var sets = setsString.Split(";");
            foreach (var set in sets)
            {
                game.Sets.Add(Set.Parse(set));
            }
            return game;
        }

        public Set GetFewerItemsSet()
        {
            var fewerItemsSet = new Set();
            foreach (var set in Sets)
            {
                fewerItemsSet.Reds = int.Max(fewerItemsSet.Reds, set.Reds);
                fewerItemsSet.Blues = int.Max(fewerItemsSet.Blues, set.Blues);
                fewerItemsSet.Greens = int.Max(fewerItemsSet.Greens, set.Greens);
            }

            return fewerItemsSet;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    private class Set
    {
        public int Reds { get; set; }
        public int Greens { get; set; }
        public int Blues { get; set; }

        public int Power()
        {
            return Reds * Greens * Blues;
        }

        public static Set Parse(string line)
        {
            var set = new Set();
            var elements = line.Split(",");
            foreach (var element in elements)
            {
                var value = Int32.Parse(Regex.Match(element, @"\d+").Value);
                if (element.Contains(Red))
                {
                    set.Reds = value;
                }
                else if (element.Contains(Blue))
                {
                    set.Blues = value;
                }
                else if (element.Contains(Green))
                {
                    set.Greens = value;
                }
            }

            return set;
        }
    }
    public static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        
        //part 1
        var sum = 0;
        var games = new List<Game>();
        foreach (var line in lines)
        {
            var game = Game.Parse(line);
            games.Add(game);
            Console.WriteLine(game);
            if (game.IsValid())
            {
                sum += game.Id;
            }
        }
        
        Console.WriteLine(sum);
        
        //part 2
        var sum2 = 0;
        foreach (var game in games)
        {
            sum2 += game.GetFewerItemsSet().Power();
        }
        
        Console.WriteLine(sum2);
    }
}