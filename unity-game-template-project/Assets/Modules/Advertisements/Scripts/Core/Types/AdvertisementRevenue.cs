namespace Modules.Advertisements.Types
{
    public struct AdvertisementRevenue
    {
        public AdvertisementRevenue(string advertisementsSystemName, string networkName, string placement,
            string advertisementUnitName, string format, double revenue, string currency)
        {
            AdvertisementsSystemName = advertisementsSystemName;
            NetworkName = networkName;
            Placement = placement;
            AdvertisementUnitName = advertisementUnitName;
            Format = format;
            Revenue = revenue;
            Currency = currency;
        }

        public string AdvertisementsSystemName { get;}
        
        public string NetworkName { get; }
        
        public string Placement { get; }
        
        public string AdvertisementUnitName { get; }
        
        public string Format { get; }
        
        public double Revenue { get; }

        public string Currency { get; }
    }
}