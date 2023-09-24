using CommonLayer.Model;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ILabelRepo
    {
        public LabelEntity CreateLabel(LabelModel labelmodel, int UserId, int NoteId);
    }
}
