using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyReadingLog.Models
{
	// 必須繼承 IdentityUser，EF 才知道這是 Identity 的會員表
	public class ApplicationUser : IdentityUser
	{
		[MaxLength(20)]
		public string NickName { get; set; }
		[MaxLength(500)]
		public string ProfilePicturePath { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime RevisedDate { get; set; }
		public bool IsDeleted { get; set; }
		// 一對多：一個使用者可寫多則評論
		public virtual ICollection<Review> Reviews { get; set; }
		// 一對多：一個使用者可建立多本書
		public virtual ICollection<Book> CreatedBooks { get; set; }
		// 一對多：一個使用者可修改多本書
		public virtual ICollection<Book> RevisedBooks { get; set; }

		public ApplicationUser()
		{
			Reviews = new List<Review>();
		}
	}
}
