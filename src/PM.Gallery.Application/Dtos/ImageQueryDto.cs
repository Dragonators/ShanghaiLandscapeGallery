using PM.Gallery.Application.Strategies.QueryStrategy.enums;

namespace PM.Gallery.Application.Dtos;

public class ImageQueryDto
{
    public string? Title { get; set; }
    public IEnumerable<string>? Tags { get; set; }

    public bool IsPaged { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public Dictionary<SortField, bool>? SortOptions { get; set; }
}