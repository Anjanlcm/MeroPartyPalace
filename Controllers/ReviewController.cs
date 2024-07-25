using MeroPartyPalace.Model;
using MeroPartyPalace.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeroPartyPalace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        [HttpPost("Add Review")]
        public IActionResult addReview(Review review)
        {
            ReviewRepository reviewRepository = new ReviewRepository();
            int id = reviewRepository.addReview(review);
            if (id != null)
            {
                return Ok("Added review successfully");
            }
            else
            {
                return Ok("Added review Unsuccessfully");

            }
        }

        [HttpGet("Get Reviews")]
        public IActionResult GetReview([FromQuery] int VenueId)
        {
            ReviewRepository reviewRepository = new ReviewRepository();
            List<Review> reviews = new List<Review>();
            reviews = reviewRepository.GetReview(VenueId);
            return Ok(reviews);
        }

        [HttpPost("Modify Review")]
        public IActionResult modifyReview(Review review)
        {
            Review review1 = new Review();
            ReviewRepository reviewRepository = new ReviewRepository();
            review1 = reviewRepository.modifyReview(review);
            return Ok(review1);
        }

        [HttpPost("Delete Review")]
        public IActionResult DeleteReview(int id)
        {
            ReviewRepository reviewRepository = new ReviewRepository();
            if(reviewRepository.DeleteReview(id))
            {
                return Ok("Deleted Successfully");
            }
            else
            {
                return Ok("Shit");
            }
                
        }

    }
}
