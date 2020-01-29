using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.FaqEntity
{
    public class Faq_FaqCategory
    {
        public Guid FaqAkId { get; set; }
        public Faq Faq { get; set; }
        public Guid FaqCategoryAkId { get; set; }
        public FaqCategory FaqCategory { get; set; }
    }
}