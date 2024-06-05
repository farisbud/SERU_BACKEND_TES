using Dapper;
using SERU_BACKEND_TES.Interface;
using SERU_BACKEND_TES.Models;
using SERU_BACKEND_TES.Models.Data;
using SERU_BACKEND_TES.ViewModels.TableRequest;
using SERU_BACKEND_TES.ViewModels.VehicleTypeVM;

namespace SERU_BACKEND_TES.Services
{
    public class VehicleSvc : VehicleIntf
    {
        private readonly DapperDBContext _dapperDB;
        public VehicleSvc(DapperDBContext dbContext)
        {
            _dapperDB = dbContext;
        }

        public async Task<Dictionary<string, object>> getVehicleBrand(reqData req)
        {
            var res = new Dictionary<string, object>();

            try
            {
                var db = _dapperDB.CreateConnection();

                int offset = req.limit * req.page;


                string sqlTotal = @"select name
                            from vehicle_brand
                            WHERE ( Isnull(@search, '') = '' OR name LIKE '%'+@search+'%')";

                string sql = @"select id, name
                            from vehicle_brand
                            WHERE ( Isnull(@search, '') = '' OR name LIKE '%'+@search+'%')
                            ORDER BY id desc
                            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY";

                //if (req.order != null)
                //{
                //    parameters.Add("@sortColumn", reqdata.columns[reqdata.order[0].column].name);
                //    parameters.Add("@sortDir", reqdata.order[0].dir.ToLower());
                //}
                var totalResult = await db.QueryAsync(sqlTotal, new { @search = req.search }).ConfigureAwait(false);

                var result = await db.QueryAsync<VehicleBrand>(sql, new { @search = req.search, @offset = offset, @limit = req.limit }).ConfigureAwait(false);
                decimal totalRow = totalResult.Count();


                res["status"] = 200;
                res["data"] = result.ToList();
                res["page"] = req.page;
                res["limit"] = req.limit;
                res["totalRows"] = totalRow;
                res["totalPage"] = Math.Ceiling(totalRow / req.limit);
            }
            catch (Exception ex)
            {
                res["status"] = 500;
                res["message"] = ex.Message.ToString();
            }

            return res;
        }

        public async Task<Dictionary<string, object>> getVehicleTypeByBrand(int id)
        {
            var res = new Dictionary<string, object>();

            try
            {
                var db = _dapperDB.CreateConnection();
                string sql = @"SELECT vb.id,vb.name, vt.id,vt.name FROM vehicle_brand vb join vehicle_type vt on vt.brand_id = vb.id where vt.brand_id = @id";

                var orderBrand = new Dictionary<int, VehicleBrand>();
                var result = (await db.QueryAsync<VehicleBrand,VehicleType,VehicleBrand>(sql, (vb, vt) =>
                {
                    VehicleBrand brands;
                    if (!orderBrand.TryGetValue(vb.id, out brands))
                    {
                        brands = vb;
                        brands.vehicleTypes = new List<VehicleType>();
                        orderBrand.Add(brands.id, brands);
                    }

                    brands.vehicleTypes.Add(vt);
                    return brands;

                }, new {@id = id}).ConfigureAwait(false)).Distinct().ToList();

                res["status"] = 200;
                res["data"] = result.FirstOrDefault();

            }
            catch (Exception ex) 
            {
                res["status"] = 500;
                res["message"] = ex.Message.ToString();
            }

            return res;
        }

        public Dictionary<string, object> createVehicleType(CreateType request)
        {
            var res = new Dictionary<string, object>();

            string sql = @"INSERT INTO vehicle_type (name,brand_id,created_at) VALUES (@name, @brandId, @created)";
            try
            {
                var db = _dapperDB.CreateConnection();

                db.Execute(sql, new
                {
                    @name = request.name,
                    @brandId = request.brandId,
                    @created = DateTime.Now,
                });

                res["status"] = 200;
                res["message"] = "Data berhasil disimpan";
            }
            catch (Exception ex)
            {
                res["status"] = 500;
                res["message"] = ex.Message.ToString();
            }
            return res;
        }

        public Dictionary<string, object> updateVehicleType(CreateType request)
        {
            var res = new Dictionary<string, object>();

            try
            {
                var db = _dapperDB.CreateConnection();

                string sql = @"Update vehicle_type set name = @name, brand_id = @brandId, updated_at = @updated where id = @id";
               
                db.Execute(sql, new
                {
                    @name = request.name,
                    @brandId = request.brandId,
                    @updated = DateTime.Now,
                    @id = request.id
                });

                res["status"] = 200;
                res["message"] = "data berhasil di update";
            }
            catch (Exception ex) 
            {
                res["status"] = 500;
                res["message"] = ex.Message.ToString();
            }

            return res;
        }
    }
}
