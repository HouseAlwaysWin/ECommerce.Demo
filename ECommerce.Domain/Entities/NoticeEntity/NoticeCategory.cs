using System;
using System.Collections.Generic;

namespace ECommerce.Domain.Entities.NoticeEntity
{
    public class NoticeCategory
    {
        public int NoticeCategoryId { get; set; }
        public Guid NoticeCategoryAkId { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }

        public Guid? ParentId { get; set; }
        public NoticeCategory Parent { get; set; }
        public virtual ICollection<NoticeCategory> Child { get; set; }

        public virtual ICollection<Notice_NoticeCategory> Notice_NoticeCategory { get; set; }
    }
}