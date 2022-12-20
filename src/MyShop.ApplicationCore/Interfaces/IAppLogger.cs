using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] arg);
        void LogWarning(string message, params object[] arg);
        void LogError(Exception exception, string? message, params object[] arg);
    }
}
