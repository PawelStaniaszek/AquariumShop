using AquariumShop.Dtos;
using AquariumShop.Handlers;
using AquariumShop.Queries;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace AquariumShop.IntegrationTests
{
    public class StoriesApiTests
    {
        private IMediator mediator;
        private GetAllProductsHandler handler;
        private HttpClient client;
        private Mock<IRepository<Product>> repo;
        public StoriesApiTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            
           // mediator = webApplicationFactory.Services.GetRequiredService<ISender>();
            mediator = webApplicationFactory.Server.Services.GetRequiredService<IMediator>();
            //var s = webApplicationFactory.Services.GetRequiredService<object>();
            client = webApplicationFactory.CreateClient();
            //h = (IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>?)handler;
            var serviceConfig = new MediatRServiceConfiguration();
            repo = new Mock<IRepository<Product>>();
            //handler = new GetAllProductsHandler(repo.Object, new Mock<IMapper>().Object);
        }

        [Fact]
        public void HandleQuery_WhenMediatorSendQueryOnce()
        {
            var result = mediator.Send(new GetAllProductsQuery());
            Assert.True(repo.Invocations.Any());
        }

        [Fact]
        public void testStory()
        {
            var story = new GetAllProductsQuery();
            string url = "/api/Product";
            var response = client.GetFromJsonAsync<IEnumerable<ProductDto>>(url).Result;
            Assert.NotNull(response);
        }
    }
}
