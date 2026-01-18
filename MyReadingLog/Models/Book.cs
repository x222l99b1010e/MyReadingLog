using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyReadingLog.Models
{
	public class Book
	{
		public int BookId { get; set; }
		[Required(ErrorMessage = "書名是必填的")] // 前端驗證：必填
		[StringLength(100)] // 資料庫：nvarchar(100) / 前端驗證：長度限制
		public string Title { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		[MaxLength(13)] // 資料庫：nvarchar(20)
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
		public virtual ICollection<BookTag> BookTags { get; set; }
		//在有 ICollection 的類別裡初始化集合，防止 NullReferenceException
		public Book()
		{
			Reviews = new HashSet<Review>(); // 使用 HashSet 效能較好且不重複
			BookTags = new HashSet<BookTag>();
			CreatedDate = DateTime.Now;
			RevisedDate = DateTime.Now;
		}
	}
}
