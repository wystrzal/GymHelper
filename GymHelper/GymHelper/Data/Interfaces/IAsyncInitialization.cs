using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymHelper.Data.Interfaces
{
    public interface IAsyncInitialization
    {
        Task Initialization { get; }
    }
}
