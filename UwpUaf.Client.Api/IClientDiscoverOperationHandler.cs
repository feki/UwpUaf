﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UwpUaf.Client.Api
{
    public interface IClientDiscoverOperationHandler
    {
        Task<DiscoveryData> ProcessDiscoverOperationAsync();
    }
}