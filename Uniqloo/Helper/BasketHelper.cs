using System.Text.Json;
using Uniqloo.ViewModel.Baskets;

namespace Uniqloo.Helper
{
    public class BasketHelper
    {
        public static List<BasketCookieItemVM> GetBasket(HttpRequest request)
        {
            string? value = request.Cookies["basket"];
            if (value is null) return new();
            return JsonSerializer.Deserialize<List<BasketCookieItemVM>>(value) ?? new();
        }
    }
}
