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
        #region 用户类型

        /// <summary>
        /// 个人用户
        /// </summary>
        public const string UserType_Personal = "personal";

        /// <summary>
        /// 团队用户
        /// </summary>
        public const string UserType_Team = "team";

        /// <summary>
        /// 企业级用户
        /// </summary>
        public const string UserType_Enterprise = "enterprise";

        public static Dictionary<string, string> Dic_UserTypes = new Dictionary<string, string>(){
            {UserType_Personal,"个人"},    
            {UserType_Team,"团队"},
            {UserType_Enterprise,"企业"}
        };
        #endregion
        #region 审核状态
        /// <summary>
        /// 审核中
        /// </summary>
        public const string Audit_Waiting = "0";

        /// <summary>
        /// 审核通过
        /// </summary>
        public const string Audit_Success = "1";

        /// <summary>
        /// 审核失败
        /// </summary>
        public const string Audit_Failue = "3";

        public static Dictionary<string, string> Dic_Audit = new Dictionary<string, string>(){
            {Audit_Waiting,"等待审核"},
            {Audit_Success,"审核通过"},
            {Audit_Failue,"审核失败"}
        };
        #endregion
    }

    public static class SystemRole
    {
        /// <summary>
        /// 系统管理员(所有权限)
        /// </summary>
        public const string SystemAdmin = "SystemAdmin";

        /// <summary>
        /// 后台管理员
        /// </summary>
        public const string BackstageAdmin = "BackstageAdmin";


        public static Dictionary<string, string> Roles = new Dictionary<string, string>(){
            {SystemAdmin,"系统管理员"},
            {BackstageAdmin,"后台管理员"}
        };

    }


    
}