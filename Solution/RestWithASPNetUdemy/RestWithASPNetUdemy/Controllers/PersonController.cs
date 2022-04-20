using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber)) 
            {
                var number = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input"); 
        }


    }
}