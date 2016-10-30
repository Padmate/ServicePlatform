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
        /// 根据BrowserId或者FingerPrint，获取今天的投票次数
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        public int GetTodayVotes(string browserId,string fingerprint)
        {
            var sql = @"select * from 
                        (
	                        select 
	                        datediff(minute,a.VoteTime,getDate()) diffminute , --投票日期与当前世界相差多少分钟（1天=1440 = 24*60分钟）
	                        a.* 
	                        from Votes a
                        ) temp where diffminute <{0} ";

            
            sql = string.Format(sql,1*24*60);
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
