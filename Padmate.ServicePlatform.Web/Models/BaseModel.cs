using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Web.Models
{
    public class BaseModel
    {
        #region BootStrap Tables
        public int limit { get; set; }
        //偏移量
        public int offset { get; set; }
        #endregion

        #region BootStrap Paginator
        
        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        #endregion
    }
}