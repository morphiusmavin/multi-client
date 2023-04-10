using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpServerEngineSampleClient
{
	class ClientsAvail
	{
		public int lbindex { get; set; }
		public int index { get; set; }
		public string ip_addr { get; set; }
		public string label { get; set; }
		public int socket { get; set; }
		public int type { get; set; }
		public string time_string { get; set; }
		//public string prev_time_string { get; set; }
		public int flag;
	}
}
