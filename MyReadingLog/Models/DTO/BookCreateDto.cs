using System.ComponentModel.DataAnnotations;

namespace MyReadingLog.Models.DTO
{
	public class BookCreateDto
	{
		[Required(ErrorMessage = "書名為必填")]
		[StringLength(100)]
		public string Title { get; set; }
		[Required(ErrorMessage = "作者為必填")]
		[StringLength(100)]
		public string Author { get; set; }
		[MaxLength(500)]
		public string Description { get; set; }
		[MaxLength(20)]
		public string ISBN { get; set; }
		[Required(ErrorMessage = "類別為必填")]
		public int CategoryId { get; set; }
		[Required(ErrorMessage = "書籍狀態為必填")]
		public int BookStatusId { get; set; }
		public DateTime? PublishedDate { get; set; }
		[MaxLength(200)]
		public string? Note { get; set; }
	}
}
