using System;
using System.Collections.Generic;

namespace AllForAll.Models;

public partial class Category
{
    //TODO: Альтернативною назвою може бути просто "Id"
    public int CategoryId { get; set; }

    //TODO: Чому це поле nullable? Хіба категорія може бути без назви?
    public string? Name { get; set; }

    //TODO: Скорочена назва поля ("Desc") може бути не зрозумілою при перегляді. Краще давати повну назву ("Description")
    public string? Desc { get; set; }

    //TODO: Чому це поле nullable? Хіба категорія може бути без фото?
    public string? CategoryPhotoLink { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
