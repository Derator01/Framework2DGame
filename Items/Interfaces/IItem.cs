namespace SimpleGame.Items.Interfaces
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        int Count { get; set; }
    }
}