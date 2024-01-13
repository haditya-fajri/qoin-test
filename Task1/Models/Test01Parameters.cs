namespace Task1.Models;

public class Test01Parameters
{
    private const int MaxPageSize = 20;
    private int _pageSize = 20;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}