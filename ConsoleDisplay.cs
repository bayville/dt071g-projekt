namespace Scoreboard
{
    public class ConsoleDisplay
    {
        private GamePenalties _gamePenalties;

        private GameEventArgs? _data;
        public ConsoleDisplay(Game game)
        {
            _gamePenalties = game.GamePenalties;
            game.UpdateGame += (sender, data) => OnUpdate(data);
        }

        private void OnUpdate(GameEventArgs data)
        {
            _data = data;
            UpdateDisplay();
        }
        public void UpdateDisplay()
        {
            if(_data != null)
            {
                Console.Clear();
                DisplayHeader("MATCHINFO");
                DisplayGameInfo(_data);
                DisplayScore(_data);
                DisplayGameStatus(_data);

                DisplayHeader("UTVISNINGAR");
                _gamePenalties.ListAllPenalties();

                // DisplayHeader("KONTROLLER");
                // DisplayControls(_data.IsRunning);
            }

        }


        private void DisplayHeader(string header)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n============== {header} ==============\n");
            Console.ResetColor();
        }
        private void DisplayGameInfo(GameEventArgs data)
        {
            Console.WriteLine($"\n{data.GameMode}");
            Console.WriteLine($"Period: {data.CurrentPeriod}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{FormatOutput.FormatGameTime(data.CurrentTime)}");
            Console.ResetColor();
        }

        private void DisplayScore(GameEventArgs data)
        {
            Console.WriteLine("\nHEMMA   BORTA");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {data.HomeScore}       {data.AwayScore}");
            Console.ResetColor();

        }
        private void DisplayGameStatus(GameEventArgs data)
        {
            if (!data.IsRunning)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nKLOCKAN STOPPAD");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nKLOCKAN KÖRS");
                Console.ResetColor();
            }
        }

        private void DisplayControls(bool isRunning)
        {

            Console.WriteLine("SPACEBAR\t- Starta/Stoppa tid");
            Console.WriteLine("H / H+SHIFT\t- Öka/Minska hemmamål");
            Console.WriteLine("G / G+SHIFT\t- Öka/Minska bortamål\n");


            if (!isRunning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nTID:");
                Console.ResetColor();

                Console.WriteLine("A\t\t- Justera tid i sekunder");
                Console.WriteLine("S\t\t- Ändra matchtid");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nUTVISNINGAR:");
                Console.ResetColor();

                Console.WriteLine("U\t\t- Lägg till utvisning");
                Console.WriteLine("R\t\t- Ta bort utvisning");
                Console.WriteLine("E\t\t- Ändra utvisningstid");
                Console.WriteLine("M\t\t- Flytta utvisning till toppen");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPERIOD / TIMERS:");
                Console.ResetColor();

                Console.WriteLine("N / N+SHIFT\t- Nästa/Föregående period");
                Console.WriteLine("I\t\t- Paus-läge");
                Console.WriteLine("T\t\t- Timeout-läge");
                Console.WriteLine("P\t\t- Pausläge");


                Console.WriteLine("\n\nQ\t\t- Avsluta programmet");

            }

            Console.WriteLine();
        }
    }
}