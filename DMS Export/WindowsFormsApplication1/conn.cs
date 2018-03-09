using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class conn
    {
        public string ConnString()
        {
            string[] lines = System.IO.File.ReadAllLines("c:\settings.config");
            return lines[0].Trim();              
        }

    }
}
