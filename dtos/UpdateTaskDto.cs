﻿namespace TaskManagementAPI.dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int StateId { get; set; }
    }
}
