using System;
using System.Collections.Generic;
using ECommerce.Domain.Entities.NoticeEntity;

namespace ECommerce.Domain.Entities.NotificationEntity
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public Guid NoticeAkId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public virtual ICollection<Notice_NoticeCategory> Notice_NoticeCategory { get; set; }

    }
}