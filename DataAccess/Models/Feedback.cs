using System;
using System.Collections.Generic;

namespace AllForAll.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? ProductId { get; set; }

    public int? UserId { get; set; }

    //TODO: Чому це поле nullable? Хіба може бути фідбек без рейтинга?
    public decimal? Rating { get; set; }

    public string? Сomment { get; set; }

    //TODO: Чому це поле nullable? Хіба може бути фідбек без дати?
    //TODO: поле можна назвати просто Date. І так зрозуміло, що воно відноситься до Feedback
    public DateTime? FeedbackDate { get; set; }

    //TODO: Чому це поле nullable? Хіба може бути фідбек без продукта?
    public virtual Product? Product { get; set; }

    //TODO: Чому це поле nullable? Хіба може бути фідбек без користувача?
    public virtual User? User { get; set; }
}
