using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultProcessor.ViewModels
{
    public class ManageUserViewModel
    {
        public IdentityUser[] Administrators { get; set; }
        public IdentityUser[] Everyone { get; set; }
        public IdentityRole[] Roles { get; set; }
    }
}
