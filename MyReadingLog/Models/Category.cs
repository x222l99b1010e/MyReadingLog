namespace MyReadingLog.Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }

		// 一對多：一個分類有多本書
		public virtual ICollection<Book> Books { get; set; }
	}
}
