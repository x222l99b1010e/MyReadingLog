using System.ComponentModel.DataAnnotations;

namespace MyReadingLog.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		[Required(ErrorMessage ="分類名稱為必填")]
		[StringLength(24)]
		public string CategoryName { get; set; }

		// 一對多：一個分類有多本書
		public virtual ICollection<Book> Books { get; set; }
	}
}
