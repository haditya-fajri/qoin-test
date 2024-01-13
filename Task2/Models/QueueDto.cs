namespace Task2.Models;

public class QueueDto<T>
{
    public string Command { get; set; }
    public T Data { get; set; }
}