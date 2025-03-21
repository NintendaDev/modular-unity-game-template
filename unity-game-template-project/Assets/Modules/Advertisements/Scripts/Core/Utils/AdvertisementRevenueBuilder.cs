using System;
using Modules.Advertisements.Types;

namespace Modules.Advertisements.Scripts.Core.Utils
{
    public sealed class AdvertisementRevenueBuilder
    {
        private AdvertisementsSystemType _advertisementsSystemType;
        private string _networkName;
        private string _unitName;
        private string _format;
        private double _revenue;
        private RevenueCurrency _currency;
        private string _placement;

        public AdvertisementRevenueBuilder()
        {
            Clear();
        }

        public AdvertisementRevenueBuilder SetAdvertisementsSystem(AdvertisementsSystemType advertisementsSystemType)
        {
            _advertisementsSystemType = advertisementsSystemType;

            return this;
        }
        
        public AdvertisementRevenueBuilder SetNetworkName(string networkName)
        {
            _networkName = networkName;

            return this;
        }
        
        public AdvertisementRevenueBuilder SetPlacement(string placement)
        {
            _placement = placement;

            return this;
        }
        
        public AdvertisementRevenueBuilder SetAdvertisementUnitName(string advertisementUnitName)
        {
            _unitName = advertisementUnitName;

            return this;
        } 
        
        public AdvertisementRevenueBuilder SetFormat(string format)
        {
            _format = format;

            return this;
        } 
        
        public AdvertisementRevenueBuilder SetRevenue(double revenue)
        {
            _revenue = revenue;

            return this;
        } 
        
        public AdvertisementRevenueBuilder SetCurrency(RevenueCurrency currency)
        {
            _currency = currency;

            return this;
        }

        public AdvertisementRevenue Build()
        {
            if (_currency == RevenueCurrency.None)
                throw new Exception("Currency cannot be None");
            
            if (_revenue <= 0)
                throw new Exception("Revenue cannot be less or equal to zero");

            string advertisementsSystemName = (_advertisementsSystemType != AdvertisementsSystemType.None) 
                ? _advertisementsSystemType.ToString() : string.Empty;
                
            var revenue = new AdvertisementRevenue(advertisementsSystemName, _networkName,_placement, _unitName, 
                _format, _revenue, _currency.ToString());
            
            Clear();

            return revenue;
        }

        public void Clear()
        {
            _advertisementsSystemType = AdvertisementsSystemType.None;
            _networkName = string.Empty;
            _unitName = string.Empty;
            _format = string.Empty;
            _revenue = 0;
            _currency = RevenueCurrency.None;
        }
    }
}