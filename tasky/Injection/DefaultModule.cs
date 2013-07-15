using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tasky.Repository;

namespace tasky.Injection
{
    public class DefaultModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISprintRepository>().To<SprintRepository>().InTransientScope();
            Bind<IStoryRepository>().To<StoryRepository>().InTransientScope();
            Bind<ITeamMemberRepository>().To<TeamMemberRepository>().InTransientScope();
            Bind<ITaskRepository>().To<TaskRepository>().InTransientScope();
        }
    }

}