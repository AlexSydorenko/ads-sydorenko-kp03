using System.Collections.Generic;

namespace lab7
{
    class HashTable
    {
        public Item[] table;
        private double loadness;
        private int size;

        public HashTable(int capacity)
        {
            table = new Item[capacity];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new Item(i);
            }
        }

        public void InsertEntry(Key key, Value value)
        {
            int hash = GetHash(key);
            Flight flight = new Flight(key, value);
            table[hash].nodes.Add(flight);
            size++;
        }

        public bool RemoveEntry(Key key)
        {
            int hash = GetHash(key);
            Flight f = FindEntry(key);
            if (f != null)
            {
                table[hash].nodes.Remove(f);
                size--;
                return true;
            }
            return true;
        }

        public Flight FindEntry(Key key)
        {
            int hash = GetHash(key);

            foreach (Flight item in table[hash].nodes)
            {
                if (item.key.ToString() == key.ToString())
                {
                    return item;
                }
            }
            return null;
        }

        public int GetHash(Key key)
        {
            char[] arr = key.ToString().ToCharArray();
            int n = (int)arr[0] % table.Length;
            return n;
        }

        public void Rehashing(ref Item[] table)
        {
            Item[] oldTable = new Item[table.Length];
            table.CopyTo(oldTable, 0);
            table = new Item[oldTable.Length * 2];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new Item(i);
            }

            for (int i = 0; i < oldTable.Length; i++)
            {
                foreach (Flight item in oldTable[i].nodes)
                {
                    int hash = GetHash(item.key);
                    table[hash].nodes.Add(item);
                }
            }
        }

        public int GetCount()
        {
            return size;
        }

        public double CountLoadness()
        {
            loadness = size / table.Length;
            return loadness;
        }

        public void ClearHashTable()
        {
            foreach (Item item in table)
            {
                item.nodes.Clear();
            }
        }

        public bool ContainsKey(Key key)
        {
            int hash = GetHash(key);
            foreach (Flight item in table[hash].nodes)
            {
                if (item.key.ToString() == key.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public void PrintHashTable()
        {
            foreach (Item item in table)
            {
                item.PrintItems();
            }
        }
    }
}
