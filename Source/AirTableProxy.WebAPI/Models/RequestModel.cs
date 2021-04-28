using System.ComponentModel.DataAnnotations;

namespace AirTableProxy.WebAPI.Models
{
    public class RequestModel
    {
        #region Properties
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        #endregion
    }
}