using System.Data;

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
        public EventHandler? PenaltyChanged;
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


        // Adds a new penalty
        public void AddNewPenalty(int playerNumber, TimeSpan penaltyLength, int team)
        {
            try
            {
                Penalty penalty = new Penalty(playerNumber, penaltyLength, penaltyLength, team);
                GetPenaltyList(team).Add(penalty);
                RemoveFinishedPenalties(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            RemoveFinishedPenalties(team);
        }

        // Returns a specific teams penaltylist
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

        // Used to restore penalites after app crash
        public void RestorePenaltyLists(List<Penalty> homePenalties, List<Penalty> awayPenalaties)
        {
            _homePenalties = homePenalties;
            _awayPenalties = awayPenalaties;
            Update();
        }

        // Loops through team penalty list and removes finished penalties
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

        // Moves a penalty to index 0 in list
        public bool MovePenaltyToTop(int index, int team)
        {
            bool moved = false;
            try
            {
                Penalty penalty = GetPenaltyList(team)[index];
                bool removed = RemovePenaltyWithIndex(index, team);
                if (removed)
                {
                    List<Penalty> teamList = GetPenaltyList(team);
                    teamList.Insert(0, penalty);
                    moved = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            RemoveFinishedPenalties(team);
            return moved;
        }

        // Removes a specific penalty
        public bool RemovePenaltyWithIndex(int index, int team)
        {
            List<Penalty> teamList = GetPenaltyList(team);
            bool removed = false;
            try
            {
                teamList.RemoveAt(index);
                removed = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            RemoveFinishedPenalties(team);
            return removed;
        }

        // Sets a specific penalty to a specific time
        public void SetPenaltyRemainingTime(int index, TimeSpan time, int team)
        {
            GetPenaltyList(team)[index].RemainingTime = time;
            Update();
        }


        // Method to list all penalties in console
        public void ListAllPenalties()
        {
            Console.WriteLine("Hemmautvisningar:");
            for (int i = 0; i < _homePenalties.Count; i++)
            {
                Console.WriteLine($"[{i}]\t{_homePenalties[i].PlayerNumber}\t{_homePenalties[i].RemainingTime}\t{_homePenalties[i].IsActive}\t{_homePenalties[i].Team}");
            }

            Console.WriteLine("\n---\n");

            Console.WriteLine("Bortautvisningar");
            for (int i = 0; i < _awayPenalties.Count; i++)
            {
                Console.WriteLine($"[{i}]\t{_awayPenalties[i].PlayerNumber}\t{_awayPenalties[i].RemainingTime}\t{_awayPenalties[i].IsActive}\t{_awayPenalties[i].Team}");
            }

        }

        public void Update()
        {
            RemoveFinishedPenalties(0);
            RemoveFinishedPenalties(1);
        }
        
        // Loops through team penaltylist sets index 0 and 1 to active and sets to (Home/Away)Penalty1/2
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
        
            PenaltyChanged?.Invoke(this, new EventArgs());
        }

        // Method to create empty penalty
        private Penalty CreateEmptyPenalty(int team)
        {
            return new Penalty(0, TimeSpan.Zero, TimeSpan.Zero, team);
        }
    }
}