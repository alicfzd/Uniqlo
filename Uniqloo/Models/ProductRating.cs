namespace Uniqloo.Models
{
    public class ProductRating
    {
        public int? ProductId { get; internal set; }
        public string UserId { get; internal set; }
        public int RatingRate { get; internal set; }
    }
}