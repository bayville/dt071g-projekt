namespace Scoreboard
{
    public static class ConvertInput
    {
        
        // Gets user input and converts to int
        public static (int, bool) ConvertToInt()
        {
            
            int number = 0;
            bool success;
            
            string? input = Console.ReadLine();

            try
            {
                number = Convert.ToInt32(input);
                success = true;
            }
            catch
            {
                Console.WriteLine("Ogiltig inmatning. Försök igen!");
                Console.WriteLine("Fyll i ett giltigt heltal:");
                success = false;
            }

            return (number, success);
        
        }

        // Gets user input and converts to double
        public static (double, bool) ConvertToDouble()
        {
        
            double number = 0;
            bool success;
            
            string? input = Console.ReadLine();

            try
            {   
                // Tries to convert to double using both , and .
                success = double.TryParse(input!.Replace(',', '.'), out number) || double.TryParse(input.Replace('.', ','),  out number);
            }
            catch
            {
                Console.WriteLine("Ogiltig inmatning. Försök igen!");
                Console.WriteLine("Fyll i ett giltigt tal:");
                success = false;
            }

            return (number, success);
        }

        // Converts double to timespan (seconds)
        public static TimeSpan TimeSpanFromSeconds(double seconds){  
            return TimeSpan.FromSeconds(seconds);
        }

        // Converst double and int to timespan (Minutes and seconds)
        public static TimeSpan TimeSpanFromMinutesSeconds(int minutes, double seconds){ 
            
            TimeSpan timeSpanSeconds = TimeSpanFromSeconds(seconds);
            TimeSpan timeSpanMinutes = TimeSpan.FromMinutes(minutes);
            return timeSpanMinutes + timeSpanSeconds;

        }
    }
}