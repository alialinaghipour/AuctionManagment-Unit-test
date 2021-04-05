using AuctionManagment.Exceptions;
using AuctionManagment.Tests.TestTools;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AuctionManagment.Tests
{
    public class AuctionTest
    {
        [Fact]
        public void open_auction_with_validation_data()
        {
            var _salleryId = 1;
            var _basePrice = 2000;
            var _isOpen = true;
            var _expirationDate = DateTime.Now.AddDays(2);
            var auction = new Auction(_salleryId, _basePrice, _isOpen, _expirationDate);

            var sut = new Auction(1, 2000, true, DateTime.Now.AddDays(2));

            sut.SalleryId.Should().Be(auction.SalleryId);
            sut.BasePrice.Should().Be(auction.BasePrice);
            sut.IsOpen.Should().Be(auction.IsOpen);
            sut.ExpirationDate.Should().BeOnOrAfter(DateTime.Now.AddDays(1));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void base_price_can_not_be_zero_or_less_than_zero(int basePrice)
        {

            Action auction = () => new AuctionBuilder()
                                        .WithBasePrice(basePrice)
                                        .Build();

            auction.Should().ThrowExactly<BasePriceCanNotBeZeroOrLessThenZeroException>();
        }

        [Fact]
        public void customer_can_not_place_a_bid_price_more_from_base_price()
        {
            var auction = new AuctionBuilder()
                             .WithBasePrice(2000)
                             .Build();

            Action expected = () => auction.PlaceBid(new Bid(1, 1000));

            expected.Should().ThrowExactly<CanNotPriceBeMoreFromBasePriceException>();
        }

        [Fact]
        public void customer_proposed_price_can_not_be_more_from_last_proposed_price()
        {
            var auction = new AuctionBuilder().Build();
            auction.Winner = new Bid(1, 8000);

            Action sut = () => auction.PlaceBid(new Bid(2, 5000));

            sut.Should().ThrowExactly<ProposedPriceConNotBeMoreFromLastProposedPriceException>();
        }

        [Fact]
        public void sallery_can_not_for_your_acution_submit_a_proposal()
        {
            var auction = new AuctionBuilder().Build();
            var bid = new Bid(5, 3000);
            bid.SalleryId = 1;

            Action sut = () => auction.PlaceBid(bid);

            sut.Should().ThrowExactly<SalleryCanNotForYourAuctionSubmitProposalException>();
        }

        [Fact]
        public void after_expiration_date_can_not_submit_a_proposal()
        {
            var auction = new AuctionBuilder().Build();
            var dateBid = DateTime.Now.AddDays(2);

            Action sut = () => auction.EndExpirationDate(dateBid);

            sut.Should().ThrowExactly<AfterExpirationDateCanNotSubmitProposalException>();
        }

        [Fact]
        public void sallery_cancel_from_sale_be_auction_closed()
        {
            var auction = new AuctionBuilder().Build();

            Action sut = () => auction.CancelFromSale(false);

            sut.Should().ThrowExactly<CancelFromSaleAuctionClosedException>();
        }

        [Fact]
        public void customer_can_not_submit_proposal_for_auction_closed()
        {
            var auction = new AuctionBuilder().Build();
            auction.IsOpen = false;

            Action sut = () => auction.PlaceBid(new Bid(1, 3000));

            sut.Should().ThrowExactly<ThisAuctionClosedExcepion>();
        }
    }
}

