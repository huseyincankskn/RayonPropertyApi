using System;

namespace Entities.VMs
{
    public class CreatePasswordVm
    {
        public Guid PsrGuid { get; set; }
        public string Password { get; set; }
    }
}