namespace SERU_BACKEND_TES.Models
{
    public class VehicleType
    {
        public int id { get; set; }
        public string? name { get; set; }

        public VehicleBrand? brands { get; set; }
        public string? brand_id { get; set; }
    }
}
