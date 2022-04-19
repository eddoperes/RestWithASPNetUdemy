using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
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

        [HttpGet("subtract/{firstNumber}/{secondNumber}")]
        public IActionResult Subtract(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var number = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("multiply/{firstNumber}/{secondNumber}")]
        public IActionResult Multiply(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var number = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var number = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("average/{firstNumber}/{secondNumber}")]
        public IActionResult Average(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var number = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input");
        }

        [HttpGet("square/{firstNumber}")]
        public IActionResult Square(string firstNumber)
        {
            if (IsNumeric(firstNumber))
            {
                var number = Math.Sqrt ((double) ConvertToDecimal(firstNumber));
                return Ok(number.ToString());
            }
            return BadRequest("Invalid input");
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool isNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

    }
}