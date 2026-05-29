// Interfaces/IRepository.cs
using System.Collections.Generic;

namespace SAMSv1.Interface
{
    /// <summary>
    /// OOP: Abstraction — defines the contract every CRUD repository must follow.
    /// </summary>
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}