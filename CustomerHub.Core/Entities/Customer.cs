using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CustomerHub.Core.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string FullName { get; set; }
        //Use Datetimes beacuse Serialization and deserialization of 'System.DateOnly' instances are not supported in .Net 6
        public DateTime DateOfBirth { get; set; }

        [MaxLength(10000)]
        public string? SvgImage { get; set; }


    }
}
