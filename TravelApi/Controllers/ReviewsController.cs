using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using TravelApi.Models;
using TravelApi.Services;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  public class ReviewsController : ControllerBase
  {
    private TravelApiContext _db;

    public ReviewsController(TravelApiContext db)
    {
      _db = db;
    }

    // GET api/reviews
    [HttpGet]
    public ActionResult<IEnumerable<Review>> Get(string country, string city, string landmark)
    {
      var query = _db.Reviews.AsQueryable();

      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      if (landmark != null)
      {
        query = query.Where(entry => entry.Landmark == landmark);
      }

      return query.OrderByDescending(review => review.Rating).ToList();

    }

    // GET api/reviews/3
    [HttpGet("{id}")]
    public ActionResult<Review> Get(int id)
    {
      return _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
    }

    // POST api/reviews
    [Authorize]
    [HttpPost]
    public void Post([FromBody] Review review)
    {
      _db.Reviews.Add(review);
      _db.SaveChanges();
    }

    // PUT api/reviews/3
    [Authorize]
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Review review)
    {
      review.ReviewId = id;
      _db.Entry(review).State = EntityState.Modified;
      _db.SaveChanges();
    }

    // DELETE api/reviews/3
    [Authorize]
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var reviewToDelete = _db.Reviews.FirstOrDefault(entry => entry.ReviewId == id);
      _db.Reviews.Remove(reviewToDelete);
      _db.SaveChanges();
    }

    // GET Review of random destination => api/reviews/random
    [HttpGet("random")]
    public ActionResult<Review> Random ()
    {
      List<Review> reviews = _db.Reviews.ToList();
      var rnd = new Random();
      int rndIdx = rnd.Next(0, reviews.Count-1);
      return reviews[rndIdx];
    }
  }
}    