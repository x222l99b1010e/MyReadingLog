using System.ComponentModel.DataAnnotations.Schema;

namespace MyReadingLog.Models
{
	public class Book
	{
		public int BookId { get; set; }
		
		public string Title { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public string ISBN { get; set; }
		//外鍵欄位
		public int CategoryId { get; set; }
		public int StatusId { get; set; }
		public string RevisorId { get; set; }

		//審計欄位
		public DateTime PublishedDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime RevisedDate { get; set; }

		//導覽屬性 (Navigation Properties)
		public virtual Category Category { get; set; }
		public virtual Status Status { get; set; }
		// 這裡最關鍵：指名這個 Revisor 物件要對應到 RevisorId
		[ForeignKey("RevisorId")]
		public virtual ApplicationUser Revisor { get; set; }

		// 記得補上 Review 的集合，這才是一對多的完整體
		public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
		public virtual ICollection<BookTag> BookTag { get; set; }
	}
}
