using SignalRAPI.Data.Repositories.Interfaces;
using SignalRAPI.Models;
using System;
using System.Threading.Tasks;

namespace SignalRAPI.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<RequestForm> RequestForms { get; }
        IGenericRepository<FormStatus> FormStatuses { get; }
        Task Save();
    }
}
