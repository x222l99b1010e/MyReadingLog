namespace MyReadingLog.Models
{
	public class ReviewStatus
	{
		public int ReviewStatusId { get; set; }
		public string ReviewStatusName { get; set; }

		public virtual ICollection<Review> Reviews { get; set; }
	}
}
