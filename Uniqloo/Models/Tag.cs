namespace Uniqloo.Models
{
    public class Tag
    {
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
