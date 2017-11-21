using Microsoft.EntityFrameworkCore;

namespace ContactService.Model {
    public class ContactModel : DbContext {
        public ContactModel() {
        }

        public ContactModel(DbContextOptions<ContactModel> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Contact>().ToTable("Contact");
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
