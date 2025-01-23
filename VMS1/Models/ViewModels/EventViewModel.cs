namespace VMS1.Models.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public Dictionary<int,int> EventIds { get; set; }
    }
}
