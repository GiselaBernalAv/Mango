using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Mango.Services.Coupons.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly AppMangoDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;

        public CouponsController(AppMangoDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response= new ResponseDto();
        }

        //public ActionResult Get()
        // public object Get()
        [HttpGet]
        public ResponseDto GetAllCoupons()
        {
            try
            {
                IEnumerable<Coupon>? objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
              //  return objList;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto GetCouponById(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u=>u.CouponId==id);
                //return obj;
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
             return _response;
            //return null;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public object GetCouponByCode(string code)
        {
            try
            {
                Coupon objl = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                 _response.Result = _mapper.Map<CouponDto>(objl);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
              return _response;
        }

        [HttpPost]
        public ResponseDto AddCoupons(CouponDto couponDto)
        {
            try
            {
                
                Coupon objg = _mapper.Map<Coupon>(couponDto);
                 _db.Coupons.Add(objg);
                 _db.SaveChanges();   
                _response.Result = _mapper.Map<CouponDto>(objg);
            }
            catch(Exception ex)
            {
                 _response.IsSuccess = false;
                 _response.Message = ex.Message;
            }
        
            return _response;
        }
        [HttpPut]
        public ResponseDto UpdateCoupons(CouponDto couponDto)
        {
            try
            {

                Coupon objg = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(objg);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(objg);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto DeleteCoupons(int id)
        {
            try
            {

                Coupon objg = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(objg);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(objg);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
