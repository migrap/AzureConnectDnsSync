using FluentScheduler;
using FluentScheduler.Model;
using System;

namespace AzureConnectDnsSync {
    internal class TaskService : ITaskService {
        private Registry _registry = new Registry();

        public void Initialize() {
            TaskManager.Initialize(_registry);
        }

        public void Start() {
            this.Log().Info(() => string.Format("Starting {0}", typeof(TaskManager).Name));
            TaskManager.Start();
        }

        public void Stop() {
            this.Log().Info(() => string.Format("Stopping {0}", typeof(TaskManager).Name));
            TaskManager.Stop();
        }

        public void Add(Action action, Action<Schedule> schedule) {
            TaskManager.AddTask(action, schedule);
        }        
    }
}