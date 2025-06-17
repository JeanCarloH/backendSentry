namespace TaskManagementAPI.dtos
{
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
