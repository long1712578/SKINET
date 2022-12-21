using AutoMapper;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Intrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SKINET.Dtos;
using SKINET.Helpers;
using System.Reflection.Metadata;

namespace SKINET.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalCount = await _productRepo.CountAsync(countSpec);
            var products = await _productRepo.GetListAsync(spec);
            var productsToReturns = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var result = new Pagination<ProductToReturnDto>
            {
                Count = totalCount,
                Data = productsToReturns,
                PageIndex = productParams.PageIndex,
                PageSize = productParams.PageSize
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ProductToReturnDto?> GetProductById(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _productRepo.GetEntityWithSpecification(spec);
            var productDto = _mapper.Map<Product, ProductToReturnDto>(product);
            return productDto;
        }
        [HttpGet("brand")]
        public async Task<ActionResult<ProductBrand?>> GetBrands()
        {
            return Ok(await _productBrandRepo.GetAllAsync());
        }
        [HttpGet("type")]
        public async Task<ActionResult<ProductType?>> GetTypes()
        {
            return Ok(await _productTypeRepo.GetAllAsync());
        }
    }
}
