using System;
using System.Collections.Generic;
using System.Text;

namespace WikiSlownikScraper2
{
    class Translation
    {
        public string BaseLanguage { get; set; }
        public string NewLanguage { get; set; }
        public string Photo { get; set; }

        public Translation(string BaseLanguage1, string NewLanguage1, string Photo1)
        {
            BaseLanguage = BaseLanguage1;   // polish
            NewLanguage = NewLanguage1;     // english
            Photo = Photo1;                 // photo link (flag)
        }
    }
}
