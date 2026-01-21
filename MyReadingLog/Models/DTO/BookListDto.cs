namespace MyReadingLog.Models.DTO
{
	public class BookListDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string CategoryName { get; set; }
		public string StatusName { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime RevisedDate { get; set; }
	}
}
