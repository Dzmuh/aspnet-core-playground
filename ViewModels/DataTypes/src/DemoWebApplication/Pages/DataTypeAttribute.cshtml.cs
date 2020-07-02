using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DemoWebApplication.Pages
{
    public class DataTypeAttributePageViewModel : PageModel
    {
        private readonly ILogger<DataTypeAttributePageViewModel> _logger;

        public DataTypeAttributePageViewModel(ILogger<DataTypeAttributePageViewModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            /// <summary>
            /// Демонстрация типа данных <see cref="System.ComponentModel.DataAnnotations.DataType.DateTime"/>,
            /// который представляет момент времени в виде даты и время суток.
            /// </summary>
            [Display(Name = "DataType.DateTime")]
            [DataType(DataType.DateTime)]
            public DateTime DateTime { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Демонстрация типа данных <see cref="System.ComponentModel.DataAnnotations.DataType.Date"/>,
            /// который представляет значение даты.
            /// </summary>
            [Display(Name = "DataType.Date")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
            public DateTime Date { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Демонстрация типа данных <see cref="System.ComponentModel.DataAnnotations.DataType.Time"/>,
            /// который представляет значение времени.
            /// </summary>
            [Display(Name = "DataType.Time")]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
            public DateTime Time { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Демонстрация типа данных <see cref="System.ComponentModel.DataAnnotations.DataType.Date"/>,
            /// который представляет непрерывный промежуток времени, на котором существует объект.
            /// </summary>
            [Display(Name = "DataType.Duration")]
            [DataType(DataType.Duration)]
            public Decimal Duration { get; set; }
        }

        public void OnGet()
        {
            this.Input = new InputModel();
        }
    }
}