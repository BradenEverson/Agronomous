﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agronomous
{
    public class gardenModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            return Redirect("/createGarden");
        }
    }
}