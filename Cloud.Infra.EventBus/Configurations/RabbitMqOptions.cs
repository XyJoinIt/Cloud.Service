﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.EventBus.Configurations;

public class RabbitMqOptions
{
    public string HostName { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }
}
