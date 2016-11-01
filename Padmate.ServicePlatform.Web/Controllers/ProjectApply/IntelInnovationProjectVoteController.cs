using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.ProjectApply
{
    public class IntelInnovationProjectVoteController:BaseController
    {
        /// <summary>
        /// 投票界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            B_IntelInnovationProjectApply bProject = new B_IntelInnovationProjectApply();

            M_VoteConfig voteConfig = bProject.ReadVoteConfiguration();

            var voteStartTime = System.Convert.ToDateTime(voteConfig.VoteStartTime);
            var voteEndTime = System.Convert.ToDateTime(voteConfig.VoteEndTime);
            TimeSpan nowtimespan = new TimeSpan(DateTime.Now.Ticks);
            

            
            //判断投票是否已经开始
            var isStartVote = false;
            if (DateTime.Compare(DateTime.Now, voteStartTime) > 0)
            {
                isStartVote = true;
                

            }
            ViewData["isStartVote"] = isStartVote;

            //判断是否已经停止投票
            var isEndVote = false ;
            if(DateTime.Compare(DateTime.Now,voteEndTime) >0)
            {
                isEndVote = true;
            }
            ViewData["isEndVote"] = isEndVote;
           
            ViewData["voteconfig"] = voteConfig;

            //投票倒计时间
            //默认以倒计时投票截止时间
            var countdownTime = voteEndTime;
            if(!isStartVote)
            {
                //如果投票还未开始，则倒计时开始时间
                countdownTime = System.Convert.ToDateTime(voteConfig.VoteStartTime);
            }
            TimeSpan counttimespan = new TimeSpan(countdownTime.Ticks);
            TimeSpan timespan = nowtimespan.Subtract(counttimespan).Duration();
            ViewData["counttime"] = JsonHandler.ToJson(timespan.TotalSeconds);
            return View();

        }

        /// <summary>
        /// 投票详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(string voteno)
        {
            //重新查找数据
            B_IntelInnovationProjectApply bProject = new B_IntelInnovationProjectApply();
            var project = bProject.GetIntelInnovationProjectApplyByVoteNo(voteno);
            if(project == null)
            {
                throw new HttpException(404, "");
            }

            ViewData["project"] = project;


            M_VoteConfig voteConfig = bProject.ReadVoteConfiguration();
            ViewData["voteconfig"] = voteConfig;

            var voteStartTime = System.Convert.ToDateTime(voteConfig.VoteStartTime);
            var voteEndTime = System.Convert.ToDateTime(voteConfig.VoteEndTime);

            //判断投票是否已经开始
            var isStartVote = false;
            if (DateTime.Compare(DateTime.Now, voteStartTime) > 0)
            {
                isStartVote = true;
            }
            ViewData["isStartVote"] = isStartVote;

            //判断是否已经停止投票
            var isEndVote = false;
            if (DateTime.Compare(DateTime.Now, voteEndTime) > 0)
            {
                isEndVote = true;
            }
            ViewData["isEndVote"] = isEndVote;


            return View();

        }

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page">当前所在页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData(M_IntelInnovationProjectApplySearch searchModel)
        {
            //每页显示数据条数
            int limit = 16;
            searchModel.limit = limit;

            B_IntelInnovationProjectApply bApply = new B_IntelInnovationProjectApply();
            var pageData = bApply.GetPageDataForVote(searchModel);
            var totalCount = bApply.GetPageDataTotalCountForVote(searchModel);
            //总页数
            var totalPages = System.Convert.ToInt32(Math.Ceiling((double)totalCount / limit));

            PageResult<M_IntelInnovationProjectApplySearch> result = new PageResult<M_IntelInnovationProjectApplySearch>(totalCount, totalPages, pageData);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetVoteResult()
        {
            B_IntelInnovationProjectApply bApply = new B_IntelInnovationProjectApply();

            var voteResult = bApply.GetVoteResult();

            return Json(voteResult);
        }
        
        /// <summary>
        /// 匿名投票
        /// </summary>
        /// <param name="ProjectId">投票项目ID</param>
        /// <param name="BrowserId">浏览器Id</param>
        /// <param name="FingerPrint">浏览器指纹</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Vote(string ProjectId, string BrowserId, string FingerPrint, bool HasEmptyCache)
        {
            Message message = new Message();
            B_IntelInnovationProjectApply bProject = new B_IntelInnovationProjectApply();

            M_VoteConfig voteConfig = bProject.ReadVoteConfiguration();

            #region 校验
            //判断投票是否已经结束
            var voteStartTime = System.Convert.ToDateTime(voteConfig.VoteStartTime);
            var voteEndTime = System.Convert.ToDateTime(voteConfig.VoteEndTime);
            //判断是否已经开始投票
            if (DateTime.Compare(DateTime.Now, voteStartTime) < 0)
            {
                message.Success = false;
                message.Content = "投票还未开始，不能再进行投票。";
                return Json(message);
            }
            //判断是否已经停止投票
            if (DateTime.Compare(DateTime.Now, voteEndTime) > 0)
            {
                message.Success = false;
                message.Content = "投票已结束，不能再进行投票。";
                return Json(message);
            }


            if (string.IsNullOrEmpty(ProjectId))
            {
                message.Success = false;
                message.Content = "获取项目信息失败，请刷新重试。";
                return Json(message);
            }
            #endregion

            //获取当前project信息
            var project =  bProject.GetIntelInnovationProjectApplyById(ProjectId);

            //一个用户某个时间间隔内可以投票的次数
            int intervalEachVotes = System.Convert.ToInt32(voteConfig.EachDayVoteTimes);
            //投票时间间隔（分钟）
            int voteInterval = System.Convert.ToInt32(voteConfig.VoteInterval);

            bool hasVoteRight = true;
            //判断是不是违法操作
            bool isIllegalOperator = false;
            #region 校验当前匿名用户是否有权投票
            //获取客户端IP
            var clientIP = Client.GetHostAddress();
            B_Vote bVote = new B_Vote();

            //查询该FingerPrint该时间间隔的投票次数
            var todayVotesByFingerprint = bVote.GetIntervalVotesByFingerPrint(FingerPrint,voteInterval);
            if(string.IsNullOrEmpty(BrowserId))
            {
                //如果BrowserId为空(这种情况下用户禁用了缓存)
                //则根据FingerPrint校验
                if(todayVotesByFingerprint >= intervalEachVotes)
                {
                    hasVoteRight = false;
                }
            }
            else
            {
                //如果BrowserId不为空（这种情况下可以使用浏览器缓存，但是用户有可能通过清空缓存进行刷票）

                //如果判断出用户进行了清空缓存操作
                if(HasEmptyCache)
                {
                    //如果已经存在的相同IP和相同FingerPrint的数据超过了该时间段的投票票数，则把此次投票当做刷票
                    var todayVotesByIPAndFingerPrint = bVote.GetIntervalVotesByClientIPAndFingerPrint(FingerPrint,clientIP,voteInterval);
                    if (todayVotesByIPAndFingerPrint >= intervalEachVotes)
                    {
                        hasVoteRight = false;
                        isIllegalOperator = true;
                    }
                }
                
                //继续判断
                if(hasVoteRight)
                {
                    //查询该BrowserId某个时间间隔的投票次数
                    var todayVotesByBrowserId = bVote.GetIntervalVotesByBrowserId(BrowserId, voteInterval);
                    //如果投票次数大于今天的投票次数，则不能再投票
                    if (todayVotesByBrowserId >= intervalEachVotes)
                    {
                        hasVoteRight = false;
                    }

                }
                
                
            }

            
            #endregion

            if(!hasVoteRight)
            {
                var intervalDay = voteInterval / (24 * 60);
                var intervalMinute = voteInterval % (24 * 60);

                var dayTip = intervalDay == 0 ? "" : intervalDay + "天";
                var minuteTip = intervalMinute == 0 ? "" : intervalMinute + "分钟";
                message.Success = false;
                message.ReturnStrId = isIllegalOperator.ToString().ToLower();
                message.Content = string.Format("您已经投过票了，{0}{1}内只能投{2}票",dayTip,minuteTip,intervalEachVotes);
                return Json(message);
            }

            M_Vote vote = new M_Vote();
            vote.VoteNo = project.VoteNo;
            vote.VoteTime = DateTime.Now;
            vote.BrowserId = BrowserId;
            vote.ClientIP = clientIP;
            vote.BrowserFingerPrint = FingerPrint;

            //进行投票
            //message.ReturnId返回当前票数
            B_IntelInnovationProjectApply bApply = new B_IntelInnovationProjectApply();
            message = bApply.DoVotes(ProjectId, vote);
            

            return Json(message);
        }

        [HttpPost]
        public ActionResult GetVotesByVoteNo(string VoteNo)
        {
            B_Vote bVote = new B_Vote();
            M_Vote model = new M_Vote();
            model.VoteNo = VoteNo;

            var votes = bVote.GetByMulitCond(model);
            return Json(votes);
        }

        /// <summary>
        /// 获取图表X Y轴数据
        /// </summary>
        /// <param name="VoteNo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetChartXYAxis(string VoteNo)
        {
            #region 图标数据分析

            B_Vote bVote = new B_Vote();
            M_Vote model = new M_Vote();
            model.VoteNo = VoteNo;

            var votes = bVote.GetByMulitCond(model);

            //先按时间进行分组
            var timeGroup = votes.GroupBy(v => v.VoteTime.ToString("yyyy-MM-dd"));
            //获取图表X轴,单位为/天
            var chartXAxis = timeGroup.Select(v => v.First().VoteTime.ToString("yyyy-MM-dd"))
                .OrderBy(v => v)
                .ToArray();

            //根据IP分组
            var ipGroup = votes.GroupBy(v => v.ClientIP);

            //获取图片的Y轴,单位为票数
            List<VoteChartY> chartSeries = new List<VoteChartY>();


            foreach (var ipG in ipGroup)
            {
                VoteChartY ipYAxisValues = new VoteChartY();
                ipYAxisValues.name = ipG.First().ClientIP;
                ipYAxisValues.data = new int[chartXAxis.Count()];
                for (int j = 0; j < chartXAxis.Count(); j++)
                {
                    //获取当前IP，在该时间段的票数
                    var ipVotes = votes.Where(v => v.ClientIP == ipG.First().ClientIP
                        && v.VoteTime.ToString("yyyy-MM-dd") == chartXAxis[j]).ToList();
                    ipYAxisValues.data[j] = ipVotes.Count();
                }
                chartSeries.Add(ipYAxisValues);
            }

            Chart chart = new Chart();
            chart.XAxis = chartXAxis;
            chart.YSeries = chartSeries.ToArray();
            #endregion 

            return Json(chart);
        }

        /// <summary>
        /// 生成GUID
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GenerateGUID()
        {
            Message message = new Message();
            message.Success = true;

            try
            {
                message.ReturnStrId = Guid.NewGuid().ToString();


            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "GUID生成失败：" + e.Message;
            }

            return Json(message);
        }

        /// <summary>
        /// 投票参数设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveVoteConfig(M_VoteConfig config)
        {
            Message message = new Message();

            #region 校验
            if (string.IsNullOrEmpty(config.VoteInterval))
            {
                message.Success = false;
                message.Content = "请设置投票时间间隔格";
                return Json(message);
            }
            var rightFormat = Regex.IsMatch(config.VoteInterval, @"^[1-9]\d*|0$");
            if (!rightFormat)
            {
                message.Success = false;
                message.Content = "投票时间间隔格式不正确，只能是0或者正整数";
                return Json(message);
            }

            if (string.IsNullOrEmpty(config.EachDayVoteTimes))
            {
                message.Success = false;
                message.Content = "请设置投票时间间隔格内的投票次数";
                return Json(message);
            }

            rightFormat = Regex.IsMatch(config.EachDayVoteTimes, @"^[1-9]\d*|0$");
            if (!rightFormat)
            {
                message.Success = false;
                message.Content = "投票次数格式不正确，只能是0或者正整数";
                return Json(message);
            }

            if (string.IsNullOrEmpty(config.VoteEndTime))
            {
                message.Success = false;
                message.Content = "请设置投票截止时间";
                return Json(message);
            }
            #endregion

            try
            {
                B_IntelInnovationProjectApply bApply = new B_IntelInnovationProjectApply();

                bApply.SaveVoteConfiguration(config);
                message.Content = "参数设置成功";


            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "参数设置失败：" + e.Message;

            }
            return Json(message);
        }

        
    }
}