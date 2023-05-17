using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetPalz.Data;
using PetPalz.Models;
using PetPalz.Models.Dtos;

namespace PetPalz.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController:ControllerBase
{
    private PetPalzContext _context;
    private IConfiguration _configuration;
    private UserManager<IdentityUser> _userManager;

    public ReviewsController(PetPalzContext context, IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpGet("{userId}")]
    public ActionResult GetUserReviews(string userId)
    {
        var reviews = _context.UserReviews.AsEnumerable().Where(x => x.UserId == userId).ToList();
        if (reviews.Count < 1) return BadRequest();
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult> PostUserReview(ReviewDto review)
    {
        var postUser = await _userManager.FindByNameAsync(User.Identity.Name);
        await _context.UserReviews.AddAsync(new UserReviews()
        {
            Name = review.Message,
            PostUserId = postUser.Id,
            UserId = review.UserId,
            Rating = review.Rating
        });
        await _context.SaveChangesAsync();
        var userRating = _context.UserRatings.AsEnumerable().First(x => x.UserId == review.UserId);
        var result = _context.UserRatings.Update(userRating);
        var userReviews = _context.UserReviews.AsEnumerable().Where(x => x.UserId == review.UserId);
        if(userReviews.ToList().Count > 0)result.Entity.Rating = userReviews.Select(x => x.Rating).Sum() / userReviews.Count();
        await _context.SaveChangesAsync();
        return Ok("Review Created");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReview(int id)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        
        UserReviews? review = _context.UserReviews.AsEnumerable().FirstOrDefault(x => x.Id == id);
        if (review == null) return BadRequest();
        if (user.Id != review.PostUserId)
            return BadRequest();
        var userRating = _context.UserRatings.AsEnumerable().First(x => x.UserId == review.UserId);
        _context.UserReviews.Remove(review);
        await _context.SaveChangesAsync();
        var result = _context.UserRatings.Update(userRating);
        var userReviews = _context.UserReviews.AsEnumerable().Where(x => x.UserId == result.Entity.UserId);
        result.Entity.Rating = userReviews.Count() ==0 ? 0 : userReviews.Select(x => x.Rating).Sum() / userReviews.Count();
        await _context.SaveChangesAsync();
        return Ok("Review Deleted");
    }
}