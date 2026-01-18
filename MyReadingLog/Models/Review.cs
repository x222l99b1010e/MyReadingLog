using System.ComponentModel.DataAnnotations;

namespace MyReadingLog.Models
{
	public class Review
	{
		// 1. [主鍵標籤] (雖然 ID 結尾 EF 會自動識別，但手寫更清楚)
		public int ReviewId { get; set; }
		// 2. [必填標籤], [字數限制標籤]
		[MaxLength(2000)]
		public string Content { get; set; }
		// 3. [範圍限制標籤 (1-5)]
		[Range(1,5)]
		public int StarRating { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime RevisedDate { get; set; }
		// 外鍵
		public int BookId { get; set; }
		// 4. [必填標籤]
		[Required]
		public string ApplicationUserId { get; set; }
		// 導覽屬性
		// 5. 加上 virtual 關鍵字
		public virtual ApplicationUser ApplicationUser { get; set; }
		public virtual Book Book { get; set; }
		// 6. 建立一個建構函數，把時間初始化為 DateTime.Now
		public Review()
		{
			CreatedDate = DateTime.Now;
			RevisedDate = DateTime.Now;
		}

	}
}
