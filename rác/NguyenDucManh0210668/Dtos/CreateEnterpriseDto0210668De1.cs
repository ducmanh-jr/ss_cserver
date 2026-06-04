using System.ComponentModel.DataAnnotations;

namespace nguyenducmanh0210668.Dtos
{
    public class CreateEnterpriseDto0210668De1
    {
        private string _name;
        private string _taxCode;
        private string _address;

        [Required(ErrorMessage = "Tên doanh nghiệp là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên doanh nghiệp không được vượt quá 200 ký tự")]
        public string Name 
        { 
            get => _name; 
            set => _name = value?.Trim(); 
        }

        [Required(ErrorMessage = "Mã số thuế là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã số thuế không được vượt quá 50 ký tự")]
        public string TaxCode 
        { 
            get => _taxCode; 
            set => _taxCode = value?.Trim(); 
        }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [StringLength(500, ErrorMessage = "Địa chỉ không được vượt quá 500 ký tự")]
        public string Address 
        { 
            get => _address; 
            set => _address = value?.Trim(); 
        }
    }
}
