namespace SERU_BACKEND_TES.Models
{
    public class VehicleBrand
    {
        public int id { get; set; }
        public string? name { get; set; }

        public List<VehicleType>? vehicleTypes { get; set; }

    }
}
