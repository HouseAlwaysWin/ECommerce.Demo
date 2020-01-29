using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.FaqEntity
{
    public class FaqCategory
    {
        public int FaqCategoryId { get; set; }
        public Guid FaqCategoryAkId { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public Guid? ParentId { get; set; }
        public FaqCategory Parent { get; set; }
        public virtual ICollection<FaqCategory> Childs { get; set; }
        public virtual ICollection<Faq_FaqCategory> Faq_FaqCategory { get; set; }
    }
}