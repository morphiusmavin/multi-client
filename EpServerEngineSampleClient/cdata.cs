using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpServerEngineSampleClient
{
	public class Cdata
	{
		public string label { get; set; }
		public int index { get; set; }
		public int port { get; set; }
		public int state { get; set; }
		public int on_hour { get; set; }
		public int on_minute { get; set; }
		public int on_second { get; set; }
		public int off_hour { get; set; }
		public int off_minute { get; set; }
		public int off_second { get; set; }
	}
}
