using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Interfaces
{
    public interface ICompany
    {
        int Id { get; set; }

        string Name { get; set; }

        Classification Classification { get; set; }
    }
}
