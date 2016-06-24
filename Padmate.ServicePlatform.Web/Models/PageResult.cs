using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Web.Models
{
    public class PageResult<T>
    {
        #region BootStrap Table
        //public int total { get; set; }

        //public List<T> rows { get; set; }

        //public PageResult(int totalCount, List<T> pageRows)
        //{
        //    total = totalCount;
        //    rows = pageRows;
        //}
        #endregion

        #region BootStrap Paginator
        public Int32 totalPages { get; set; }

        public List<T> pageDatas { get; set; }

        public PageResult(Int32 totalpages, List<T> pageResult)
        {
            totalPages = totalpages;
            pageDatas = pageResult;
        }
        #endregion
    }
}