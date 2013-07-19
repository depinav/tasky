using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using tasky.DAL;
using tasky.Models;
using tasky.Repository;

namespace WcfService
{
    public class TaskService : ITaskService {

        private ITaskRepository taskRepo;

        TaskService() { }

        TaskService(ITaskRepository iTask) {
            taskRepo = iTask;
        }

        public ICollection<Task> getTasks() {

            return taskRepo.FindAll();
        }
    }
}
