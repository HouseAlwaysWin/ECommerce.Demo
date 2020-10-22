using System.Collections.Generic;
using API.Domain.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class ProductController : ControllerBase {
        private IProductService _productService;
        public ProductController (IProductService productService) {
            _productService = productService;
        }

    }
}