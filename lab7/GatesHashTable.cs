using System.Collections.Generic;

namespace lab7
{
    class GatesHashTable
    {
        private Item[] table;
        private int size;

        public GatesHashTable(int capacity)
        {
            table = new Item[capacity];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new Item(i);
            }
        }

        public void InsertEntry(Key key, Flight flight)
        {
            int hash = GetHash(key);
            table[hash].nodes.Add(flight);
            size++;
        }

        public bool RemoveEntry(Key flightKey)
        {
            Flight f = FindEntry(flightKey);
            int hash = GetHash(new Key(f.value.gate));
            if (f != null)
            {
                table[hash].nodes.Remove(f);
                size--;
                return true;
            }
            return false;
        }

        public Flight FindEntry(Key key)
        {   
            for (int i = 0; i < table.Length; i++)
            {
                foreach (Flight item in table[i].nodes)
                {
                    if (item.key.ToString() == key.ToString())
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public int GetHash(Key key)
        {
            char[] arr = key.ToString().ToCharArray();
            return ((int)arr[0] - 65) % table.Length;
        }

        public int GetGateCount(string gate)
        {
            int hash = GetHash(new Key(gate));
            return table[hash].nodes.Count;
        }

        public string FindFreeGate()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].nodes.Count == 0)
                {
                    char gate = (char)(i + 65);
                    return gate.ToString();
                }
            }
            return null;
        }

        public Flight GetLastFlight(string gate)
        {
            int hash = GetHash(new Key(gate));
            return table[hash].nodes[table[hash].nodes.Count - 1];
        }

        public string FindGateThatWillBeFreeTheFastest()
        {
            string gate = "";
            int minTime = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (i == 0)
                {
                    char c = (char)(i + 65);
                    gate = c.ToString();
                    minTime = table[i].FindTimeUntilFreeGate();
                    continue;
                }
                if (table[i].FindTimeUntilFreeGate() < minTime)
                {
                    char c = (char)(i + 65);
                    gate = c.ToString();
                    minTime = table[i].FindTimeUntilFreeGate();
                }
            }

            return gate;
        }

        public void ClearHashTable()
        {
            foreach (Item item in table)
            {
                item.nodes.Clear();
            }
        }

        public void PrintHashTable()
        {
            foreach (Item item in table)
            {
                item.PrintItems();
            }
        }

        public List<Flight> GetSameGateFlights(string gate)
        {
            Key key = new Key(gate);
            int hash = GetHash(key);
            return table[hash].nodes;
        }
    }
}
