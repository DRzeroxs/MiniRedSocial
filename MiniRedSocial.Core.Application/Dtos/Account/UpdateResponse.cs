﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRedSocial.Core.Application.Dtos.Account
{
    public class UpdateResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
