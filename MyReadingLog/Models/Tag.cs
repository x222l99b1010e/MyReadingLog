using System.ComponentModel.DataAnnotations;

namespace MyReadingLog.Models
{
	public class Tag
	{
		public int TagId { get; set; }
		[Required(ErrorMessage ="標籤為必填")]
		[StringLength(24)]
		public string TagName { get; set; }
		// 多對多：一個標籤可屬於多本書
		public virtual ICollection<BookTag> BookTags { get; set; }

		public Tag()
		{
			BookTags = new HashSet<BookTag>();
		}
	}
}
