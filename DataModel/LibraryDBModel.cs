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
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<UserCart> Cart { get; set; }
    }
}

