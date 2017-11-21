using Microsoft.EntityFrameworkCore;

namespace ContactService.Model {
    public class ContactModel : DbContext {

        private string connectionString;
        public ContactModel() {
        }

        public ContactModel(DbContextOptions<ContactModel> options) : base(options) { }
        public ContactModel(string conStr) {
            connectionString = conStr;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Contact>().ToTable("Contact");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                if (!string.IsNullOrEmpty(connectionString))
                    optionsBuilder.UseSqlServer(connectionString);
                else
                    optionsBuilder.UseInMemoryDatabase(databaseName: "ContactsService");
            }
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
