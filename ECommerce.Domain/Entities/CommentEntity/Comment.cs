using System;
using ECommerce.Domain.Entities.ProductEntity;

namespace ECommerce.Domain.Entities.CommentEntity {
    public class Comment {
        public int CommentId { get; set; }
        public Guid CommentAkId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public decimal ReviewRate { get; set; }
        public ProductSku ProductSku { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
    }
}