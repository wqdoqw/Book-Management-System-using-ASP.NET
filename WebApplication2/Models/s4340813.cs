namespace WebApplication2.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Infrastructure;

    public class Book
    {

        
        public int ID { get; set; }

        [Required]
        [Display(Name = "Book ID")]
        public int BookID { get; set; }

        [Required]
        public string Name { get; set;  }

        [Required]
        public string Author { get; set;  }

        [Required]
        public string Year { get; set;  }

        [Required]
        public decimal Price { get; set;  }

        [Required]
        public int Stock { get; set;  }

    }



    public class s4340813 : DbContext
    {
        // Your context has been configured to use a 's4340813' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApplication2.Models.s4340813' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 's4340813' 
        // connection string in the application configuration file.
        public s4340813()
            : base("name=s4340813")
        {
        }

        public System.Data.Entity.DbSet<WebApplication2.Models.Book> Books { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

}