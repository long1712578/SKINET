using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SKINET.Controllers
{
    public class BasketController : BaseApiController
    {
        private IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository  = basketRepository;
        }

        /// <summary>
        /// Get basket by i
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var customerBasket = await _basketRepository.GetBasketAsync(id);
            return Ok(customerBasket ?? new CustomerBasket(id));
        }

        /// <summary>
        /// basket upadte
        /// </summary>
        /// <param name="basketUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basketUpdate)
        {
            var customerBasket = await _basketRepository.UpdateBasketAsync(basketUpdate);
            return Ok(customerBasket);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasketById(string id)
        {
            var isDelete = await _basketRepository.DeleteBasketAsync(id);
            if (isDelete)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
