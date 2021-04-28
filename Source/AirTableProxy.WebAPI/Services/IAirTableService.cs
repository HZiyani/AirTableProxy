using AirTableProxy.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirTableProxy.WebAPI.Services
{
    public interface IAirTableService
    {
        Task<List<ResponseModel>> GetRecords(string appKey, string baseId);
        Task<ResponseModel> AddRecord(RequestModel request, string appKey, string baseId);
    }
}