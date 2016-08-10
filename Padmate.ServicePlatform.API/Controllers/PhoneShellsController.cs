using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Padmate.ServicePlatform.API.Controllers
{
    /// <summary>
    /// 智能皮套API
    /// </summary>
    public class PhoneShellsController:BaseApiController
    {
        List<M_PhoneShell> phoneShell = new List<M_PhoneShell>
        { 
            new M_PhoneShell { Id = new Guid().ToString(), IsActivate=true ,ActivateDate = DateTime.Now,PhoneId =new Guid().ToString() }, 
            new M_PhoneShell { Id = new Guid().ToString(), IsActivate=false ,ActivateDate = null,PhoneId =null  }, 
            new M_PhoneShell { Id = new Guid().ToString(), IsActivate=false ,ActivateDate = null,PhoneId =new Guid().ToString()  } 
        };

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<M_PhoneShell> GetAll()
        {
           
            return phoneShell;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/PhoneShells/{id}",Name="GetById")]
        public IHttpActionResult GetById(string id)
        {
            var BB = System.Convert.ToInt16(id);
            var result = phoneShell.FirstOrDefault((p) => p.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model">参数模型对象</param>
        /// <returns>成功：201</returns>
        [HttpPost]
        public IHttpActionResult Add([FromBody] M_PhoneShell model)
        {
            if(string.IsNullOrEmpty(model.Id))
            {
                return BadRequest("Id不能为空");
            }

            phoneShell.Add(model);
            return CreatedAtRoute("GetById", new { id = model.Id }, model);
        }

        /// <summary>
        /// Put
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(string Id,[FromBody] M_PhoneShell model)
        {
            
            return CreatedAtRoute("GetById", new { id = model.Id }, model);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(string Id)
        {

            return Ok();
        }
    }
}