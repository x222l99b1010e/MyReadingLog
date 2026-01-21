using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyReadingLog.Models;

namespace MyReadingLog.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
		// 將 Table 註冊進來!!!!!!!!!!!!!!!
		// IdentityDbContext 已經包含了 ApplicationUser 的 DbSet  
		// 所以不需要再重複宣告一次
		//public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookStatus> BookStatuses { get; set; }
		public DbSet<BookTag> BookTags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<ReviewStatus> ReviewStatuses { get; set; }
		public DbSet<Tag> Tags { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// 必須保留 base，否則 Identity 的設定會消失// Identity 必須保留這行
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Book>()
				.Property(b => b.Title)
				.HasMaxLength(100)
				.IsRequired();
			// Fluent API 配置（如果需要的話）
			// 1. 設定 ISBN 為唯一索引 (Unique Index)
			modelBuilder.Entity<Book>()
				.HasIndex(b => b.ISBN)
				.IsUnique();
			// 2. 設定 Title 索引 (非唯一，純優化搜尋)
			modelBuilder.Entity<Book>()
				.HasIndex(b => b.Title);
			// 3. 設定預設值 (對應 DBML 的 SYSDATETIME)
			//Audit Fields(審計欄位) 的懶人法： 你寫了很多 HasDefaultValueSql("SYSDATETIME()")。
			//這沒問題，但實務上，老手會寫一個 BaseEntity 類別，讓所有 Model 繼承它，
			//並在 DbContext 裡用一個迴圈一次設定完所有表的 CreatedDate。
			//這可以當作你下一階段優化程式碼的練習。
			modelBuilder.Entity<Book>()
				.Property(b => b.CreatedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			modelBuilder.Entity<Book>()
				.Property(b => b.RevisedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			modelBuilder.Entity<ApplicationUser>()
				.Property(u => u.CreatedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			modelBuilder.Entity<ApplicationUser>()
				.Property(u => u.RevisedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			modelBuilder.Entity<Review>()
				.Property(r => r.CreatedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			modelBuilder.Entity<Review>()
				.Property(r => r.RevisedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			// 4. 如果想手動指定外鍵關係（通常 EF 會自動判斷，但手寫更精確）
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Category)
				.WithMany(c => c.Books)
				.HasForeignKey(b => b.CategoryId)
				.OnDelete(DeleteBehavior.Restrict); // 防止刪除分類時連帶刪除書籍;
			// --- Book 與 Creator (建立者) 的關係 ---
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Creator) // Book 實體裡的 Creator 導覽屬性
				.WithMany(u => u.CreatedBooks) // ApplicationUser 裡的 CreatedBooks 集合
				.HasForeignKey(b => b.CreatorId)    // 對應的外鍵欄位
				.OnDelete(DeleteBehavior.Restrict); // 會員想刪帳號時，系統噴錯：「你還有書，不准刪！」 用 Restrict (不允許刪除有書的會員)
			// --- Book 與 Revisor (修改者) 的關係 ---
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Revisor) // Book 實體裡的 Revisor 導覽屬性
				.WithMany(u => u.RevisedBooks)      // ApplicationUser 裡的 RevisedBooks 集合 //後續有補上User的RevisedBooks ICollection
				.HasForeignKey(b => b.RevisorId)    // 對應的外鍵欄位
				.OnDelete(DeleteBehavior.SetNull); //1.SetNull 會員刪帳號後，書評還在，但「作者」欄位變 Null。 (人走了，書還在但修改者變空)
												   //2.Restrict 會員想刪帳號時，系統噴錯：「你還有書評，不准刪！」
												   //3.Cascade 會員一刪帳號，他寫過的 100 篇書評全部消失。
			// --- Book 與 BookStatus 的關係 ---
			// BookStatus 沒有對應的 ICollection<Book> 屬性
			// 所以用 .WithMany() 而不是 .WithMany(bs => bs.Books)
			// 未來如果要擴充狀態管理，防止刪除狀態時連帶刪除書籍，因此使用 Restrict
			// 目前基本上無意義
			modelBuilder.Entity<Book>()
				.HasOne(b => b.BookStatus) // Book 有一個狀態
				.WithMany() // 狀態對應多本書，但 BookStatus 類別裡沒有集合屬性
				.HasForeignKey(b => b.BookStatusId)
				.OnDelete(DeleteBehavior.Restrict); // 未來如果要擴充狀態管理，防止刪除狀態時連帶刪除書籍;

			modelBuilder.Entity<Review>()
				.HasOne(r => r.ApplicationUser)
				.WithMany(u => u.Reviews)
				.HasForeignKey(r => r.ApplicationUserId)
				.OnDelete(DeleteBehavior.SetNull); // 會員刪帳號後，評論還在，但「評論者」欄位變 Null。
			// 1. 設定 BookTag 的複合主鍵
			// 複合主鍵 //如字面意思  有key=> bookid and tagid
			modelBuilder.Entity<BookTag>()
				.HasKey(bt => new { bt.BookId, bt.TagId });
			modelBuilder.Entity<Tag>()
				.HasIndex(t => t.TagName)
				.IsUnique();// 標籤名稱通常也是唯一的

			
			// 2. 設定 Review 的複合唯一索引 (一人一書一評)
			// 如字面意思  有Index=> bookid and applicationuserid 而且是唯一
			modelBuilder.Entity<Review>()
				.HasIndex(r => new { r.BookId, r.ApplicationUserId })
				.IsUnique();
		}
	}
    
}
