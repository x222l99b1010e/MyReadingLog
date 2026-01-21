namespace MyReadingLog.Models
{
	public class BookStatus
	{
		public int BookStatusId { get; set; }
		public string BookStatusName { get; set; }

		public virtual ICollection<Book> Books { get; set; }
	}
}
