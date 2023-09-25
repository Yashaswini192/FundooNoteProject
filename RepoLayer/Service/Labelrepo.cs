using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Service
{
    public class Labelrepo : ILabelRepo
    {
        private readonly FundooContext fundooContext;

        public Labelrepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity CreateLabel(LabelModel labelmodel,int UserId,int NoteId)
        {
            try
            {
                LabelEntity label = new LabelEntity(); 

                var user = fundooContext.UserTable.FirstOrDefault(y => y.UserId == UserId);
                if ((user != null && NoteId != null) || (NoteId == null))
                {
                    label.LabelName = labelmodel.LabelName;
                    label.UserId = UserId;
                    label.NoteId = NoteId;

                    fundooContext.Label.Add(label);
                    fundooContext.SaveChanges();

                    if (label != null)
                    {
                        return label;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<LabelEntity> RetrieveLabel(int userId)
        {
            try
            {
                var user = fundooContext.Label.Where(x => x.UserId == userId).ToList();
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LabelEntity> UpdateLabel(string newLabelName, long UserId, string labelName)
        {
            try
            {
                var user = fundooContext.Label.Where(x => x.UserId == UserId && x.LabelName == labelName).ToList();
                if (user != null)
                {
                    foreach (var item in user)
                    {
                        item.LabelName = newLabelName;
                    }
                    fundooContext.SaveChanges();
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LabelEntity DeleteLabel(string labelName, int userId)
        {
            try
            {
                var deleteLabel = fundooContext.Label.Where(x => x.UserId == userId && x.LabelName == labelName).FirstOrDefault();
                if (deleteLabel != null)
                {
                    fundooContext.Label.Remove(deleteLabel);
                    fundooContext.SaveChanges();
                    return deleteLabel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
