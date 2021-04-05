using AuctionManagment.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagment
{
    public class Auction
    {
        public int SalleryId { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Bid Winner { get; set; }

        public Auction(int salleryId, decimal basePrice = 0, bool isOpen = false, DateTime expirationDate = default)
        {
            if (basePrice <= 0)
                throw new BasePriceCanNotBeZeroOrLessThenZeroException();

            SalleryId = salleryId;
            BasePrice = basePrice;
            IsOpen = isOpen;
            ExpirationDate = expirationDate;
        }

        public void PlaceBid(Bid bid)
        {
            if (bid.Price < BasePrice)
                throw new CanNotPriceBeMoreFromBasePriceException();

            if (IsOpen == false)
                throw new ThisAuctionClosedExcepion();

            if (bid.SalleryId == SalleryId)
                throw new SalleryCanNotForYourAuctionSubmitProposalException();

            if (bid.Price < Winner.Price)
                throw new ProposedPriceConNotBeMoreFromLastProposedPriceException();
            Winner = bid;
        }

        public void EndExpirationDate(DateTime dateBid)
        {
            if (dateBid > ExpirationDate)
                throw new AfterExpirationDateCanNotSubmitProposalException();
        }

        public void CancelFromSale(bool isOpen)
        {
            if (isOpen == false)
                throw new CancelFromSaleAuctionClosedException();

            IsOpen = isOpen;
        }
    }
}
