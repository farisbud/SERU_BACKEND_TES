using System.ComponentModel.DataAnnotations;

namespace SERU_BACKEND_TES.ViewModels.VehicleTypeVM
{
    public class CreateType
    {
        public int id {  get; set; }
        [Required(ErrorMessage = "Tipe Kendaraan tidak boleh kosong !!")]
        public string name { get; set; }
        [Required(ErrorMessage = "Brand Kendaraan tidak boleh kosong !!")]
        public int brandId { get; set; }
    }

    //public class UpdateType
    //{
        //public int id {get; set;}
        //[Required(ErrorMessage = "Tipe Kendaraan tidak boleh kosong !!")]
        //public string name { get; set; }
        //[Required(ErrorMessage = "Brand Kendaraan tidak boleh kosong !!")]
        //public int brandId { get; set; }
    //}

}
