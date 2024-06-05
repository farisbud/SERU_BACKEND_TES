using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SERU_BACKEND_TES.Helper;
using SERU_BACKEND_TES.Interface;
using SERU_BACKEND_TES.ViewModels.TableRequest;
using SERU_BACKEND_TES.ViewModels.VehicleTypeVM;

namespace SERU_BACKEND_TES.Controllers
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly VehicleIntf _vehicle;
        private readonly GenerateJwt _generateJwt;
        public VehicleController(VehicleIntf vehicle,GenerateJwt generateJwt) 
        { 
        _vehicle = vehicle;
        _generateJwt = generateJwt;
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getVehicleBrand")]
        public async Task<IActionResult> getVehicleBrand(reqData req)
        {
            var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

            if (token == null)
            {
                return Unauthorized();
            }

            try
            {
                var data = await _vehicle.getVehicleBrand(req);
                return Ok(data);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("getVehicleTypeByBrand")]
        public async Task<IActionResult> getVehicleTypeByBrand(int id)
        {
            var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

            if (token == null)
            {
                return Unauthorized();
            }

            try
            {
                var data = await _vehicle.getVehicleTypeByBrand(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateVehicleType")]
        public async Task<IActionResult> CreateVehicleType(CreateType req)
        {
            var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

            if (token == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = _vehicle.createVehicleType(req);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 500,
                    message = ex.Message.ToString(),
                });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("UpdateVehicleType")]
        public async Task<IActionResult> UpdateVehicleType(CreateType req)
        {
            var token = await _generateJwt.Verify(Request.Cookies["seruu"]);

            if (token == null)
            {
                return Unauthorized();
            }

            try
            {
                var result = _vehicle.updateVehicleType(req);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 500,
                    message = ex.Message.ToString(),
                });
            }
        }
    }
}
