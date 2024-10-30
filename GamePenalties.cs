namespace Scoreboard
{
    public class GamePenalties
    {
        public List<Penalty> _homePenalties { get; private set; }
        public List<Penalty> _awayPenalties { get; private set; }
        public Penalty HomePenalty1 { get; private set; }
        public Penalty HomePenalty2 { get; private set; }
        public Penalty AwayPenalty1 { get; private set; }
        public Penalty AwayPenalty2 { get; private set; }
        private const int MaxActivePenalties = 2;

        public GamePenalties()
        {
            _homePenalties = new List<Penalty>();
            _awayPenalties = new List<Penalty>();
            HomePenalty1 = CreateEmptyPenalty(0);
            HomePenalty2 = CreateEmptyPenalty(0);
            AwayPenalty1 = CreateEmptyPenalty(1);
            AwayPenalty2 = CreateEmptyPenalty(1);
        }

        private static Penalty CreateEmptyPenalty(int team)
        {
            return new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, team);
        }

        public void AddNewPenalty(int playerNumber, TimeSpan penaltyLength, int team)
        {
            Penalty penalty = new Penalty(playerNumber, penaltyLength, penaltyLength, team);
            GetPenaltyList(team).Add(penalty);
            Update();
        }

        private List<Penalty> GetPenaltyList(int team)
        {
            if (team == 0)
            {
                return _homePenalties;
            }
            else
            {
                return _awayPenalties;
            }
        }

        public void RestorePenaltyLists(List<Penalty> homePenalties, List<Penalty> awayPenalaties)
        {
            _homePenalties = homePenalties;
            _awayPenalties = awayPenalaties;
            Console.WriteLine("I restorePenaltyList");

            Console.WriteLine("Home Penalties:");
            foreach (Penalty pen in _homePenalties)
            {
                Console.WriteLine($"{pen.PlayerNumber}\t{pen.RemainingTime}\t{pen.IsActive}\t{pen.Team}");
            }

            Console.WriteLine("\n---\n");

            Console.WriteLine("Away penalties");
            foreach (Penalty pen in _awayPenalties)
            {
                Console.WriteLine($"{pen.PlayerNumber}\t{pen.RemainingTime}\t{pen.IsActive}\t{pen.Team}");
            }
            Update();
        }
        public void RemoveFinishedPenalties(int team)
        {
            List<Penalty> teamList = GetPenaltyList(team);
            List<Penalty> penalitiesToRemove = teamList.Where(p => p.IsActive && p.RemainingTime <= TimeSpan.Zero).ToList();

            foreach (var penalty in penalitiesToRemove)
            {
                teamList.Remove(penalty);
            }
            UpdateActivePenalties(team);
        }

        public void SetPenaltyRemainingTime(int team, int index, TimeSpan time)
        {
            GetPenaltyList(team)[index].RemainingTime = time;
            Update();
        }

        public void ListAllPenalties()
        {
            Console.WriteLine("Home Penalties:");
            foreach (Penalty pen in _homePenalties)
            {
                Console.WriteLine($"{pen.PlayerNumber}\t{pen.RemainingTime}\t{pen.IsActive}\t{pen.Team}");
            }

            Console.WriteLine("\n---\n");

            Console.WriteLine("Away penalties");
            foreach (Penalty pen in _awayPenalties)
            {
                Console.WriteLine($"{pen.PlayerNumber}\t{pen.RemainingTime}\t{pen.IsActive}\t{pen.Team}");
            }


        }

        public void Update()
        {
            RemoveFinishedPenalties(0);
            RemoveFinishedPenalties(1);
        }

        private void UpdateActivePenalties(int team)
        {
            List<Penalty> teamList = GetPenaltyList(team);
            List<Penalty> activePenalties = new List<Penalty>();


            for (int i = 0; i < teamList.Count; i++)
            {
                if (i < MaxActivePenalties)
                {
                    teamList[i].IsActive = true;
                    activePenalties.Add(teamList[i]);
                }
                else
                {
                    teamList[i].IsActive = false;
                }
            }

            if (team == 0)
            {
                HomePenalty1 = activePenalties.Count > 0 ? activePenalties[0] : CreateEmptyPenalty(team);
                HomePenalty2 = activePenalties.Count > 1 ? activePenalties[1] : CreateEmptyPenalty(team);
            }
            else
            {
                AwayPenalty1 = activePenalties.Count > 0 ? activePenalties[0] : CreateEmptyPenalty(team);
                AwayPenalty2 = activePenalties.Count > 1 ? activePenalties[1] : CreateEmptyPenalty(team);
            }
        }


    }
}