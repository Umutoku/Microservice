using FreeEducation.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeEducation.Shared.ControllerBases
{
    public class CustomControllerBase :ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}
