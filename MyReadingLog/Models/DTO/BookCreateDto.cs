namespace MyReadingLog.Models.DTO
{
	public class BookCreateDto
	{
		public string Title { get; set; }
		public string Author { get; set; }
		public string? Description { get; set; }
		public string ISBN { get; set; }
		public string CategoryName { get; set; }
		public string StatusId { get; set; }
		public DateTime PublishedDate { get; set; }
	}
}
