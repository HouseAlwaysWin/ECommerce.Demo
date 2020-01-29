using System;
using ECommerce.Domain.Entities.NotificationEntity;

namespace ECommerce.Domain.Entities.NoticeEntity
{
    public class Notice_NoticeCategory
    {
        public Guid NoticeAkId { get; set; }
        public Notice Notice { get; set; }
        public Guid NoticeCategoryAkId { get; set; }
        public NoticeCategory NoticeCategory { get; set; }
    }
}