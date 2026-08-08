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
using System.Runtime.InteropServices;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text.RegularExpressions;
using Memory;

namespace FS4_Flight_Tracker
{
    public partial class Form1 : Form
    {

        // กำหนดชื่อให้ตรงกับ Shared Memory ของ Bridge DLL ที่คุณใช้
        private const string MAP_NAME = "AeroflyBridgeData";

        private MemoryMappedFile mmf;
        private MemoryMappedViewAccessor accessor;
        private Timer timer;



        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        const int PROCESS_VM_READ = 0x0010;




        public Form1()
        {
            InitializeComponent();
            InitializeSharedMemory();
            InitializeTelemetryTimer();
        }

        public static string GetCurrentAircraftRaw(string mcfFilePath)
        {
            if (!File.Exists(mcfFilePath)) return "Unknown";

            string content = File.ReadAllText(mcfFilePath);

            // หาบล็อก tmsettings_aircraft และอ่านค่าใน tag name
            string pattern = @"<\[tmsettings_aircraft\]\[aircraft\]\[\]\s*<\[string8u\]\[name\]\[([^\]]+)\]>";
            Match match = Regex.Match(content, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value; // ได้ค่า "b787_9"
            }

            return "Not Found";
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
            StatusUpdateDepatureText();
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

            //       LoadAircraftName();

            if (accessor == null) return;

            try
            {
                // อ่าน Struct ทั้งหมดออกมารวดเดียว
                /*
                 
                0 = Altitude ความสูง ใช้ Roll
                 
                8 / 24 = หมุนซ้ายขวา แกน X

                16 = ความเร็ว Speed Knots ใช้ IndicatedAirspeed

                 */

                accessor.Read<AeroflyBridgeData>(0, out AeroflyBridgeData data);
                accessor.Read<AeroflyBridgeData>(16, out AeroflyBridgeData data2);
                accessor.Read<AeroflyBridgeData>(24, out AeroflyBridgeData data3);
                accessor.Read<AeroflyBridgeData>(10, out AeroflyBridgeData data4);

                double altitudestatus = data.Roll * (10.31493 / Math.PI); // 10.31493

                double speedstatus = data2.IndicatedAirspeed * (6.1075 / Math.PI); // 6.1075

                double rollstatus = data3.Roll * (191 / Math.PI); // 6.1075

                double pitchstatus = data4.Airspeed * (500 / Math.PI); // 6.1075

                // แสดงผล
                AltitideStatus.Text = "Altitude\n" + $"{altitudestatus:F0}";

                SpeedStatus.Text = "Speed\n" + $"{speedstatus:F0}";

                RollStatus.Text = "Roll\n" + $"{rollstatus:F0}";

                PitchStatus.Text = "Pitch\n" + $"{pitchstatus:F0}";
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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }



        private void timerreadingmemory_Tick(object sender, EventArgs e)
        {
            
        }

        private void LoadNameAircraft_Click(object sender, EventArgs e)
        {

        }

        private void StatusUpdateDepatureText()
        {
            string processName = "aerofly_fs_4";

            // ตั้งค่าตาม Cheat Engine
            int baseOffset = 0x0173BB88;
            int[] offsets = new int[] { 0x8, 0x358, 0xC8, 0x10 }; // เรียง Offsets ตามที่โชว์ใน CE

            // ดึงค่า Value ข้อความ
            string ceValue = Form1.GetCEStringValue(processName, baseOffset, offsets , 4);

            // แสดงผลบนหน้าจอ Form
            DepartureText.Text = ceValue; // จะขึ้นข้อความเช่น "VTBD / DMK" เหมือนในตาราง CE เป๊ะๆ
        }

        // ฟังก์ชันอ่านข้อความ String จาก Pointer Path (ถอดแบบการทำงานของ Cheat Engine)
        public static string GetCEStringValue(string processName, int baseOffset, int[] offsets, int stringLength = 32)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return "Process Not Found";

            Process game = processes[0];
            IntPtr hProcess = OpenProcess(PROCESS_VM_READ, false, game.Id);
            if (hProcess == IntPtr.Zero) return "Access Denied (Run as Admin)";

            // 1. เริ่มจาก Base Address + Base Offset
            IntPtr currentAddress = IntPtr.Add(game.MainModule.BaseAddress, baseOffset);
            byte[] pointerBuffer = new byte[8]; // 64-bit Pointer ใช้ 8 Bytes
            int bytesRead;

            // 2. วนลูปอ่าน Pointer ทีละ Layer
            for (int i = 0; i < offsets.Length; i++)
            {
                if (!ReadProcessMemory(hProcess, currentAddress, pointerBuffer, pointerBuffer.Length, out bytesRead))
                    return "??"; // อ่านไม่ได้เหมือน CE แสดง ??

                long nextAddress = BitConverter.ToInt64(pointerBuffer, 0);
                if (nextAddress == 0) return "Null"; // Pointer หลุด

                currentAddress = (IntPtr)(nextAddress + offsets[i]);
            }

            // 3. อ่าน Bytes ข้อความปลายทาง
            byte[] stringBuffer = new byte[stringLength];
            if (ReadProcessMemory(hProcess, currentAddress, stringBuffer, stringBuffer.Length, out bytesRead))
            {
                // แปลง Bytes เป็น UTF-8 String
                string text = Encoding.UTF8.GetString(stringBuffer);

                // *** หัวใจสำคัญ: Cheat Engine จะตัดข้อความตรง Null Byte (\0) ตัวแรกทันที ***
                int nullIndex = text.IndexOf('\0');
                if (nullIndex >= 0)
                {
                    text = text.Substring(0, nullIndex);
                }

                return text.Trim(); // คืนค่าข้อความเหมือนช่อง Value ของ CE
            }

            return "??";
        }
    }
}