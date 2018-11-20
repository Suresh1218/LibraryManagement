namespace DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryDBModel : DbContext
    {
        public LibraryDBModel()
            : base("LibraryDBModel")
        {
        }

        public LibraryDBModel GetNewInstance()
        {
            var entities = new LibraryDBModel();
            if (entities.Database.Connection.State == System.Data.ConnectionState.Closed)
            {
                entities.Database.Connection.Open();
            }
            return entities;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
        public DbSet<UserLog> User { get; set; }
        public DbSet<Books> Book { get; set; }
    }
}

