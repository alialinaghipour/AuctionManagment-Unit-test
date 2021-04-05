using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagment.Tests.TestTools
{
    public class AuctionBuilder
    {
        private int _salleryId = 1;
        private decimal _basePrice = 2000;
        private bool _isOpen = true;
        private DateTime _expirationDate = DateTime.Now.AddDays(2);

        public Auction Build()
        {
            return new Auction(_salleryId, _basePrice, _isOpen, _expirationDate);
        }
        public AuctionBuilder WithBasePrice(int basePrice)
        {
            _basePrice = basePrice;
            return this;
        }
    }
}
