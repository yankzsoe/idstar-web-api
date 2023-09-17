using idstar_web_api.Interface;
using Microsoft.AspNetCore.Mvc;

namespace idstar_web_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateNumberController : ControllerBase {
        private readonly HelperNumberGenerator _numberGenerator;
        private readonly INumberGenerator _injectNumberGenerator;

        private readonly ISingletonGenerator _singletonGenerator1;
        private readonly ISingletonGenerator _singletonGenerator2;

        private readonly IScopedGenerator _scopedGenerator1;
        private readonly IScopedGenerator _scopedGenerator2;

        private readonly ITransientGenerator _transientGenerator1;
        private readonly ITransientGenerator _transientGenerator2;

        public GenerateNumberController(INumberGenerator injectNumberGenerator,
            ISingletonGenerator singletonGenerator1,
            ISingletonGenerator singletonGenerator2,
            IScopedGenerator scopedGenerator1,
            IScopedGenerator scopedGenerator2,
            ITransientGenerator transientGenerator1,
            ITransientGenerator transientGenerator2) {
            _numberGenerator = HelperNumberGenerator.GetInstance();
            _injectNumberGenerator = injectNumberGenerator;
            _singletonGenerator1 = singletonGenerator1;
            _singletonGenerator2 = singletonGenerator2;
            _scopedGenerator1 = scopedGenerator1;
            _scopedGenerator2 = scopedGenerator2;
            _transientGenerator1 = transientGenerator1;
            _transientGenerator2 = transientGenerator2;
        }

        [HttpGet("GetRandomNumber")]
        public IActionResult GetRandomNumber() {
            var number = _numberGenerator.GenerateNumber();
            var number2 = _injectNumberGenerator.GenerateNumber();

            var result = new {
                numberFromHelper = number,
                numberFromDI = number2,
            };

            return Ok(result);
        }

        [HttpGet("GetDiffLifetime")]
        public IActionResult GetDiffLifetime() {
            var singletonValue1 = _singletonGenerator1.GenerateNumber();
            var singletonValue2 = _singletonGenerator2.GenerateNumber();

            var scopedValue1 = _scopedGenerator1.GenerateNumber();
            var scopedValue2 = _scopedGenerator2.GenerateNumber();

            var transientValue1 = _transientGenerator1.GenerateNumber();
            var transientValue2 = _transientGenerator2.GenerateNumber();

            var singleton = new {
                value1 = singletonValue1,
                value2 = singletonValue2,
            };

            var scoped = new {
                value1 = scopedValue1,
                value2 = scopedValue2,
            };

            var transient = new {
                value1 = transientValue1,
                value2 = transientValue2,
            };

            var result = new {
                singleton = singleton,
                scoped = scoped,
                transient = transient
            };

            return Ok(result);
        }
    }

    public class HelperNumberGenerator : INumberGenerator {
        private int _number;
        private static HelperNumberGenerator? _helperInstance;
        public int GenerateNumber() {
            return _number++;
        }

        public static HelperNumberGenerator GetInstance() {
            if (_helperInstance == null) {
                _helperInstance = new HelperNumberGenerator();
            }
            return _helperInstance;
        }
    }
}