using AirTableProxy.WebAPI.Controllers;
using AirTableProxy.WebAPI.Models;
using AirTableProxy.WebAPI.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AirTableProxy.WebAPI.Tests
{
    public class MessagesControllerTest
    {
        readonly MessagesController _controller;
        readonly IAirTableService _service;
        readonly IConfiguration _configuration;

        public MessagesControllerTest()
        {
            _service = new AirTableServiceFake();
            _configuration = new ConfigurationBuilder()
                .Build();
            _controller = new MessagesController(_configuration, _service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _controller.Get().Result;

            // Assert
            var items = Assert.IsType<List<ResponseModel>>(result);
            Assert.Equal(AirTableServiceFake.Responses.Count, items.Count);
        }

        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new RequestModel()
            {
                Title = "Fake Test Title",
                Text = "Fake Test Text",
            };

            // Act
            var createdResponse = await _controller.Post(testItem);

            // Assert
            Assert.IsType<ResponseModel>(createdResponse);
            Assert.Equal("Fake Test Text", createdResponse.Text);
        }
    }
}