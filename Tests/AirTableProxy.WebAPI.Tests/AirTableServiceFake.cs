using AirTableProxy.WebAPI.Models;
using AirTableProxy.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Tests
{
    public class AirTableServiceFake : IAirTableService
    {
        #region Fields
        public static readonly List<ResponseModel> Responses;
        #endregion

        #region Constructors
        static AirTableServiceFake()
        {
            Responses = new List<ResponseModel>()
            {
                new ResponseModel
                {
                    ID = "1921cb6468dc4b6baf19f5fd5cd435cc",
                    Title = "Fake Title 1",
                    Text = "Fake Text 1",
                    ReceivedAt = "2021-04-28T12:00:00.000Z",
                },
                new ResponseModel
                {
                    ID = "53c661e7c2354537b8addbb3d95127be",
                    Title = "Fake Title 2",
                    Text = "Fake Text 2",
                    ReceivedAt = "2021-04-28T12:00:00.000Z",
                },
                new ResponseModel
                {
                    ID = "9b9b727644b1408ab3f08803a92a2dc1",
                    Title = "Fake Title 3",
                    Text = "Fake Text 3",
                    ReceivedAt = "2021-04-28T12:00:00.000Z",
                },
            };
        }
        #endregion

        #region Methods
        #region IAirTableService Implementation
        public Task<ResponseModel> AddRecord(RequestModel request, string appKey, string baseId)
        {
            var response = new ResponseModel
            {
                ID = Utilities.GenerateID(),
                Title = request.Title,
                Text = request.Text,
                ReceivedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            };
            Responses.Add(response);
            return Task.FromResult(response);
        }

        public Task<List<ResponseModel>> GetRecords(string appKey, string baseId)
        {
            return Task.FromResult(Responses);
        }
        #endregion
        #endregion
    }
}
