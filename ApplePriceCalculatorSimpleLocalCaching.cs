using System.Collections.Generic;

namespace LocalDictionaryCaching
{
    public class ApplePriceCalculatorSimpleLocalCaching
    {
        private readonly IMarketPriceService marketPriceService;

        public ApplePriceCalculatorSimpleLocalCaching(IMarketPriceService marketPriceService)
        {
            this.marketPriceService = marketPriceService;
        }

        public decimal CalculateTodaysMarketPriceForAppleBasket(ICollection<IApple> appleBasket)
        {
            var marketPrices = this.marketPriceService.LoadTodaysMarketPrices();
            // We could alter the service interface to allow us to specify the varieties to load e.g. this.marketPriceService.LoadTodaysMarketPrices(appleBasket.Select(a => a.Variety).Distinct())
            
            var totalPrice = 0m;

            foreach (var apple in appleBasket)
            {
                // in a more realistic version of this implementation there is a good chance that it won't be a simple case of adding up the prices. e.g. 
                // if(apple.IsPastBestBeforeDate){continue;}

                totalPrice += marketPrices[apple.Variety];
            }

            return totalPrice;
        }

    }

    public interface IMarketPriceService
    {
        Dictionary<AppleVariety, decimal> LoadTodaysMarketPrices();
    }

    public enum AppleVariety
    {
        GrannySmith,
        Cox,
        Pipin
    }

    public interface IApple  
    {
        AppleVariety Variety { get; }
    }
}
