using log4net;
using log4net.Appender;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Web.Arrtibutes
{
    /// <summary>
    /// log4net 日志帮助类
    /// </summary>
    public class LogHelp
    {
        /// <summary>
        /// 修改日志存放路径
        /// MVC:/logs/MVC/
        /// API:/logs/API/
        /// </summary>
        /// <param name="logDirectory"></param>
        public static void ChangeLoggerFile(string logDirectory)
        {
            //get the current logging repository for this application 
            ILoggerRepository repository = LogManager.GetRepository();
            //get all of the appenders for the repository 
            IAppender[] appenders = repository.GetAppenders();
            //only change the file path on the 'FileAppenders' 
            foreach (IAppender appender in (from iAppender in appenders
                                            where iAppender is FileAppender
                                            select iAppender))
            {
                FileAppender fileAppender = appender as FileAppender;
                //set the path to your logDirectory using the original file name defined 
                //in configuration 
                fileAppender.File = HttpContext.Current.Server.MapPath("~" + logDirectory);
                //make sure to call fileAppender.ActivateOptions() to notify the logging 
                //sub system that the configuration for this appender has changed. 
                fileAppender.ActivateOptions();
            }
        }

        public const string MVCLogDirectory = "/logs/MVC/Exception";

        public const string APILogDirectory = "/logs/API/Exception";
    }
}