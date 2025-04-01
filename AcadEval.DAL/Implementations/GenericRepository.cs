using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadEval.DAL.Interfaces;

namespace AcadEval.DAL.Implementations
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
    }
}
