using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Utility
{
    public static class Common
    {
        #region 文章类型

        public const string Activity = "activity";

        /// <summary>
        /// 活动预告
        /// </summary>
        public const string ActivityForecast = "activityforecast";

        /// <summary>
        /// 精彩活动
        /// </summary>
        public const string WonderfulActivity = "wonderfulactivity";

        /// <summary>
        /// 资讯
        /// </summary>
        public const string Information = "information";

        public static Dictionary<string, string> Dic_ArticleType = new Dictionary<string, string>(){
            {ActivityForecast,"活动预告"},
            {WonderfulActivity,"精彩活动"},
            {Information,"资讯"}
        };
        #endregion
        #region 图片类型

        public const string Image_HomeBG = "homebg";
        public const string Article_Thumbnails = "article_thumbnails";
        public const string Project_Thumbnails = "project_thumbnails";
        public const string ProjectAttachment_Thumbnails = "projectattachment_thumbnails";

        public static Dictionary<string, string> Dic_ImageType = new Dictionary<string, string>(){
            {Image_HomeBG,"首页背景图片"},
            {Article_Thumbnails,"文章缩略图"},
            {Project_Thumbnails,"项目缩略图"},
            {ProjectAttachment_Thumbnails,"项目附件缩略图"}

        };
        #endregion

        #region 项目类型

        public const string ZZ_Project = "zz_project";
        public const string Other_Project = "other_project";

        public static Dictionary<string, string> Dic_ProjectType = new Dictionary<string, string>(){
            {ZZ_Project,"众创项目"},
            {Other_Project,"其它项目"}


        };
        #endregion
        
    }

    public static class SystemRole
    {
        public const string Admin = "Admin";
    }


    
}