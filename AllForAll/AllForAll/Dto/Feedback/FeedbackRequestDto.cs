namespace AllForAll.Dto.Feedback;

public class FeedbackRequestDto
{
    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    public decimal? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? FeedbackDate { get; set; }
}