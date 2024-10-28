namespace Scoreboard
{
    public static class GameSettingsManager
    {

        public static GameSettings GetGameSettings()
        {
            Console.WriteLine("Välj en förinställd inställning eller anpassa dina egna:");
            Console.WriteLine("1. Seniormatch");
            Console.WriteLine("2. Seniormatch slutspel");
            Console.WriteLine("3. Ungdomsmatch");
            Console.WriteLine("4. Anpassa egna inställningar");

            var key = Console.ReadKey(true);
            GameSettings settings;

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
            PrintGameSettings(settings);
            return settings;
        }

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
        private static GameSettings CustomSettings()
        {


            // Console.WriteLine("Ange periodlängd i minuter: ");
            // string gameType = Console.ReadLine();
            // Console.WriteLine("Ange periodens varaktighet i minuter:");
            // double periodDuration = double.TryParse(Console.ReadLine(), out double result) ? result : 20;


            return new GameSettings(TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(45), 3, false);
        }


        public static void PrintGameSettings(GameSettings settings)
        {
            Console.Clear();
            Console.WriteLine("Spelinställningar:");
            Console.WriteLine($"Periodlängd:            {FormatDisplayTime(settings.PeriodLength)}");
            Console.WriteLine($"Intervallängd:          {FormatDisplayTime(settings.IntermissionLength)}");
            Console.WriteLine($"Förlängning:            {FormatDisplayTime(settings.OvertimePeriodLength)}");
            Console.WriteLine($"Timeoutlängd:          {FormatDisplayTime(settings.TimeOutLength)}");
            Console.WriteLine($"Pauslängd:             {FormatDisplayTime(settings.PowerbreakLength)}");
            Console.WriteLine($"Antal perioder:        {settings.NumberOfPeriods}");
            Console.WriteLine($"Räkna ner:             {(settings.CountDown ? "Ja" : "Nej")}");
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