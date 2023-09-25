namespace Adventure
{
    public class Location
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Dictionary<string, ActionSubject> Subjects { get; set; }

    }

    public class Effect
    {
        public string Description { get; set; }
        public Action<ActionSubject, string, Location, Player, Action<string>> Action { get; set; }
    }

    public class ActionSubject
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Dictionary<string, Effect> Actions { get; set; }
    }

    public class Player
    {
        public int HitPoints { get; set; }
        public List<ActionSubject> Inventory { get; set; }
    }
}