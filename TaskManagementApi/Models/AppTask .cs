namespace TaskManagementApi.Models
{
    public class AppTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int StateId { get; set; }
        public State? State { get; set; }
    }
}
