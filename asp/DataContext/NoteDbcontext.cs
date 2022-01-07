using asp.Models;
using Microsoft.EntityFrameworkCore;

namespace asp.DataContext
{
    public class NoteDbcontext : DbContext //상속을 할떄 : 을 사용, Dbcontext를 상속, 뒤쪽이 부모
    {
   
        public DbSet<User> Users { get; set; } //테이블 생성
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //db연결
        {
            optionsBuilder.UseSqlServer("Data Source=kuniv-practice.database.windows.net;Initial Catalog=kuniv-practice;User ID=kuniv;Password=tkdgkdlqkswja123@@;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public NoteDbcontext(DbContextOptions<NoteDbcontext> options)
    : base(options)
        { 
        }

        public NoteDbcontext()
        {
        }
    }
}

