﻿using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface ICollabRepo
    {
        public CollabEntity CreateCollab(string Email, int UserId, int NoteId);
    }
}
