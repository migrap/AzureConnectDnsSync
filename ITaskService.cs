using FluentScheduler.Model;
using Ninject;
using System;

namespace AzureConnectDnsSync {
    internal interface ITaskService : IStartable, IInitializable {
        void Add(Action action, Action<Schedule> schedule);
    }
}