﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.DataType.Interfaces
{
    public interface IUniqueObject<Tkey>
    {
        Tkey GetKey();
    }
}
