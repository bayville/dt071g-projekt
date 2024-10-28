namespace Scoreboard
{
    public class ConsoleDisplay
    {
        public ConsoleDisplay(Game game)
        {
            game.UpdateGame += (sender, data) => UpdateDisplay(data);
        }

        public void UpdateDisplay(GameEventArgs data)
        {
            Console.Clear();
            Console.WriteLine(data.GameClock.activeTimer.Mode);
            Console.WriteLine($"Period: {data.GamePeriod.CurrentPeriod}");
            Console.WriteLine(FormatDisplayTime(data.GameClock.activeTimer.CurrentTime));
            Console.WriteLine("\n\tRESULTAT");
            Console.WriteLine("H\t\tA");
            Console.WriteLine($"{data.GameScore.HomeScore}\t\t{data.GameScore.AwayScore}");
            Console.WriteLine("\nKONTROLLER:\n");
            Console.WriteLine("S - Start | P - Paus");
            Console.WriteLine("\nH - Hemma mål + | G - Borta mål + | Håll ned Shift för att minska målet");
            Console.WriteLine("\nA - Justera tid i sekunder | '-' före tiden för att justera tiden med negativt värde.");
            Console.WriteLine("\nN - Ny period | Håll ned Shift för att gå tillbaka en period");
            Console.WriteLine("\nI - Aktivera Intervall-läge | T - Aktivera Timeout-läge | B - Aktivera pausläge\n\n");


            if (!data.GameClock.activeTimer.IsRunning)
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