

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AllForAll.Models;
using BusinessLogic.Dto.Feedback;
using BusinessLogic.Interfaces;

namespace AllForAll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<IActionResult> GetFeedbacks(CancellationToken cancellationToken)
        {
            var feedbacks = await _feedbackService.GetAllFeedbacksAsync(cancellationToken);
            return Ok(feedbacks);
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback([FromRoute] int id, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id, cancellationToken);
            if (feedback == null)
            {
                return NotFound();
            }
            return Ok(feedback);
        }

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] FeedbackRequestDto feedbackDto, CancellationToken cancellationToken)
        {
            var feedbackId = await _feedbackService.CreateFeedbackAsync(feedbackDto, cancellationToken);
            return Ok(feedbackId);
        }

        // PUT: api/Feedback
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback([FromRoute] int id, [FromBody] FeedbackRequestDto feedbackDto, CancellationToken cancellation = default)
        {
            await _feedbackService.UpdateFeedbackAsync(id, feedbackDto, cancellation);
            return NoContent();
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback([FromRoute] int id, CancellationToken cancellation = default)
        {
            await _feedbackService.DeleteFeedbackAsync(id, cancellation);
            return NoContent();
        }

    }
}
