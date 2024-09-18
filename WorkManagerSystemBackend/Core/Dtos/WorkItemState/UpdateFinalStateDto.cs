namespace WorkManagerSystemBackend.Core.Dtos.WorkItemState
{
    public class UpdateFinalStateDto
    {
        public long Id { get; set; }
        public string FinalStatus { get; set; }
    }
}
