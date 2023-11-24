﻿using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBusiness
    {
        public LabelEntity CreateLabel(LabelModel labelmodel, int UserId, int NoteId);

        public List<LabelEntity> RetrieveLabel( int userId,int NoteId);

        public List<LabelEntity> UpdateLabel(int labelId, long UserId, string labelName);

        public LabelEntity DeleteLabel(int NoteId, int userId);

    }
}
