using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Service
{
    public class B_Vote
    {
        D_Vote _dVote = new D_Vote();

        public M_Vote GetById(int id)
        {
            var vote = _dVote.GetVoteById(id);
            var result = ConverEntityToModel(vote);
            return result;
        }

        /// <summary>
        /// 根据BrowserId，获取某个时间间隔的投票次数
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public int GetIntervalVotesByBrowserId(string browserId,int interval)
        {
            var todayVotes = _dVote.GetIntervalVotes(browserId, null, null, interval);
            return todayVotes;
        }

        /// <summary>
        /// 根据FingerPrint获取某个时间间隔的投票次数
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public int GetIntervalVotesByFingerPrint(string fingerprint, int interval)
        {
            var todayVotes = _dVote.GetIntervalVotes(null, fingerprint, null, interval);
            return todayVotes;
        }

        /// <summary>
        /// 根据IP和FingerPrint获取某个时间间隔的投票次数
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public int GetIntervalVotesByClientIPAndFingerPrint(string fingerprint, string clientip, int interval)
        {
            var todayVotes = _dVote.GetIntervalVotes(null, fingerprint, clientip, interval);
            return todayVotes;
        }

        public List<M_Vote> GetAllData()
        {
            var votes = _dVote.GetAll();
            var result = votes.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<M_Vote> GetByMulitCond(M_Vote model)
        {
            var search = new Vote()
            {
                VoteNo = model.VoteNo,
                ClientIP = model.ClientIP,
                BrowserId = model.BrowserId,
                BrowserFingerPrint = model.BrowserFingerPrint
            };

            var votes = _dVote.GetByMulitCond(search);
            var result = votes.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 根据BrowserId获取投票数据
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public List<M_Vote> GetByBrowserId(string browserId)
        {
            var votes = _dVote.GetVoteByBrowserId(browserId);
            var result = votes.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddVote(M_Vote model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "投票新增成功";

            try
            {
                //新增投票
                var vote = new Vote()
                {
                    VoteNo = model.VoteNo,
                    VoteTime = model.VoteTime,
                    BrowserId = model.BrowserId,
                    BrowserFingerPrint = model.BrowserFingerPrint,
                    ClientIP = model.ClientIP
                };

                message.ReturnId = _dVote.AddVote(vote);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "投票新增失败，异常：" + e.Message;
            }
            return message;
        }

        

        public Message BatchDeleteByIds(List<int> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "投票删除成功";

            try
            {
                _dVote.BatchDeleteVote(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "投票删除失败：" + e.Message;
            }

            return message;
        }

        private M_Vote ConverEntityToModel(Vote vote)
        {
            if (vote == null) return null;

            var model = new M_Vote()
            {
                Id = vote.Id,
                VoteNo = vote.VoteNo,
                VoteTime = vote.VoteTime,
                BrowserId = vote.BrowserId,
                BrowserFingerPrint = vote.BrowserFingerPrint,
                ClientIP = vote.ClientIP

            };
            return model;
        }

    }
}
