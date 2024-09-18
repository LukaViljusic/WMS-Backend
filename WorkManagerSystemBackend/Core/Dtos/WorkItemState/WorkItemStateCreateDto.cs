namespace WorkManagerSystemBackend.Core.Dtos.WorkItemState
{
    public class WorkItemStateCreateDto
    {
        public String Name { get; set; }
        public String Color { get; set; }
        public String InitialStatus = "True";
        public String FinalStatus = "False";
        public long SpaceId { get; set; }
    }
}
