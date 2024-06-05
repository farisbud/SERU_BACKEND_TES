namespace SERU_BACKEND_TES.Models
{
    public class VehicleModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<VehicleType> vehicleTypes { get; set; }
        public string type_id { get; set; }

        public PriceList priceLists { get; set; }
    }
}
