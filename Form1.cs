using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Memory;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace FS4_Flight_Tracker
{
    public partial class Form1 : Form
    {

        // กำหนดชื่อให้ตรงกับ Shared Memory ของ Bridge DLL ที่คุณใช้
        private const string MAP_NAME = "AeroflyBridgeData";

        private MemoryMappedFile mmf;
        private MemoryMappedViewAccessor accessor;
        private Timer timer;

        public Form1()
        {
            InitializeComponent();
            InitializeSharedMemory();
            InitializeTelemetryTimer();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct AeroflyBridgeData
        {
            public double Longitude;          // Offset 0
            public double Latitude;           // Offset 8
            public double Altitude;           // Offset 16 (Meters)
            public double Pitch;              // Offset 24 (Radians)
            public double Roll;               // Offset 32 (Radians)
            public double Heading;            // Offset 40 (Radians)
            public double IndicatedAirspeed;  // Offset 48 (m/s) **ตำแหน่งความเร็ว**
            public double GearPosition;       // Offset 56
            public double FlapPosition;       // Offset 64
            public double ThrottlePosition;   // Offset 72
            public double Airspeed;           // Offset 80
        }

        private void timerstatusaeroflyfs4_Tick(object sender, EventArgs e)
        {
            /*
            Process[] p = Process.GetProcessesByName("aerofly_fs_4");

            if (p.Length > 0)
            {
                statusaeroflyfs4.Text = "✔️ Aerofly FS 4 Connected";
                statusaeroflyfs4.ForeColor = Color.LimeGreen;
            }
            else
            {
                statusaeroflyfs4.Text = "❌ Aerofly FS 4 Disconnected";
                statusaeroflyfs4.ForeColor = Color.Red;
            }
            */
        }
            
        private void InitializeSharedMemory()
        {
            try
            {
                // เปิดการเชื่อมต่อ Shared Memory
                mmf = MemoryMappedFile.OpenExisting(MAP_NAME);
                accessor = mmf.CreateViewAccessor();
                statusaeroflyfs4.Text = "Status: Connected to Aerofly FS4";
                statusaeroflyfs4.ForeColor = Color.Lime;
            }
            catch (FileNotFoundException)
            {
                statusaeroflyfs4.Text = "Status: Shared Memory not found (Open Aerofly first)";
                statusaeroflyfs4.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                statusaeroflyfs4.Text = $"Status: Error ({ex.Message})";
                statusaeroflyfs4.ForeColor = Color.Red;
            }
        }

        private void InitializeTelemetryTimer()
        {
            timer = new Timer();
            timer.Interval = 33; // อัปเดตประมาณ 30 FPS (33ms)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (accessor == null) return;

            try
            {
                // อ่าน Struct ทั้งหมดออกมารวดเดียว
                /*
                 
                0 = Altitude ความสูง ใช้ Roll
                 
                8 = หมุนซ้ายขวา

                16 = ความเร็ว Speed Knots ใช้ IndicatedAirspeed

                 */

                accessor.Read<AeroflyBridgeData>(0, out AeroflyBridgeData data); 
                accessor.Read<AeroflyBridgeData>(16, out AeroflyBridgeData data2);


                double altitudestatus = data.Roll * (10.31493 / Math.PI); // 10.31493

                double speedstatus = data2.IndicatedAirspeed * (6.1075 / Math.PI); // 6.1075

                // แสดงผล
                AltitideStatus.Text = "Altitude\n" + $"{altitudestatus:F0}";

                SpeedStatus.Text = "Speed\n" + $"{speedstatus:F0}";
            }
            catch (Exception ex)
            {
                statusaeroflyfs4.Text = "Read Error: " + ex.Message;
            }
        }

        // คืนทรัพยากรเมื่อปิดหน้าต่างโปรแกรม
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timer?.Stop();
            accessor?.Dispose();
            mmf?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
