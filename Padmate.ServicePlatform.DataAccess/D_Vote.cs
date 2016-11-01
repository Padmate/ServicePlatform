using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_Vote
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

       

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vote GetVoteById(int id)
        {
            var vote = _dbContext.Votes.FirstOrDefault(a => a.Id == id);
            return vote;
        }

        /// <summary>
        /// 根据BrowserId或者FingerPrint，获取某个时间间隔的投票次数
        /// </summary>
        /// <param name="browserId"></param>
        /// <param name="fingerprint"></param>
        /// <param name="clientip"></param>
        /// <param name="voteinterval">投票时间间隔,单位为/分钟</param>
        /// <returns></returns>
        public int GetIntervalVotes(string browserId, string fingerprint, string clientip, int voteinterval)
        {
            var sql = @"select * from 
                        (
	                        select 
	                        datediff(minute,a.VoteTime,getDate()) diffminute , --投票日期与当前时间相差多少分钟
	                        a.* 
	                        from Votes a
                        ) temp where diffminute <{0} ";

            
            //（1天=1440 = 24*60分钟）
            sql = string.Format(sql,voteinterval);
            var querySql = _dbContext.Database.SqlQuery<Vote>(sql);

            var query = querySql.Where(a => 1 == 1);

            if(!string.IsNullOrEmpty(browserId))
            {
                query = query.Where(v=>v.BrowserId == browserId);
            }
            if(!string.IsNullOrEmpty(fingerprint))
            {
                query = query.Where(v => v.BrowserFingerPrint == fingerprint);

            }
            if (!string.IsNullOrEmpty(clientip))
            {
                query = query.Where(v => v.ClientIP == clientip);

            }
            var todayVotes = query.ToList().Count(); ;

            return todayVotes;
        }

        /// <summary>
        /// 根据设备ID，获取投票信息
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public List<Vote> GetVoteByBrowserId(string browserId)
        {
            var votes = _dbContext.Votes.Where(v => v.BrowserId == browserId).ToList() ;
            return votes;
            
        }

        public List<Vote> GetAll()
        {
            var votes = _dbContext.Votes
                .ToList();

            return votes;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        public List<Vote> GetByMulitCond(Vote vote)
        {
            var query = _dbContext.Votes.Where(a => 1 == 1);

            if(!string.IsNullOrEmpty(vote.VoteNo))
            {
                query = query.Where(v=>v.VoteNo == vote.VoteNo);
            }
            if (!string.IsNullOrEmpty(vote.ClientIP))
            {
                query = query.Where(v => v.ClientIP == vote.ClientIP);
            }
            if (!string.IsNullOrEmpty(vote.BrowserId))
            {
                query = query.Where(v => v.BrowserId == vote.BrowserId);
            }
            if (!string.IsNullOrEmpty(vote.BrowserFingerPrint))
            {
                query = query.Where(v => v.BrowserFingerPrint == vote.BrowserFingerPrint);
            }

            var votes = query.ToList();

            return votes;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        public int AddVote(Vote vote)
        {
            _dbContext.Votes.Add(vote);
            _dbContext.SaveChanges();
            return vote.Id;
        }


        public void DeleteVote(int id)
        {
            var vote = _dbContext.Votes.Where(i => i.Id == id).FirstOrDefault();
            if (vote != null)
            {
                _dbContext.Votes.Remove(vote);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteVote(List<int> ids)
        {
            var votes = _dbContext.Votes.Where(i => ids.Contains(i.Id)).ToList();
            if (votes.Count > 0)
            {
                _dbContext.Votes.RemoveRange(votes);
                _dbContext.SaveChanges();
            }
        }
    }
}
