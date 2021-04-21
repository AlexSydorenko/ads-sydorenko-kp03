using System;
using System.Collections.Generic;

namespace lab7
{
    class CommandUserInterface
    {
        public static void ProcessUsersRequests(HashTable ht_allFlights, GatesHashTable ht_byGates)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Command list:");
            Console.ResetColor();
            Console.WriteLine("[test case] - add test flights to the system");
            Console.WriteLine("[add] - add new flight to the system");
            Console.WriteLine("[delete] - remove flight by key");
            Console.WriteLine("[find] - find flight by the key");
            Console.WriteLine("[print all flights] - get information about all flights in the airport");
            Console.WriteLine("[print gate] - get information about flights that depart from the same gate");
            Console.WriteLine("[clear] - deletete all flights from the system");
            Console.WriteLine("[exit]");

            string command = "";
            do
            {
                Console.Write("Enter command: ");
                command = Console.ReadLine();
                Console.WriteLine();

                if (command == "test case")
                {
                    ProcessTestCase(ht_allFlights, ht_byGates);
                }
                else if (command == "add")
                {
                    ProcessAdd(ht_allFlights, ht_byGates);
                }
                else if (command == "delete")
                {
                    ProcessDelete(ht_allFlights, ht_byGates);
                }
                else if (command == "find")
                {
                    ProcessFind(ht_allFlights);
                }
                else if (command == "print all flights")
                {
                    ProcessPrintAllFlights(ht_allFlights);
                }
                else if (command == "print gate")
                {
                    ProcessPrintGate(ht_byGates);
                }
                else if (command == "clear")
                {
                    ProcessClear(ht_allFlights, ht_byGates);
                }
                else if (command == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Bye!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Incorrect command");
                    Console.ResetColor();
                }
            }
            while (command != "exit");
        }

