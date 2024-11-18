using System.Web;

namespace Lab_5.Models
{
    public class SearchViewModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? Items { get; set; }
        public string? StartsWith { get; set; }
        public string? EndsWith { get; set; }

        public string ParseToQuery()
        {
            var queryParams = HttpUtility.ParseQueryString(string.Empty);

            if (DateFrom != null) queryParams["dateFrom"] = DateFrom?.ToString("yyyy-MM-ddTHH:mm:ss");
            if (DateTo != null) queryParams["dateTo"] = DateTo?.ToString("yyyy-MM-ddTHH:mm:ss");
            if (!String.IsNullOrEmpty(Items)) queryParams["items"] = Items;
            if (!String.IsNullOrEmpty(StartsWith)) queryParams["nameStartsWith"] = StartsWith;
            if (!String.IsNullOrEmpty(EndsWith)) queryParams["nameEndsWith"] = EndsWith;

            return String.Join("&", queryParams);
        }
    }
}
