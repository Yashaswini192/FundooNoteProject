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

                var user = fundooContext.users.FirstOrDefault(y => y.UserId == UserId);
                if ((user != null && NoteId != null) || (NoteId == null))
                {
                    label.LabelName = labelmodel.LabelName;
                    label.UserId = UserId;
                    label.NoteId = NoteId;

                    fundooContext.LabelTable.Add(label);
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
    }
}
