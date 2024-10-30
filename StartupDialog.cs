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
                Console.WriteLine("\nÅterställ senaste matchen? y/n");
                bool restore = Confirm();

                if (restore)
                {
                    Console.WriteLine("\nÅterställ tidigare match");
                    isRestore = true;
                    gameSettings = savedState.GameSettings;
                    return (gameSettings, isRestore, savedState);
                }
            }

            Console.WriteLine("\nStarta ny match");
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
                    Console.WriteLine("\nJa");
                    confirmed = true;
                    break;

                case ConsoleKey.N:
                    Console.WriteLine("\nNej");
                    confirmed = false;
                    break;

                default:
                    Console.WriteLine("\nFel tangent, välj Y eller N");
                    Console.WriteLine(key.Key);
                    confirmed = false;
                    break;
            }
            return confirmed;
        }

    }


}