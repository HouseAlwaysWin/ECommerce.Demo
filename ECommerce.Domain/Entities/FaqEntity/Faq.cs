using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.FaqEntity
{
    public class Faq
    {
        public int FaqId { get; set; }
        public Guid FaqAkId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public virtual ICollection<Faq_FaqCategory> Faq_FaqCategory { get; set; }
    }
}