using System;
using System.Collections.Generic;

namespace nguyenducmanh0210668.Entities
{
    public class Product0210668De1
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime ImportDate { get; set; }

        public ICollection<EnterpriseProduct0210668De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668De1>();
    }
}
