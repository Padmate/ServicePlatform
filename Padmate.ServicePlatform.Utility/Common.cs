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
            {UserType_Team,"团队"},            
            {UserType_Enterprise,"企业"},
            {UserType_Personal,"个人"} 
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
        #region 行业领域
        /// <summary>
        /// 智能硬件
        /// </summary>
        public const string FieldScope_IntelligentHardware = "intelligenthardware";

        /// <summary>
        /// 机器人
        /// </summary>
        public const string FieldScope_Robot = "robot";

        /// <summary>
        /// 物联网
        /// </summary>
        public const string FieldScope_Internet = "internet";

        /// <summary>
        /// 大数据
        /// </summary>
        public const string FieldScope_BigData = "bigdata";

        /// <summary>
        /// AR /VR
        /// </summary>
        public const string FieldScope_ARVR = "arvr";

        /// <summary>
        /// 其它
        /// </summary>
        public const string FieldScope_Other = "other";


        public static Dictionary<string, string> Dic_FieldScope = new Dictionary<string, string>(){
            {FieldScope_IntelligentHardware,"智能硬件"},
            {FieldScope_Robot,"机器人"},
            {FieldScope_Internet,"物联网"},
            {FieldScope_BigData,"大数据"},
            {FieldScope_ARVR,"AR/VR"},
            {FieldScope_Other,"其它"}
        };
        #endregion

        #region 项目阶段
        /// <summary>
        /// 创意
        /// </summary>
        public const string ProjectStage_CY = "cy";

        /// <summary>
        /// 研发
        /// </summary>
        public const string ProjectStage_YF = "yf";

        /// <summary>
        /// 产品开发
        /// </summary>
        public const string ProjectStage_CPKF = "cpkf";

        /// <summary>
        /// 试运营
        /// </summary>
        public const string ProjectStage_SYY = "syy";

        /// <summary>
        /// 市场拓展
        /// </summary>
        public const string ProjectStage_SCTZ = "sctz";

        public static Dictionary<string, string> Dic_ProjectStage = new Dictionary<string, string>(){
            {ProjectStage_CY,"创意"},
            {ProjectStage_YF,"研发"},
            {ProjectStage_CPKF,"产品开发"},
            {ProjectStage_SYY,"试运营"},
            {ProjectStage_SCTZ,"市场拓展"}
        };
        #endregion
        #region 用户附件类型
        /// <summary>
        /// 营业执照
        /// </summary>
        public const string UserAttachment_BusinessLicense = "1";
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