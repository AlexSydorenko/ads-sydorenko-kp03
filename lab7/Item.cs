using System;
using System.Collections.Generic;

namespace lab7
{
    class Item
    {
        public int key;
        public List<Flight> nodes;

        public Item(int key)
        {
            this.key = key;
            nodes = new List<Flight>();
        }

        public void PrintItems()
        {
            foreach (var item in nodes)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine();
            }
        }

        public int FindTimeUntilFreeGate()
        {
            int timeWhenDepartsTheLastFlight = nodes[nodes.Count - 1].value.departureTime.CountTimeToDepartInMinutes(nodes[nodes.Count - 1].value.isDelayed);
            int currentTime = 0;
            DateTime dt = DateTime.Now;
            currentTime += dt.Year * 525600 + dt.Month * 43200 + dt.Day * 1440 + dt.Hour * 60 + dt.Minute;
            return timeWhenDepartsTheLastFlight - currentTime;
        }

        public bool FindFlight(Key key)
        {
            foreach (var item in nodes)
            {
                if (item.key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public Flight GetLastFlight()
        {
            return nodes[nodes.Count - 1];
        }
    }
}
