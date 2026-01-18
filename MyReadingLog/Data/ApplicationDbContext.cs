using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyReadingLog.Models;

namespace MyReadingLog.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
		// 將 Table 註冊進來!!!!!!!!!!!!!!!
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookTag> BookTags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<Status> Statuses { get; set; }
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
			modelBuilder.Entity<Book>()
				.Property(b => b.CreatedDate)
				.HasDefaultValueSql("SYSDATETIME()");
			// 4. 如果想手動指定外鍵關係（通常 EF 會自動判斷，但手寫更精確）
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Category)
				.WithMany(c => c.Books)
				.HasForeignKey(b => b.CategoryId)
				.OnDelete(DeleteBehavior.Restrict); // 防止刪除分類時連帶刪除書籍;
			modelBuilder.Entity<Book>()
				.HasOne(b => b.RevisorId)
				.WithMany()// 使用者可以修改很多本書
				.HasForeignKey(b => b.Revisor)
				.OnDelete(DeleteBehavior.Restrict); // 防止刪除使用者時連帶刪除書籍;
													//「如果你想刪除這個使用者，但這本書還掛在他名下，不准刪！」
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
