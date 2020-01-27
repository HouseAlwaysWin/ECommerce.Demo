using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.ProductEntity
{
    public class Category
    {
        public int ID { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public DateTime EditedDate { get; set; }
        public DateTime CreatedDate { get; set; }


        public Guid? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> Childs { get; set; }

        public int? ProductID { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}