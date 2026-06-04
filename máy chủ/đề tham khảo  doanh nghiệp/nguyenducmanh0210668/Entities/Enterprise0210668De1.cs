using System;
using System.Collections.Generic;

namespace nguyenducmanh0210668.Entities
{
    public class Enterprise0210668De1
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TaxCode { get; set; } = null!;
        public string Address { get; set; } = null!;

        public ICollection<EnterpriseProduct0210668De1> EnterpriseProducts { get; set; } = new List<EnterpriseProduct0210668De1>();
    }
}
