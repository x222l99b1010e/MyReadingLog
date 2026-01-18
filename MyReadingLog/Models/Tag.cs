namespace MyReadingLog.Models
{
	public class Tag
	{
		public int TagId { get; set; }
		public string TagName { get; set; }
		// 多對多：一個標籤可屬於多本書
		public virtual ICollection<BookTag> BookTags { get; set; }
	}
}
