using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Statistic
    {
        public string PCName { get; set; }
        public DateTime UpTime { get; set; }
        public float CpuUsage { get; set; }
        public float UsedMemory { get; set; }
        public float TotalMemory { get; set; }
        public float AvaibleMemory { get; set; }
        public DateTime SystemTime { get; set; }
        //public DriveInfo[] Drives { get; set; }
    }
}
