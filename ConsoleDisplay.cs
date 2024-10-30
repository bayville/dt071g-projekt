namespace Scoreboard
{
    public class ConsoleDisplay
    {
        private Game _game;
        private GamePenalties _gamePenalties;
        
        public ConsoleDisplay(Game game)
        {
            _game = game;
            _gamePenalties = game.GamePenalties;
            game.UpdateGame += (sender, data) => UpdateDisplay(data);
        }

        public void UpdateDisplay(GameEventArgs data)
        {
            Console.Clear();
            Console.WriteLine(data.GameMode);
            Console.WriteLine($"Period: {data.CurrentPeriod}");
            Console.WriteLine(FormatDisplayTime(data.CurrentTime));
            Console.WriteLine("\n\tRESULTAT");
            Console.WriteLine("H\t\tA");
            Console.WriteLine($"{data.HomeScore}\t\t{data.AwayScore}");

            _gamePenalties.ListAllPenalties();

            
            Console.WriteLine("\nKONTROLLER:\n");
            Console.WriteLine("Spacebar = Starta/Stoppa tid");
            Console.WriteLine("\nH - Hemma mål + | G - Borta mål + | Håll ned Shift för att minska målet");
            Console.WriteLine("\nA - Justera tid i sekunder | '-' före tiden för att dra tillbaka klockan.");
            Console.WriteLine("\nN - Ny period | Håll ned Shift för att gå tillbaka en period");
            Console.WriteLine("\nI - Aktivera Intervall-läge | T - Aktivera Timeout-läge | B - Aktivera pausläge\n\n");


            if (!data.IsRunning)
            {
                Console.WriteLine("Klockan stoppad!");
            }
        }

        private string FormatDisplayTime(TimeSpan time)
        {
            if (time.TotalMinutes >= 1)
            {
                if (time.TotalMinutes <= 9)
                {
                    return $"Tid:\n{time:m\\:ss}\n";
                }
                return $"Tid:\n{time:mm\\:ss}\n";
            }
            else
            {
                return $"Tid:\n{time:ss\\.f}\n";
            }
        }

    }
}