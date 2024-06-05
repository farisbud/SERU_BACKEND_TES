namespace SERU_BACKEND_TES.Models
{
    public class VehicleYear
    {
        public int id { get; set; }
        public string year { get; set; }

        public PriceList priceLists { get; set; }
    }
}
