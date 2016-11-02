﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        /// <summary>
        /// 投票编号
        /// </summary>
        public string VoteNo { get; set; }

        /// <summary>
        /// 投票时间
        /// </summary>
        public DateTime VoteTime { get; set; }

        /// <summary>
        /// 浏览器ID
        /// 程序为浏览器生成的GUID，存储在localstorage或者cookies,
        /// 用户清除缓存后消失
        /// </summary>
        public string BrowserId { get; set; }

        /// <summary>
        /// 浏览器指纹(作为识别浏览器唯一性的一个依据，与BrowserId等价)
        /// 根据一些列资源计算出，浏览器插件的改变可能会导致指纹变化
        /// </summary>
        public string BrowserFingerPrint { get; set; }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP { get; set; }
    }
}