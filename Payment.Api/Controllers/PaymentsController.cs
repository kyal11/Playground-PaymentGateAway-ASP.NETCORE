using Microsoft.AspNetCore.Mvc;
using Payment.Application.Interfaces;
using Payment.Domain.DTO;

namespace Payment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var payment = await _paymentService.GetPaymentByIdAsync(id);

        if (payment== null)
        {
            return NotFound(new { message = $"Payment with {id} not found!" });
        }

        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
    {
        try
        {
            var payment = await _paymentService.CreatePaymentAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        } catch ( ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        } catch (Exception ex)
        {
            _logger.LogError(ex, "This request cant make payment/Something wrong");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> CompletePayment(Guid id)
    {
        var result = await _paymentService.CompletePaymentAsync(id);

        if (!result)
        {
            return NotFound(new { message = "Fail process, Data payment not found!" });
        }
        return Ok(new { message = "Payment successfuly completed!" });
    }
}
