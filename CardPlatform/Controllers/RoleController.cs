using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZtApplication.RoleApp;

namespace CardPlatform.Controllers
{
    public class RoleController : ControllerBase
    {

        private readonly IRoleAppService _service;
        public RoleController(IRoleAppService service)
        {
            _service = service;
        }
    }
}