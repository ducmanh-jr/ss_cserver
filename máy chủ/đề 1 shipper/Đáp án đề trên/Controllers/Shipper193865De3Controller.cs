using ApiWebBasicPlatFrom.Controllers;
using ApiWebBasicPlatFrom.Dtos.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nguyen_Khanh_Thu_193865.Dtos.Shipper193865De3Dto;
using Nguyen_Khanh_Thu_193865.Services.Interfaces;

namespace Nguyen_Khanh_Thu_193865.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Shipper193865De3Controller : ApiControllerBase
    {
        private IShipperServices193865De3 _shipperServices193865;

        public Shipper193865De3Controller(
            IShipperServices193865De3 shipperServices193865,
            ILogger<IShipperServices193865De3> logger
        )
            : base(logger)
        {
            _shipperServices193865 = shipperServices193865;
        }

        [HttpGet]
        public ActionResult GetAll(FilterDto input)
        {
            try
            {
                return Ok(_shipperServices193865.GetAll(input));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("Product")]
        public ActionResult GetProduct(int idShiper)
        {
            try
            {
                return Ok(_shipperServices193865.GetProductMaxByShipper(idShiper));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public ActionResult Create(CreateShipper193865De3Dtos input)
        {
            try
            {
                _shipperServices193865.Create(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        public ActionResult Update(UpdateShipper193865Dto input)
        {
            try
            {
                _shipperServices193865.Update(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpDelete]
        public ActionResult Delete(int idShipper)
        {
            try
            {
                _shipperServices193865.Delete(idShipper);
                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
