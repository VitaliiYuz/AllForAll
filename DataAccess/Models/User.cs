using System;
using System.Collections.Generic;

namespace AllForAll.Models;

public partial class User
{
    public int UserId { get; set; }

    //TODO: Чому це поле nullable? Хіба не може бути імені користувача?
    public string? Username { get; set; }

    //TODO: Чому це поле nullable? Хіба не може бути пошти?
    public string? Email { get; set; }

    public int? UserRoleId { get; set; }

    public DateOnly? DateJoined { get; set; }

    //TODO: Чому це поле nullable? Хіба не може бути пароля?
    public string? Password { get; set; }

    public string? IsGoogleAcc { get; set; }

    public string? UserPhotoLink { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual UserRole? UserRole { get; set; }
}
