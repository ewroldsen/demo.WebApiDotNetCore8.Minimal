using System.ComponentModel.DataAnnotations;

namespace demo.WebApiDotNetCore8.Minimal.Models
{
   public class Court
   {

      public int Id { get; set; }
      [MaxLength(20)]
      public string? CourtType { get; set; }

      public int? NumbOfCourts { get; set; }
      public string? Title { get; set; }
      [MaxLength(150)]
      public string? StreetAddress { get; set; }

      [MaxLength(50)]
      public string? City { get; set; }

      [MaxLength(2)]
      public string? StateAbrev { get; set; }

      [MaxLength(5)]
      public string? ZipCode { get; set; }
      // lat/lng precision not required.
      [MaxLength(12)]
      public string? Latitude { get; set; }
      [MaxLength(12)]
      public string? Longitude { get; set; }
   }
}
