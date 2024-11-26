using System;
using AuctionService;
using BiddingService.Models;
using Grpc.Net.Client;

namespace BiddingService.Services;

public class GrpcAuctionClient(ILogger<GrpcAuctionClient> logger, IConfiguration config)
{
    public Auction GetAuction(string id)
    {
        logger.LogInformation("Calling GRPC Service");

        var channel = GrpcChannel.ForAddress(config["GrpcAuction"]!);
        var client = new GrpcAuction.GrpcAuctionClient(channel);
        var request = new GetAuctionRequest { Id = id };

        try
        {
            var reply = client.GetAuction(request);
            var auction = new Auction
            {
                ID = reply.Auction.Id,
                AuctionEnd = DateTime.Parse(reply.Auction.AuctionEnd),
                Seller = reply.Auction.Seller,
                ReservedPrice = reply.Auction.ReservedPrice
            };

            return auction;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not call GRPC Server");
            return null;
        }
    }
}