        private static void ProcessTestCase(HashTable ht_allFlights, GatesHashTable ht_byGates)
        {
            List<Flight> flights = new List<Flight>(){
                new Flight(new Key("AM2021"), new Value("London", "C", new TimeDate(2021, "April", 24, 900))),
                new Flight(new Key("AM2054"), new Value("Rome", "D", new TimeDate(2021, "April", 24, 990))),
                new Flight(new Key("BD2021"), new Value("Dnipro", "B", new TimeDate(2021, "April", 24, 870))),
                new Flight(new Key("KA0000"), new Value("Monaco", "A", new TimeDate(2021, "April", 24, 840))),
                new Flight(new Key("MN1234"), new Value("Chicago", "B", new TimeDate(2021, "April", 24, 720))),

                new Flight(new Key("NI9021"), new Value("Odessa", "A", new TimeDate(2021, "May", 4, 60))),
                new Flight(new Key("PM2677"), new Value("Paris", "D", new TimeDate(2021, "April", 24, 1030))),
                new Flight(new Key("EL1983"), new Value("Dortmund", "B", new TimeDate(2021, "April", 26, 1243))),
                new Flight(new Key("KP1001"), new Value("Liverpool", "D", new TimeDate(2021, "April", 25, 800))),
                new Flight(new Key("OP4234"), new Value("Moscow", "D", new TimeDate(2021, "April", 24, 900)))
            };

            foreach (Flight f in flights)
            {
                if (ht_byGates.GetGateCount(f.value.gate) == 0)
                {
                    ht_allFlights.InsertEntry(f.key, f.value);
                    ht_byGates.InsertEntry(new Key(f.value.gate), f);
                    if (ht_allFlights.CountLoadness() == 1)
                    {
                        ht_allFlights.Rehashing(ref ht_allFlights.table);
                        Console.WriteLine("Rehashing was completed!");
                    }
                    continue;
                }

                Flight lastFlight = ht_byGates.GetLastFlight(f.value.gate);
                if ((f.value.departureTime.CountTimeToDepartInMinutes(f.value.isDelayed) -
                    lastFlight.value.departureTime.CountTimeToDepartInMinutes(lastFlight.value.isDelayed)) < 105)
                {
                    if (ht_byGates.FindFreeGate() != null)
                    {
                        f.value.gate = ht_byGates.FindFreeGate();
                        Console.WriteLine($"Gate of the flight with key `{f.key.ToString()}` was changed to {f.value.gate}!");

                        ht_allFlights.InsertEntry(f.key, f.value);
                        ht_byGates.InsertEntry(new Key(f.value.gate), f);
                        if (ht_allFlights.CountLoadness() == 1)
                        {
                            ht_allFlights.Rehashing(ref ht_allFlights.table);
                            Console.WriteLine("Rehashing was completed!");
                        }
                        continue;
                    }

                    // ищем гейт, который освободится быстрее
                    if (ht_byGates.FindGateThatWillBeFreeTheFastest() != f.value.gate)
                    {
                        f.value.gate = ht_byGates.FindGateThatWillBeFreeTheFastest();
                        Console.WriteLine($"Gate of the flight with key `{f.key.ToString()}` was changed to {f.value.gate}!");
                    }
                    
                    // определяем, на сколько задержится рейс
                    Flight lastFlightInNewGate = ht_byGates.GetLastFlight(f.value.gate);
                    int timeDifference = (f.value.departureTime.CountTimeToDepartInMinutes(f.value.isDelayed) -
                        lastFlightInNewGate.value.departureTime.CountTimeToDepartInMinutes(lastFlightInNewGate.value.isDelayed));
                    if (timeDifference < 0)
                    {
                        timeDifference = Math.Abs(timeDifference);
                        f.value.isDelayed = timeDifference + 105;
                    }
                    else if (timeDifference < 105 && timeDifference >= 0)
                    {
                        f.value.isDelayed = 105 - timeDifference;
                    }

                    ht_allFlights.InsertEntry(f.key, f.value);
                    ht_byGates.InsertEntry(new Key(f.value.gate), f);
                    if (ht_allFlights.CountLoadness() == 1)
                    {
                        ht_allFlights.Rehashing(ref ht_allFlights.table);
                        Console.WriteLine("Rehashing was completed!");
                    }
                    continue;
                }


                ht_allFlights.InsertEntry(f.key, f.value);
                ht_byGates.InsertEntry(new Key(f.value.gate), f);
                if (ht_allFlights.CountLoadness() == 1)
                {
                    ht_allFlights.Rehashing(ref ht_allFlights.table);
                    Console.WriteLine("Rehashing was completed!");
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Hash-table was successfully filled with test flights!");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void ProcessAdd(HashTable ht_allFlights, GatesHashTable ht_byGates)
        {
            Console.Write("Enter key of the flight (format - AA0000): ");
            string key = Console.ReadLine().ToUpper();
            if (!IsCorrectKey(key))
            {
                return;
            }
            if (ht_allFlights.ContainsKey(new Key(key)))
            {
                Console.WriteLine("Flight with such key is already exists in the system! Try again!");
                return;
            }

            Console.Write("Enter aeroport of arrival: ");
            string aeroportOfArrival = Console.ReadLine();

            Console.Write("Enter gate: ");
            string gate = Console.ReadLine();
            gate = gate.ToUpper();
            if (gate != "A" && gate != "B" && gate != "C" && gate != "D")
            {
                Console.WriteLine("Only 4 gates in our airport: A, B, C and D! Try again!");
                return;
            }

            Console.WriteLine("Now enter departure time!");
            Console.Write("Year: ");
            int year = 0;
            if (int.TryParse(Console.ReadLine(), out year))
            {
                if (year.ToString().Length != 4)
                {
                    Console.WriteLine("Year is 4 integers! Try again!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Year is 4 integers! Try again!");
                return;
            }

            Console.Write("Month: ");
            string month = Console.ReadLine();            
            if (!IsCorrectMonth(month))
            {
                Console.WriteLine("You entered non-existing month! Try again!");
                return;
            }

            Console.Write("Day: ");
            int day = 0;
            if (int.TryParse(Console.ReadLine(), out day))
            {
                int maxNumOfDays = IsCorrectDay(month, day);
                if (maxNumOfDays == -1)
                {
                    Console.WriteLine("Day is integer from 1 to 31! Try again!");
                    return;
                }
                if (maxNumOfDays != 1)
                {
                    Console.WriteLine($"`{maxNumOfDays}` days in `{month}`! Try again!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Day is integer from 1 to 31! Try again!");
                return;
            }

            Console.Write("Time (in format HH:MM): ");
            string time = Console.ReadLine();
            if (time.Length != 5 || time[2] != ':')
            {
                Console.WriteLine("Entered time doesn't match the specified format HH:MM! Try again!");
                return;
            }
            int hours = 0;
            int minutes = 0;
            if (int.TryParse(time.Substring(0, 2), out hours) && int.TryParse(time.Substring(3, 2), out minutes))
            {
                if (hours > 23 || hours < 1 || minutes > 59 || minutes < 0)
                {
                    Console.WriteLine("Hours or minutes are out of range! Hours from 1 to 23, minutes from 0 to 59! Try again!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Hours and minutes are only integers! Try again!");
                return;
            }
            int totalMinutes = hours * 60 + minutes;

            TimeDate departureTime = new TimeDate(year, month, day, totalMinutes);
            if (!IsCorrectTime(DateTime.Now, departureTime))
            {
                Console.WriteLine("Incorrect departure time! Try again!");
                return;
            }
            Flight flight = new Flight(new Key(key), new Value(aeroportOfArrival, gate, departureTime));


            if (ht_byGates.GetGateCount(flight.value.gate) == 0)
            {
                ht_allFlights.InsertEntry(flight.key, flight.value);
                ht_byGates.InsertEntry(new Key(flight.value.gate), flight);
                Console.WriteLine($"Flight with {key} was successfully added to the system!");
                if (ht_allFlights.CountLoadness() == 1)
                {
                    ht_allFlights.Rehashing(ref ht_allFlights.table);
                    Console.WriteLine("Rehashing was completed!");
                }
                return;
            }

            Flight lastFlight = ht_byGates.GetLastFlight(flight.value.gate);
            if ((flight.value.departureTime.CountTimeToDepartInMinutes(flight.value.isDelayed) -
                lastFlight.value.departureTime.CountTimeToDepartInMinutes(lastFlight.value.isDelayed)) < 105)
            {
                if (ht_byGates.FindFreeGate() != null)
                {
                    flight.value.gate = ht_byGates.FindFreeGate();
                    Console.WriteLine($"Gate of the flight with key `{flight.key.ToString()}` was changed to {flight.value.gate}!");

                    ht_allFlights.InsertEntry(flight.key, flight.value);
                    ht_byGates.InsertEntry(new Key(flight.value.gate), flight);
                    Console.WriteLine($"Flight with {key} was successfully added to the system!");
                    if (ht_allFlights.CountLoadness() == 1)
                    {
                        ht_allFlights.Rehashing(ref ht_allFlights.table);
                        Console.WriteLine("Rehashing was completed!");
                    }
                    return;
                }

                // ищем гейт, который освободится быстрее
                flight.value.gate = ht_byGates.FindGateThatWillBeFreeTheFastest();
                Console.WriteLine($"Gate of the flight with key `{flight.key.ToString()}` was changed to {flight.value.gate}!");

                // определяем, на сколько задержится рейс
                Flight lastFlightInNewGate = ht_byGates.GetLastFlight(flight.value.gate);
                int timeDifference = (flight.value.departureTime.CountTimeToDepartInMinutes(flight.value.isDelayed) -
                    lastFlightInNewGate.value.departureTime.CountTimeToDepartInMinutes(lastFlightInNewGate.value.isDelayed));
                if (timeDifference < 0)
                {
                    timeDifference = Math.Abs(timeDifference);
                    flight.value.isDelayed = timeDifference + 105;
                }
                else if (timeDifference < 105 && timeDifference >= 0)
                {
                    flight.value.isDelayed = 105 - timeDifference;
                }

                ht_allFlights.InsertEntry(flight.key, flight.value);
                ht_byGates.InsertEntry(new Key(flight.value.gate), flight);
                Console.WriteLine($"Flight with {key} was successfully added to the system!");
                if (ht_allFlights.CountLoadness() == 1)
                {
                    ht_allFlights.Rehashing(ref ht_allFlights.table);
                    Console.WriteLine("Rehashing was completed!");
                }
                return;
            }


            ht_allFlights.InsertEntry(flight.key, flight.value);
            ht_byGates.InsertEntry(new Key(flight.value.gate), flight);

            Console.WriteLine($"Flight with {key} was successfully added to the system!");

            if (ht_allFlights.CountLoadness() == 1)
            {
                ht_allFlights.Rehashing(ref ht_allFlights.table);
                Console.WriteLine("Rehashing was completed!");
            }
        }

        private static void ProcessDelete(HashTable ht_allFlights, GatesHashTable ht_byGates)
        {
            Console.Write("Enter key of the flight, which want to be deleted (format - AA0000): ");
            string key = Console.ReadLine().ToUpper();
            if (!IsCorrectKey(key))
            {
                return;
            }

            if (ht_allFlights.ContainsKey(new Key(key)))
            {
                ht_allFlights.RemoveEntry(new Key(key));
                ht_byGates.RemoveEntry(new Key(key));

                Console.WriteLine($"Flight with key {key} was successfully removed from the system!");
            }
            else
            {
                Console.WriteLine("No flight with such key in the system!");
            }
        }

        private static void ProcessFind(HashTable ht_allFlights)
        {
            Console.Write("Enter key of the flight to watch it's information (format - AA0000): ");
            string key = Console.ReadLine().ToUpper();
            if (!IsCorrectKey(key))
            {
                return;
            }

            if (ht_allFlights.ContainsKey(new Key(key)))
            {
                Flight f = ht_allFlights.FindEntry(new Key(key));
                Console.WriteLine(f.ToString());
            }
            else
            {
                Console.WriteLine("No flight with such code in the system! Try again!");
            }
        }

        private static void ProcessPrintAllFlights(HashTable ht_allFlights)
        {
            if (ht_allFlights.GetCount() != 0)
            {
                ht_allFlights.PrintHashTable();
            }
            else
            {
                Console.WriteLine("There are no flights in the system!");
            }
        }

        private static void ProcessPrintGate(GatesHashTable ht_byGates)
        {
            Console.Write("Enter gate: ");
            string gate = Console.ReadLine().ToUpper();
            if (gate != "A" && gate != "B" && gate != "C" && gate != "D")
            {
                Console.WriteLine("Only 4 gates in our airport: A, B, C and D! Try again!");
                return;
            }

            if (ht_byGates.GetGateCount(gate) != 0)
            {
                List<Flight> sameGate = ht_byGates.GetSameGateFlights(gate);
                Console.WriteLine($"List of flights that depart from the gate {gate}:");
                Console.WriteLine();
                foreach (Flight item in sameGate)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine($"There are no flights that depart from gate {gate}!");
            }
        }

        private static void ProcessClear(HashTable ht_allFlights, GatesHashTable ht_byGates)
        {
            ht_allFlights.ClearHashTable();
            ht_byGates.ClearHashTable();

            Console.WriteLine("All flights was successfully deleted from the system!");
        }

        private static bool IsCorrectKey(string key)
        {
            if (key.Length != 6)
            {
                Console.WriteLine($"Key length should be `6`, but have `{key.Length}`!");
                return false;
            }
            if (!char.IsLetter(key[0]) || !char.IsLetter(key[1]))
            {
                Console.WriteLine("First two digits of the key are only letters! Try again!");
                return false;
            }
            if (!char.IsDigit(key[2]) || !char.IsDigit(key[3]) || !char.IsDigit(key[4]) || !char.IsDigit(key[5]))
            {
                Console.WriteLine("4 numbers should be after letters in the key! Try again!");
                return false;
            }
            return true;
        }

        private static bool IsCorrectMonth(string str)
        {
            if (str != "January" && str != "February" && str != "March" && str != "April" && str != "May" && str != "June"
            && str != "July" && str != "August" && str != "September" && str != "October" && str != "November" && str != "December")
            {
                return false;
            }
            return true;
        }

        private static int IsCorrectDay(string month, int day)
        {
            if (day < 1 || day > 31)
            {
                return -1;
            }
            if ((month == "January" || month == "March" || month == "May" || month == "July" || month == "August" || month == "October" || month == "December") && day > 31)
            {
                return 31;
            }
            else if ((month == "April" || month == "June" || month == "September" || month == "November") && day > 30)
            {
                return 30;
            }
            else if (day > 28)
            {
                return 28;
            }
            return 1;
        }

        private static bool IsCorrectTime(DateTime currentTime, TimeDate departureTime)
        {
            int timeNow = currentTime.Year * 525600 + currentTime.Month * 43200 + currentTime.Day * 1440 + currentTime.Hour * 60 + currentTime.Minute;
            int depTime = departureTime.CountTimeToDepartInMinutes();
            if (depTime - timeNow < 1)
            {
                return false;
            }
            return true;
        }
    }
}
