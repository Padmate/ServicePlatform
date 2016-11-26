using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_VoteBlackList
    {
        ServiceDbContext _dbContext = new ServiceDbContext();



        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoteBlackList GetVoteBlackListById(int id)
        {
            var vote = _dbContext.VoteBlackLists.FirstOrDefault(a => a.Id == id);
            return vote;
        }

        /// <summary>
        /// 根据Voteno查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoteBlackList GetVoteBlackListByVoteNo(string voteno)
        {
            var vote = _dbContext.VoteBlackLists.FirstOrDefault(a => a.VoteNo == voteno);
            return vote;
        }

        /// <summary>
        /// 根据ClientIP查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VoteBlackList GetVoteBlackListByClientIP(string clientip)
        {
            var vote = _dbContext.VoteBlackLists.FirstOrDefault(a => a.ClientIP == clientip);
            return vote;
        }


        public List<VoteBlackList> GetAll()
        {
            var votes = _dbContext.VoteBlackLists
                .ToList();

            return votes;
        }

        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        public List<VoteBlackList> GetByMulitCond(VoteBlackList vote)
        {
            var query = _dbContext.VoteBlackLists.Where(a => 1 == 1);

            if (!string.IsNullOrEmpty(vote.VoteNo))
            {
                query = query.Where(v => v.VoteNo == vote.VoteNo);
            }
            if (!string.IsNullOrEmpty(vote.ClientIP))
            {
                query = query.Where(v => v.ClientIP == vote.ClientIP);
            }

            var votes = query.ToList();

            return votes;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="vote"></param>
        /// <returns></returns>
        public int AddVoteBlackList(VoteBlackList vote)
        {
            _dbContext.VoteBlackLists.Add(vote);
            _dbContext.SaveChanges();
            return vote.Id;
        }


        public void DeleteVoteBlackList(int id)
        {
            var vote = _dbContext.VoteBlackLists.Where(i => i.Id == id).FirstOrDefault();
            if (vote != null)
            {
                _dbContext.VoteBlackLists.Remove(vote);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteVoteBlackList(List<int> ids)
        {
            var votes = _dbContext.VoteBlackLists.Where(i => ids.Contains(i.Id)).ToList();
            if (votes.Count > 0)
            {
                _dbContext.VoteBlackLists.RemoveRange(votes);
                _dbContext.SaveChanges();
            }
        }
    }
}
