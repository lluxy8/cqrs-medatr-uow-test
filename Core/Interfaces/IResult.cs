using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; }
    }

}
