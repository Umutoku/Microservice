using System;
using System.Collections.Generic;
using System.Text;

namespace FreeEducation.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
