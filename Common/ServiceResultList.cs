using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Common
{
    public class ServiceResultList<T>:ServiceResult
    {
        public T data { get; set; }
    }
}
