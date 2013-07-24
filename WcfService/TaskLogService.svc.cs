using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService {

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TaskLogService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TaskLogService.svc or TaskLogService.svc.cs at the Solution Explorer and start debugging.

    public class TaskLogService : ITaskLogService {

        TaskyDBServiceDataContext db = new TaskyDBServiceDataContext();

        public ICollection<TaskLog> getTaskLogs() {

            var result = (from taskLog in db.TaskLogs select taskLog);
            return result.ToList();
        }

        public ICollection<TaskLog> getTaskLog(int id) {

            var result = (from taskLog in db.TaskLogs where taskLog.id == id select taskLog);
            return result.ToList();
        }

        public void createTaskLog(int loggedHours, DateTime loggedDate, int taskID) {

            Task aTask = (from task in db.Tasks where task.id == taskID select task).ToList().First();

            TaskLog newLog = new TaskLog { 

                loggedHours = loggedHours,
                logDate = loggedDate,
                taskId = aTask.id
            };

            db.TaskLogs.InsertOnSubmit(newLog);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public void deleteTaskLog(int id) {

            TaskLog logToDelete = (from taskLog in db.TaskLogs where taskLog.id == id select taskLog).ToList().First();

            db.TaskLogs.DeleteOnSubmit(logToDelete);

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }
        }

        public ICollection<TaskLog> updateTaskLog(int id, int loggedHours, DateTime loggedDate, int taskID) {

            int getTaskID = 0;
            var query = (from taskLog in db.TaskLogs where taskLog.id == id select taskLog);
            try {

               getTaskID = (from task in db.Tasks where task.id == taskID select task).ToList().First().id;
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            foreach (TaskLog log in query) {

                log.loggedHours = loggedHours;
                log.logDate = loggedDate;
                log.taskId = getTaskID;
            }

            try {

                db.SubmitChanges();
            }
            catch (Exception e) {

                Console.WriteLine(e);
            }

            var result = (from aLog in db.TaskLogs where aLog.id == id select aLog);
            return result.ToList();
        }
    }
}