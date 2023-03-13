namespace TeamGenerator
{
    public class Team
    {
        public string TeamName { get; set; }

        public VolunteerData Coach { get; set; }

        public List<Participant> Players { get; set; }
    }
}
