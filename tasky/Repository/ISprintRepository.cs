﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tasky.Models;

namespace tasky.Repository
{
    public interface ISprintRepository
    {
        ICollection<Sprint> FindAll();
        ICollection<Story> FindStoriesForSprint(int id);
        ICollection<TaskLog> FindTaskLogsForSprint(int id);
        int Save(Sprint s);
        Sprint FindById(int id);
        void Delete(int id);

        int SumTaskEstimatesForSprint(int id);
        void SaveStories(int id, List<Story> stories);
    }
}
