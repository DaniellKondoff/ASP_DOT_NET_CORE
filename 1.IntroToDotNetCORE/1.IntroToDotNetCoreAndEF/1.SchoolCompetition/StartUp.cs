namespace _1.SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StartUp
    {
        static void Main(string[] args)
        {
            var scores = new Dictionary<string, int>();
            var categories = new Dictionary<string, SortedSet<string>>();
            while (true)
            {
                var line = Console.ReadLine();

                if (line=="END")
                {
                    break;
                }

                var parts = line.Split();
                var name = parts[0];
                var category = parts[1];
                var score = int.Parse(parts[2]);

                if (!scores.ContainsKey(name))
                {
                    scores[name] = 0;
                }

                if (!categories.ContainsKey(name))
                {
                    categories[name] = new SortedSet<string>();
                }

                scores[name] += score;
                categories[name].Add(category);
            }

            var orderedStudents = scores.OrderByDescending(kvp => kvp.Value).ThenBy(kvp => kvp.Key);

            foreach (var studentKvp in orderedStudents)
            {
                var nameS = studentKvp.Key;
                var scoreS = studentKvp.Value;
                var studentCategories = categories[nameS];

                var categoriesText = $"[{string.Join(", ", studentCategories)}]";

                Console.WriteLine($"{nameS}: {scoreS} {categoriesText}");
            }
        }
    }
}
