namespace FeQuizJourney.Components.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Teacher Teacher { get; set; } = new();
    }

    public class RoomResponse
    {
        public int RoomId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class RoomDetail
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? TeacherUsername { get; set; }
    }

    public class RoomDetailResponse
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = new();
        public List<Question> Questions { get; set; } = new();

        public string TeacherUsername => Teacher?.Username ?? "N/A";
    }

}
