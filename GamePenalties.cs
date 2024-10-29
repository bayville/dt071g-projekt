namespace Scoreboard
{
    public class GamePenalties
    {
        private List<Penalty> _homePenalties;
        private List<Penalty> _awayPenalties;
        public Penalty HomePenalty1;
        public Penalty HomePenalty2;
        public Penalty AwayPenalty1;
        public Penalty AwayPenalty2;
        private const int MaxActivePenalties = 2;

        public GamePenalties()
        {
            _homePenalties = new List<Penalty>();
            _awayPenalties = new List<Penalty>();
            HomePenalty1 = new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, 0);
            HomePenalty2 = new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, 0);
            AwayPenalty1 = new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, 0);
            AwayPenalty2 = new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, 0);
        }

        public void AddNewPenalty(int playerNumber, TimeSpan penaltyLength, int team)
        {
            Penalty penalty = new Penalty(playerNumber, penaltyLength, penaltyLength, team);

            if (team == 0)
            {
                _homePenalties.Add(penalty);
            }
            else if (team == 1)
            {
                _awayPenalties.Add(penalty);
            }
            CheckActivePenalties();
        }

        public void RemoveFinishedPenalties()
        {
            List<Penalty> penalitiesToRemove = _homePenalties.Where(p => p.IsActive && p.RemainingTime <= TimeSpan.Zero).ToList();

            foreach (var penalty in penalitiesToRemove)
            {
                _homePenalties.Remove(penalty);
            }
            CheckActivePenalties();
        }

        public void SetPenaltyRemainingTime(int index, TimeSpan time)
        {
            _homePenalties[index].RemainingTime = time;
            RemoveFinishedPenalties();
        }

        public void ListAllPenalties()
        {
            Console.WriteLine("Home Penalties:");
            foreach (Penalty pen in _homePenalties)
            {
                Console.WriteLine($"{pen.PlayerNumber}\t{pen.RemainingTime}\t{pen.IsActive}\t{pen.Team}");
            }
            Console.WriteLine("---\n");
 
        }
        public void CheckActivePenalties()
        {
            List<Penalty> activePenalties = new List<Penalty>();
            
            for (int i = 0; i < _homePenalties.Count; i++)
            {
                if (i < MaxActivePenalties)
                {
                    _homePenalties[i].IsActive = true;
                    activePenalties.Add(_homePenalties[i]);
                }

            }

            for (int i = 0; i < activePenalties.Count; i++)
            {
                if (i == 0)
                {
                    HomePenalty1 = activePenalties[i];
                    HomePenalty2 = new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, 0);
                }
                if (i == 1)
                {
                    HomePenalty2 = activePenalties[i];
                }
            }

        }

    }


}