using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Interfaces
{
    public interface IRestClient
    {
        Task<ApiResult<TData>> ExecuteGetAsync<TData>(string requestUrl) where TData : class;
    }
}
