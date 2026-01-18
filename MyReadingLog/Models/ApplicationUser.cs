namespace MyReadingLog.Models
{
	public class ApplicationUser
	{
		public string Id { get; set; }
		public string NickName { get; set; }
		public string ProfilePicturePath { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime RevisedDate { get; set; }
		// 一對多：一個使用者可寫多則評論
		public virtual ICollection<Review> Reviews { get; set; }

		public ApplicationUser()
		{
			CreatedDate = DateTime.Now;
			RevisedDate = DateTime.Now;
			Reviews = new List<Review>();
		}
	}
}
