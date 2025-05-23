namespace FeQuizJourney.Components.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Text { get; set; }
        public int CorrectChoiceId { get; set; }
    }

    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public Choice? SelectedChoice { get; set; }
        public List<Choice> Choices { get; set; } = new();
        public int CorrectChoiceId { get; set; }
        public int Score { get; set; }
    }

    public class StudentAnswerRequest
    {
        public int QuestionId { get; set; }
        public int SelectedChoiceId { get; set; }
        public int TimeTaken { get; set; }
    }

    public class StudentAnswerResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = new();
        public int SelectedChoiceId { get; set; }
        public int TimeTaken { get; set; }
        public int Score { get; set; }
    }

    public class UserScoreResponse
    {
        public string Username { get; set; } = string.Empty;
        public double Score { get; set; }
    }

    public class CreateRoomRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddHours(72);
    }

    public class CreateQuestionRequest
    {
        public int RoomId { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<CreateChoice> Choices { get; set; } = new();
    }

    public class CreateChoice
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;
    }

}
