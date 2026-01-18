using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyReadingLog.Models;

namespace MyReadingLog.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {

		public DbSet<Book> Books { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Status> Statuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder); // Identity 必須保留這行
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
		}
	}
    
}
