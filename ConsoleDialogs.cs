namespace Scoreboard
{
    public static class ConsoleDialogs
    {
        // Dialog to get yes/no
        public static (bool, bool) Confirm(bool canCancel)
        {
            bool correctKey = false;
            bool confirmed = false;
            bool cancel = false;

            // Loops until y/n or x to cancel is pressed
            while (!correctKey)
            {

                Console.WriteLine("\nY = Ja");
                Console.WriteLine("N = Nej");

                if (canCancel)
                {
                    Console.WriteLine("X = Avbryt");
                }


                // Reads input
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        Console.WriteLine("\nJa\n");
                        confirmed = true;
                        correctKey = true;
                        break;

                    case ConsoleKey.N:
                        Console.WriteLine("\nNej\n");
                        correctKey = true;
                        break;

                    case ConsoleKey.X:
                        if (canCancel)
                        {
                            Console.WriteLine("Avbryt");
                            correctKey = true;
                            cancel = true;
                        }
                        break;

                }
            }

            // Returns a tuple 
            return (confirmed, cancel);
        }


        // Dialog to choose home/away team
        public static (int, bool) ChooseTeam()
        {
            Console.WriteLine("Välj lag:\n");
            bool correctKey = false;
            bool cancel = false;
            int team = 2;

            while (!correctKey)
            {
                Console.WriteLine("H = Hemmalag");
                Console.WriteLine("G = Bortalag");
                Console.WriteLine("X = Avbryt");


                // Reads input
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.H:
                        Console.WriteLine("\nHemmalag\n");
                        correctKey = true;
                        team = 0;
                        break;

                    case ConsoleKey.G:
                        Console.WriteLine("\nBortalag\n");
                        correctKey = true;
                        team = 1;
                        break;

                    case ConsoleKey.X:
                        Console.WriteLine("Avbryt");
                        correctKey = true;
                        cancel = true;
                        break;

                }
            }

            // Returns tuple
            return (team, cancel);
        }


        // Dialog to choose penalty time
        public static (TimeSpan, bool) ChoosePenaltyTime()
        {
            bool correctKey = false;
            bool cancel = false;
            TimeSpan time = TimeSpan.Zero;

            while (!correctKey)
            {
                Console.WriteLine("\n2 = 02:00 (Två minuter)");
                Console.WriteLine("4 = 04:00 (Fyra minuter)");
                Console.WriteLine("5 = 05:00 (Fem minuter)");
                Console.WriteLine("0 = Fyll i tid själv");
                Console.WriteLine("X = Avbryt\n");
                

                // Reads input
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D2:
                        Console.WriteLine("\n02:00\n");
                        time = TimeSpan.FromMinutes(2);
                        correctKey = true;
                        break;

                    case ConsoleKey.D4:
                        Console.WriteLine("\n04:00\n");
                        time = TimeSpan.FromMinutes(4);
                        correctKey = true;
                        break;

                    case ConsoleKey.D5:
                        Console.WriteLine("\n05:00\n");
                        time = TimeSpan.FromMinutes(5);
                        correctKey = true;
                        break;

                    case ConsoleKey.D0:
                        Console.WriteLine("\nFyll i tid själv\n");

                        time = TimeSpanFromMinutesSeconds();
                        correctKey = true;
                        break;

                    case ConsoleKey.X:
                        Console.WriteLine("Avbryt");
                        correctKey = true;
                        cancel = true;
                        break;
                }
            }

            // Returns tuple
            return (time, cancel);
        }

        // Dialog to set playernumber
        public static int SetPlayerNumber()
        {
            int playerNumber = 0;
            bool success = false;

            Console.WriteLine("Ange spelarnummer:\n");

            while (!success)
            {
                (playerNumber, success) = ConvertInput.ConvertToInt();
            }

            return playerNumber;
        }

        // Get input from user and return a timespan in seconds
        public static TimeSpan TimeSpanFromSeconds()
        {
            double seconds;

            Console.WriteLine("\nAnge sekunder:");

            while (true)
            {
                // converts input to double
                (seconds, bool success) = ConvertInput.ConvertToDouble();

                if (success == true)
                {
                    break;
                }
            }

            // Returns timespan
            return ConvertInput.TimeSpanFromSeconds(seconds);
        }

        // Get input from user and return a timespan in minutes and seconds
        public static TimeSpan TimeSpanFromMinutesSeconds()
        {
            double seconds;
            int minutes;

            while (true)
            {
                Console.WriteLine("\nAnge minuter:");
                 // converts input to int
                (minutes, bool minutesSuccess) = ConvertInput.ConvertToInt();

                if (minutesSuccess)
                    break;
            }

            while (true)
            {
                Console.WriteLine("\nAnge sekunder:");
                 // converts input to double
                (seconds, bool secondsSuccess) = ConvertInput.ConvertToDouble();

                if (secondsSuccess)
                    break;
            }

            // Returns timespan with minutes and seconds
            return ConvertInput.TimeSpanFromMinutesSeconds(minutes, seconds);
        }

    }
}