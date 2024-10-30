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
                Console.WriteLine("\nÅterställ senaste matchen?");
                (bool restore, _ ) = ConsoleDialogs.Confirm(false);

                if (restore)
                {
                    Console.Clear();
                    Console.WriteLine("\nÅterställ tidigare match");
                    isRestore = true;
                    gameSettings = savedState.GameSettings;
                    return (gameSettings, isRestore, savedState);
                }
            }

            Console.Clear();
            Console.WriteLine("\nStarta ny match");
            gameSettings = GameSettingsManager.GetGameSettings();

            Console.WriteLine("\nÄr inställningarna korrekta?");
        
            (bool confirm, _) = ConsoleDialogs.Confirm(false);

            while (!confirm)
            {
                Console.Clear();
                gameSettings = GameSettingsManager.GetGameSettings();
                Console.WriteLine("\nÄr inställningarna korrekta?");
                (confirm, _) = ConsoleDialogs.Confirm(false);
            }

            return (gameSettings, isRestore, null);
        }


    }


}