using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Core.Models;

namespace Felix.Core.Interfaces
{
    public interface IBot
    {
        Task<string> GetResponse(Message message);
    }
}
