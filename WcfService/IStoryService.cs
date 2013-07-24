using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStoryService" in both code and config file together.
    [ServiceContract]
    public interface IStoryService
    {
        [OperationContract]
        ICollection<Story> getStories();

        [OperationContract]
        ICollection<Story> getStory(int id);

        [OperationContract]
        void createStory(string title, string description, int points, int sprintID);

        [OperationContract]
        void deleteStory(int id);

        [OperationContract]
        ICollection<Story> updateStory(int id, string title, string description, int points, string status, int sprintID);
    }
}
