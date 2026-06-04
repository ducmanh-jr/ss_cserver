using System.ComponentModel.DataAnnotations;

namespace Nguyen_Khanh_Thu_193865.Dtos.Shipper193865De3Dto
{
    public class CreateShipper193865De3Dtos
    {
      
        private string _maShipper;
        public string MaShipper
        {
            get => _maShipper;
            set => _maShipper = value?.Trim();
        }

        private string _cccd;
        public string CCCD
        {
            get => _cccd;
            set => _cccd = value?.Trim();
        }
        private string _ten;
        public string Ten
        {
            get => _ten;
            set => _ten = value?.Trim();
        }

        public DateTime NgayThamGia { get; set; }
    }
}
