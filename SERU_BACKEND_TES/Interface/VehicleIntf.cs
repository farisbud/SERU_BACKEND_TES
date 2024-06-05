using SERU_BACKEND_TES.ViewModels.TableRequest;
using SERU_BACKEND_TES.ViewModels.VehicleTypeVM;

namespace SERU_BACKEND_TES.Interface
{
    public interface VehicleIntf
    {
        Task<Dictionary<string, object>> getVehicleBrand(reqData req);
        Task<Dictionary<string, object>> getVehicleTypeByBrand(int id);
        Dictionary<string, object> createVehicleType(CreateType request);
        Dictionary<string, object> updateVehicleType(CreateType request);
    }
}
