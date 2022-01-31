using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        private static readonly Random Random = new Random();
        
        internal static string MakeUniqueCustomerName(HashSet<string> names)
        {
            var maxNames = (ListPrefix.Count - 2) * (ListSuffix.Count - 2);

            if (names.Count >= maxNames)
            {
                throw new InvalidOperationException("Close to maximum number of unique names");
            }
            
            var prefix = GetRandom(ListPrefix);
            var suffix = GetRandom(ListSuffix);
            var result = prefix + suffix;

            if (names.Contains(result))
            {
                return MakeUniqueCustomerName(names);
            }
            
            return prefix + suffix;
        }

        internal static string MakeCustomerEmail(string name) => $"contact@{name.ToLower()}.com";

        internal static string GetRandomState()
        {
            return GetRandom(ListState);
        }

        private static readonly List<string> ListState = new List<string>()
        {
            "AK", "AL", "AR", "CO", "DE", "FL", "HI", "IL", "KS", "ME", "MI", "MO", "NV", "NC", "PA", "UT", "VA"
        };
        
        private static string GetRandom(IList<string> items)
        {
            return items[Random.Next(items.Count)];
        }

        internal static decimal GetRandomOrderTotal()
        {
            return Random.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            var possibleSpan = end - start;
            var newSpan = new TimeSpan(0, Random.Next(0, (int)possibleSpan.TotalMinutes), 0);
            return start + newSpan;
        }
        
        internal static DateTime? GetRandomOrderCompleted(DateTime placed)
        {
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = DateTime.Now - placed;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return placed.AddDays(Random.Next(7, 14));
        }


        private static readonly List<string> ListPrefix = new List<string>()
        {
            "ABC",
            "XYZ",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Comfort",
        };
        
        private static readonly List<string> ListSuffix = new List<string>()
        {
            "Corporation",
            "Co",
            "Logistic",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planners",
            "Automotive",
            "Books",
        };
    }
}