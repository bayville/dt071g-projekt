namespace Scoreboard
{
    public static class GameSettingsManager
    {
        private static bool success = false;
        private static int numberOfPeriods;



        public static GameSettings GetGameSettings()
        {   
            GameSettings settings;

            // Display menu
            Console.WriteLine("\n\nVälj en förinställd inställning eller anpassa dina egna:");
            Console.WriteLine("1. Seniormatch");
            Console.WriteLine("2. Seniormatch slutspel");
            Console.WriteLine("3. Ungdomsmatch");
            Console.WriteLine("4. Anpassa egna inställningar");
            

            // Read input
            var key = Console.ReadKey(true);

            // Get settings or configure your own
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    settings = GetPreConfiguredSettings(1);
                    break;
                case ConsoleKey.D2:
                    settings = GetPreConfiguredSettings(2);
                    break;
                case ConsoleKey.D3:
                    settings = GetPreConfiguredSettings(3);
                    break;
                case ConsoleKey.D4:
                    settings = CustomSettings();
                    break;
                default:
                    Console.WriteLine("Felaktigt val, anpassa egna inställningar: .");
                    settings = CustomSettings();
                    break;
            }
            
            // Prints selected game settings
            PrintGameSettings(settings);
            
    
            return settings;
        }

        // Preconfigured gamesettings
        private static GameSettings GetPreConfiguredSettings(int setting)
        {
            switch (setting)
            {
                case 1:
                    return new GameSettings(TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(18), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45), 3, true);
                case 2:
                    return new GameSettings(TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(18), TimeSpan.FromMinutes(20), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45), 3, true);
                case 3:
                    return new GameSettings(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45), 3, false);
                default:
                    return CustomSettings();
            }
        }

        // Dialog-tree to get custom settings from user
        private static GameSettings CustomSettings()
        {
            Console.Clear();
            Console.WriteLine("\nAnge periodlängd");
            TimeSpan periodLength = ConsoleDialogs.TimeSpanFromMinutesSeconds();
            Console.WriteLine("\nAnge pauslängd");
            TimeSpan intermissionLength = ConsoleDialogs.TimeSpanFromMinutesSeconds();
            Console.WriteLine("\nAnge överttidslängd");
            TimeSpan overtimePeriodLength = ConsoleDialogs.TimeSpanFromMinutesSeconds();
            Console.WriteLine("\nAnge timeoutlängd");
            TimeSpan timeoutLength = ConsoleDialogs.TimeSpanFromSeconds();
            Console.WriteLine("\nAnge powerbreaklängd");
            TimeSpan powerbreakLength = ConsoleDialogs.TimeSpanFromSeconds();
            
            
            while (!success)
            {
                Console.WriteLine("\nAnge antal perioder för ordinarie speltid");
                (numberOfPeriods, success) = ConvertInput.ConvertToInt();
            }

            Console.WriteLine("\nSka klockan räkna ner?");
            (bool countDown, _) = ConsoleDialogs.Confirm(false);
            
            // Return gamesettings
            return new GameSettings(periodLength, intermissionLength, overtimePeriodLength, timeoutLength, powerbreakLength, numberOfPeriods, countDown);
        }


        // Prints gamesettings
        public static void PrintGameSettings(GameSettings settings)
        {
            Console.Clear();
            Console.WriteLine("Spelinställningar:");
            Console.WriteLine($"Periodlängd:            {FormatDisplayTime(settings.PeriodLength)}");
            Console.WriteLine($"Pauslängd:              {FormatDisplayTime(settings.IntermissionLength)}");
            Console.WriteLine($"Förlängning:            {FormatDisplayTime(settings.OvertimePeriodLength)}");
            Console.WriteLine($"Timeoutlängd:           {FormatDisplayTime(settings.TimeOutLength)}");
            Console.WriteLine($"Pauslängd:              {FormatDisplayTime(settings.PowerbreakLength)}");
            Console.WriteLine($"Antal perioder:         {settings.NumberOfPeriods}");
            Console.WriteLine($"Räkna ner:              {(settings.CountDown ? "Ja" : "Nej")}");
        }



        private static string FormatDisplayTime(TimeSpan time)
        {
            if (time.TotalMinutes >= 1)
            {
                return $"{time:mm\\:ss}";
            }
            else
            {
                return $"{time:ss\\s}";
            }
        }
    }


}