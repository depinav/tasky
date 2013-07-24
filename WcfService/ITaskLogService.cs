using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITaskLogService" in both code and config file together.
    [ServiceContract]
    public interface ITaskLogService
    {
        [OperationContract]
        ICollection<TaskLog> getTaskLogs();

        [OperationContract]
        ICollection<TaskLog> getTaskLog(int id);

        [OperationContract]
        void createTaskLog(int loggedHours, DateTime loggedDate, int taskID);

        [OperationContract]
        void deleteTaskLog(int id);

        [OperationContract]
        ICollection<TaskLog> updateTaskLog(int id, int loggedHours, DateTime loggedDate, int taskID);
    }
}
