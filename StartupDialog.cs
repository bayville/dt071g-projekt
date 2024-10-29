namespace Scoreboard
{
    public static class StartUpDialog
    {
        public static (GameSettings gameSettings, bool isRestore, GameEventArgs? restoreData) Start(GameEventArgs? savedState)
        {
            bool isRestore = false;
            GameSettings gameSettings;

            if (savedState != null)
            {
                Console.WriteLine("Återställ senaste matchen? y/n");
                bool restore = Confirm();

                if (restore)
                {
                    Console.WriteLine("Återställ tidigare match");
                    isRestore = true;
                    gameSettings = savedState.GameSettings;
                    return (gameSettings, isRestore, savedState);
                }
            }

            Console.WriteLine("Starta ny match");
            gameSettings = GameSettingsManager.GetGameSettings();
            return (gameSettings, isRestore, null);
        }

        private static bool Confirm()
        {
            var key = Console.ReadKey(true);
            bool confirmed;
            switch (key.Key)
            {
                case ConsoleKey.Y:
                    Console.WriteLine("Ja");
                    confirmed = true;
                    break;

                case ConsoleKey.N:
                    Console.WriteLine("Nej");
                    confirmed = false;
                    break;

                default:
                    Console.WriteLine("Fel tangent, välj Y eller N");
                    Console.WriteLine(key.Key);
                    confirmed = false;
                    break;
            }
            return confirmed;
        }

    }


}