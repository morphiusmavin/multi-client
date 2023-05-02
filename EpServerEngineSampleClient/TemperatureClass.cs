using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpServerEngineSampleClient
{
	public class TemperatureClass
	{
		public int client_id { get; set; }
		public int sensor_no { get; set; }
		public string time { get; set; }
		public int temp { get; set; }
	}
}
