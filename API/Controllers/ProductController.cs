using System.Collections.Generic;
using ECommerce.Demo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ECommerce.Demo.API.Controllers {
    public class ProductController : ControllerBase {
        private IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController (IProductService productService, ILogger<ProductController> logger) {
            _productService = productService;
            _logger = logger;
            _logger.LogDebug (1, "Test logging");
        }

        [HttpGet ("Hello")]
        public IActionResult Hello () {
            _logger.LogInformation ("hello");
            return Ok ("Hello");
        }

    }
}