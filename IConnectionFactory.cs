using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantoss.Data.Framework
{
    /// <summary>
    /// Connection factory
    /// </summary>
    public interface IConnectionFactory
    {
        Task<T?> CreateConnection<T>(ConnectionType connectionType) where T : class;
    }
}
