namespace MicroCredential.Infrastructure.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        public string Name { get; set; }
        
        public short Age { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string Country { get; set; }
    }
}
