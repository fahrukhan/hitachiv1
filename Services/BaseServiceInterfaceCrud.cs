using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Services
{
    public interface BaseServiceInterfaceCrud
    {
        Task<Response<List<T>>> All<T>();
        Task<Response<T>> GetById<T>(int id);
        Task<Response<T>> Add<T>(Object newObject);
        Task<Response<T>> Update<T>(Object updateDepartment);
        Task<Response<String>> Delete(int id);
    }
}