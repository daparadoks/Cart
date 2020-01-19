namespace Paradox.Core
{
    public class Category : Base
    {
        public Category(string title, int id = 0) : base(id)
        {
            Title = title;
        }

        public string Title { get; }
    }
}