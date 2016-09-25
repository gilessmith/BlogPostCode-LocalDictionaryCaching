namespace LocalDictionaryCaching
{
    using System.Collections.Generic;

    public class ApplePriceCalculatorWithFullyCachedMarketPriceService
    {
        private readonly ICachedMarketPriceService marketPriceService;

        public ApplePriceCalculatorWithFullyCachedMarketPriceService(ICachedMarketPriceService marketPriceService)
        {
            this.marketPriceService = marketPriceService;
        }

        public decimal CalculateTodaysMarketPriceForAppleBasket(ICollection<IApple> appleBasket)
        {
            var totalPrice = 0m;

            foreach (var apple in appleBasket)
            {
                // Note that it would be easy to alter this interface to TryFetchPrice(out price)
                // if the price wasn't essential.
                totalPrice += this.marketPriceService.FetchTodaysPriceForVariety(apple.Variety);
            }

            return totalPrice;
        }
    }

    /// <summary>
    /// This implementation of this interface should only handle caching for market price retreival. It should call a
    /// separate interface to actually load the prices from the data provider, thus maintaining the SRP.
    /// </summary>
    public interface ICachedMarketPriceService
    {
        decimal FetchTodaysPriceForVariety(AppleVariety variety);
    }
}