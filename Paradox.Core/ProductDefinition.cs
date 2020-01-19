namespace Paradox.Core
{
    public class ProductDefinition: Base
    {
        public ProductDefinition(string title, Category category)
        {
            Title = title;
            Category = category;
        }
        public string Title { get; }
        public Category Category { get; }
    }
}