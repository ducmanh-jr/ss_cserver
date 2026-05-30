using System.ComponentModel.DataAnnotations;

namespace DucManhJr1234.Dtos.Enterprises;

public class DeleteEnterpriseDto1234De1
{
    [Range(1, int.MaxValue, ErrorMessage = "Id doanh nghiep phai lon hon 0")]
    public int Id { get; set; }
}
