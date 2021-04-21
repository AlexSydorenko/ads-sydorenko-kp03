using System;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable ht_allFlights = new HashTable(8);
            GatesHashTable ht_byGates = new GatesHashTable(4);
            CommandUserInterface.ProcessUsersRequests();
        }
    }

    class Flight
    {
        public Key key;
        public Value value;
        
        public Flight(Key key, Value value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return string.Format($"{key.ToString()}:\n{value.ToString()}");
        }
    }

    class Key
    {
        private string flightCode;

        public Key(string code)
        {
            this.flightCode = code;
        }

        public override string ToString()
        {
            return string.Format(this.flightCode);
        }
    }

    class Value
    {
        private string aeroportOfArrival;
        public string gate;
        public TimeDate departureTime;
        public int isDelayed;

        public Value(string aeroportOfArrival, string gate, TimeDate departureTime)
        {
            this.aeroportOfArrival = aeroportOfArrival;
            this.gate = gate;
            this.departureTime = departureTime;
            this.isDelayed = 0;
        }

        public override string ToString()
        {
            int hours = this.isDelayed / 60;
            int minutes = this.isDelayed % 60;
            string delayTime = "";
            if (minutes < 10)
            {
                delayTime = $"{hours}:0{minutes}";
            }
            else
            {
                delayTime = $"{hours}:{minutes}";
            }
            return string.Format($"Aeroport of arrival: {this.aeroportOfArrival}\n"
                + $"Gate: {this.gate}\n"
                + $"Departure time: {this.departureTime.ToString()}\n"
                + $"Delay: {delayTime}\n");
        }
    }
}
