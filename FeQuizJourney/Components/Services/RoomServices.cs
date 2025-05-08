using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.JSInterop;
using FeQuizJourney.Components.Models;

namespace FeQuizJourney.Components.Services;
public class RoomServices
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public RoomServices(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<ApiResponsePagination?> GetRoomsAsync()
    {
        // Ambil token dari localStorage
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrEmpty(token))
            return null;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync("http://localhost:5121/api/room?page=1&pageSize=5");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed to fetch rooms: " + response.StatusCode);
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<ApiResponsePagination>(json, options);
        return result;
    }

    public async Task<RoomDetailResponse?> GetRoomsByIdAsync(int roomId)
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        if (string.IsNullOrEmpty(token))
            return null;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"http://localhost:5121/api/room/{roomId}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("Failed to fetch room: " + response.StatusCode);
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Karena JSON-nya langsung berupa RoomDetailResponse
        var dto = JsonSerializer.Deserialize<RoomDetailResponse>(json, options);

        if (dto == null)
            return null;

        // Mapping ke RoomDetail
        var roomDetail = new RoomDetailResponse
        {
            Id = dto.Id,
            Code = dto.Code,
            Title = dto.Title,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            TeacherId = dto.Teacher?.Id ?? dto.TeacherId,
            Teacher = dto.Teacher != null ? new Teacher
            {
                Id = dto.Teacher.Id,
                Username = dto.Teacher.Username,
            } : null,
            Questions = dto.Questions?.Select(q => new Question
            {
                Id = q.Id,
                RoomId = q.RoomId,
                Text = q.Text,
                CorrectChoiceId = q.CorrectChoiceId
            }).ToList() ?? new List<Question>()
        };

        return roomDetail;
    }

}

