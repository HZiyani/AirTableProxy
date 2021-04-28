using AirtableApiClient;
using AirTableProxy.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Services
{
    public class AirTableService : IAirTableService
    {
        #region Methods
        #region IAirTableServiceImplementation
        public async Task<ResponseModel> AddRecord(RequestModel request, string appKey, string baseId)
        {
            var responseModel = new ResponseModel
            {
                ID = Utilities.GenerateID(),
                Title = request.Title,
                Text = request.Text,
                ReceivedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            };

            using AirtableBase airtableBase = new AirtableBase(appKey, baseId);
            var fields = new Fields();

            fields.AddField("id", responseModel.ID);
            fields.AddField("Summary", responseModel.Title);
            fields.AddField("Message", responseModel.Text);
            fields.AddField("receivedAt", responseModel.ReceivedAt);

            Task<AirtableCreateUpdateReplaceRecordResponse> task = airtableBase.CreateRecord("Messages", fields);

            AirtableCreateUpdateReplaceRecordResponse response = await task;

            if (response.Success)
            {
                return responseModel;
            }
            else if (response.AirtableApiError is AirtableApiException)
            {
                throw new InvalidOperationException(response.AirtableApiError.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException("Unknown error");
            }
        }

        public async Task<List<ResponseModel>> GetRecords(string appKey, string baseId)
        {
            var records = new List<AirtableRecord>();

            using AirtableBase airtableBase = new AirtableBase(appKey, baseId);

            Task<AirtableListRecordsResponse> task = airtableBase.ListRecords("Messages");

            AirtableListRecordsResponse response = await task;

            if (response.Success)
            {
                records.AddRange(response.Records.ToList());
            }
            else if (response.AirtableApiError is AirtableApiException)
            {
               throw new InvalidOperationException(response.AirtableApiError.ErrorMessage);
            }
            else
            {
                throw new InvalidOperationException("Unknown error");
            }

            var responseList = new List<ResponseModel>();

            foreach (var record in records)
            {
                if (record.Fields != null && RecordHasFields(record))
                {
                    var recordReponse = new ResponseModel
                    {
                        ID = record.Fields["id"].ToString(),
                        Title = record.Fields["Summary"].ToString(),
                        Text = record.Fields["Message"].ToString(),
                    };
                    DateTime.TryParse(record.Fields["receivedAt"].ToString(), out var receivedAt);
                    recordReponse.ReceivedAt = receivedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                    responseList.Add(recordReponse);
                }
            }

            return responseList;
        }
        #endregion

        private bool RecordHasFields(AirtableRecord record)
        {
            string[] fields = { "id", "Summary", "Message", "receivedAt" };
            return fields.All(f => record.Fields.ContainsKey(f));
        }
        #endregion
    }
}