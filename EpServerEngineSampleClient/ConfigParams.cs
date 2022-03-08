using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpServerEngineSampleClient
{
    public class ConfigParams
    {
        public int fan_on { get; set; }
        public int fan_off { get; set; }
        public int rpm_mph_update_rate { get; set; }
        public int high_rev_limit { get; set; }
        public int low_rev_limit { get; set; }
        public int FPGAXmitRate { get; set; }
        public int lights_on_value { get; set; }
        public int lights_off_value { get; set; }
        public int adc_rate { get; set; }
        public int rt_value_select { get; set; }
        public int lights_on_delay { get; set; }
        public int engine_temp_limit { get; set; }
        public int battery_box_temp { get; set; }
        public int test_bank { get; set; }
		public int password_timeout { get; set; }
		public int password_retries { get; set; }
		public int baudrate3 { get; set; }
		public string password { get; set; }

		public int si_fan_on { get; set; }
        public int si_fan_off { get; set; }
        public int si_rpm_mph_update_rate { get; set; }
        public int si_high_rev_limit { get; set; }
        public int si_low_rev_limit { get; set; }
        public int si_FPGAXmitRate { get; set; }
        public int si_lights_on_value { get; set; }
        public int si_lights_off_value { get; set; }
		public int si_adc_rate { get; set; }
		public int si_rt_value_select { get; set; }
        public int si_lights_on_delay { get; set; }
        public int si_engine_temp_limit { get; set; }
        public int si_test_bank { get; set; }
        public int si_battery_box_temp { get; set; }
		public int si_password_timeout { get; set; }
		public int si_password_retries { get; set; }
		public int si_baudrate3 { get; set; }
		public bool set { get; set; }
	}
}
