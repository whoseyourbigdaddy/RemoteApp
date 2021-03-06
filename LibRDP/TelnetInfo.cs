﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace LibRDP
{
    public class TelnetHost : RDPHost
    {
        public override void GenerateHost(object obj)
        {
            this.RZIndex = System.Threading.Interlocked.Decrement(ref _Index);

            var client = new LibRDP.TelnetControl(this.RInfo);
            client.OnDisconnectedEvent += ClientHost_OnDisconnected;
            var wf = new WindowsFormsHost()
            {
                Background = Brushes.Black,
                Child = client
            };
            this.RObject = client;
            this.Host = wf;
            this.Host.IsEnabled = false;
        }

        public TelnetHost(LibRDP.RemoteInfo rinfo) : base(rinfo) { }
    }

    public class TelnetInfo : RemoteInfo
    {
        public override bool CanFullScreen => false;

        public TelnetInfo() : base(Protocol.TELNET)
        {
            this.Port = 23;

            this.EHost = new TelnetHost(this);
        }
    }
}
