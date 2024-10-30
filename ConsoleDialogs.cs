namespace Scoreboard
{
    public static class ConsoleDialogs
    {
        public static (bool, bool) Confirm(bool canCancel)
        {
            bool correctKey = false;
            bool confirmed = false;
            bool cancel = false;

            while (!correctKey)
            {

                Console.WriteLine("\nY = Ja");
                Console.WriteLine("N = Nej");

                if (canCancel)
                {
                    Console.WriteLine("X = Avbryt");
                }


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
            return (confirmed, cancel);
        }

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
            return (team, cancel);
        }

        public static (TimeSpan, bool) ChoosePenaltyTime()
        {
            bool correctKey = false;
            bool cancel = false;
            TimeSpan time = TimeSpan.Zero;

            while (!correctKey)
            {
                Console.WriteLine("2 = 02:00 (Två minuter)");
                Console.WriteLine("4 = 04:00 (Fyra minuter)");
                Console.WriteLine("5 = 05:00 (Fem minuter)");
                Console.WriteLine("0 = Fyll i tid själv");
                Console.WriteLine("X = Avbryt");

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
            return (time, cancel);
        }

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


        public static TimeSpan TimeSpanFromSeconds()
        {
            double seconds;

            Console.WriteLine("\nAnge sekunder:");

            while (true)
            {
                (seconds, bool success) = ConvertInput.ConvertToDouble();

                if (success == true)
                {
                    break;
                }
            }

            return ConvertInput.TimeSpanFromSeconds(seconds);
        }

        public static TimeSpan TimeSpanFromMinutesSeconds()
        {
            double seconds;
            int minutes;

            while (true)
            {
                Console.WriteLine("\nAnge minuter:");
                (minutes, bool minutesSuccess) = ConvertInput.ConvertToInt();

                if (minutesSuccess)
                    break;
            }

            while (true)
            {
                Console.WriteLine("\nAnge sekunder:");
                (seconds, bool secondsSuccess) = ConvertInput.ConvertToDouble();

                if (secondsSuccess)
                    break;
            }

            return ConvertInput.TimeSpanFromMinutesSeconds(minutes, seconds);
        }

    }
}