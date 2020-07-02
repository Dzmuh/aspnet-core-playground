using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DemoWebApplication.Pages
{
    public class IndexPageViewModel : PageModel
    {
        private readonly ILogger<IndexPageViewModel> _logger;

        public IndexPageViewModel(ILogger<IndexPageViewModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
