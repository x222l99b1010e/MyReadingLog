using Microsoft.AspNetCore.Identity;

namespace MyReadingLog.Models
{
	// 必須繼承 IdentityUser，EF 才知道這是 Identity 的會員表
	public class ApplicationUser : IdentityUser
	{
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
