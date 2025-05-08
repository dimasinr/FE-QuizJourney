namespace FeQuizJourney.Components.Models
{
        public class ApiResponsePagination
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public List<Room> Data { get; set; } = new();
        }

        public class ApiResponse<T>
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
            public T Data { get; set; }
        }

}
