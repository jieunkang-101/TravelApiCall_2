using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public TravelApiContext(DbContextOptions<TravelApiContext> options)
        : base(options)
    {
    }

    public DbSet<Review> Reviews { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Review>()
        .HasData(
          new Review { ReviewId =1, Author = "user1", Country = "France", City = "Paris", Landmark = "Louvre Museum", Description = "I loved this place. If your are an art lover, this place is a paradise for you. One day is not enough to visit the entire museum. But still you can visit the main attractions including the famous Mona Lisa.", Rating = 5 },
          new Review { ReviewId =2, Author = "user2", Country = "Australia", City = "Sydney", Landmark = "Opera House", Description = "Wow! Sydney Opera House is absolutely FABULOUS! What an elegant, gorgeous, impressive display of truly artistic architecture.  The beauty of this site is enhanced with each different view.", Rating = 4 },
          new Review { ReviewId =3, Author = "user2", Country = "USA", City = "Portland", Landmark = "Pittock Mansion", Description = "This was a great way to get to know the city of Portland! It was fascinating to learn about one of the first wealthy families to live there. The mansion is really cool and the hike through forest park is a fun way to get there!", Rating = 4 },
          new Review { ReviewId =4, Author = "user1", Country = "Thailand", City = "Bangkok", Landmark = "Temple of the Emerald Buddha", Description = "A place to be definitely included in must visit tourist destination. Place is well maintained and mermerizing. It is my first time to see buildings crafted with real gold.", Rating = 3 },
          new Review { ReviewId =5, Author = "user3", Country = "Korea", City = "Seoul", Landmark = "Gyeongbokgung Palace", Description = "It is one of the best places on South Korea to see the old architecture of a palace. Even traditional events are demonstrate times to time. The most interesting thing in this place is that you can take photographs with soldiers with traditional costumes.", Rating = 5 }
        );       

      builder.Entity<User>()
        .HasData(
          new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
          new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User } 
        );
    }
  }
}  