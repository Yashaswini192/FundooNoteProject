﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo labelRepo;

        public LabelBusiness(ILabelRepo labelRepo)
        {
             this.labelRepo = labelRepo;
        }
        public LabelEntity CreateLabel(LabelModel labelmodel, int UserId, int NoteId)
        {
            try
            {
                return labelRepo.CreateLabel(labelmodel, UserId, NoteId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<LabelEntity> RetrieveLabel(int userId)
        {
            try
            {
                return labelRepo.RetrieveLabel(userId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<LabelEntity> UpdateLabel(string newLabelName, long UserId, string labelName)
        {
            try
            {
                return labelRepo.UpdateLabel(newLabelName, UserId, labelName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public LabelEntity DeleteLabel(string labelName, int userId)
        {
            try
            {
                return labelRepo.DeleteLabel(labelName, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
