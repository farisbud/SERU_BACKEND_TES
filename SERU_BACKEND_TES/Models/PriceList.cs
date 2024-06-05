namespace SERU_BACKEND_TES.Models
{
    public class PriceList
    {
        public int id { get; set; }
        public string code { get; set; }
        public double price { get; set; }

        public List<VehicleYear> years { get; set; }
        public int year_id { get; set; }

        public List<VehicleModel> vehicleModels { get; set; }
        public int model_id { get; set; }

    }
}
