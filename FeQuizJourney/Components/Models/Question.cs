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
        public List<Choice> Choices { get; set; } = new();
        public int CorrectChoiceId { get; set; }
    }
}
