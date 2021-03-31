namespace SaleWebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [Key]
        public int No { get; set; }

        public Guid Id { get; set; }

       
        public string Name { get; set; }

        
        public string Description { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitPrice { get; set; }

        public int SalePrice { get; set; }

      
        public string Image { get; set; }

        public int CategoryNo { get; set; }

        public bool IsActive { get; set; }

        public virtual Category Category { get; set; }
    }
}
