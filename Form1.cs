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



        // Imports Windows APIs สำหรับอ่าน Process Memory
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        private const int PROCESS_WM_READ = 0x0010;




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
            StatusUpdateArrivalText();
            StatusUpdateAircraftNameText();
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

                // Volanta Style
                VLTA_SPD.Text = "SPD: " + $"{speedstatus:F0}" +"kts";
                VLTA_ALT.Text = "ALT: " + $"{altitudestatus:F0}" + "ft";

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
            panel1.Visible = false;
            panelVolantaStyle.Visible = true;
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
            DepartureText.Text = ceValue;
            VLTA_DEP_Status.Text = ceValue;
        }
        private void StatusUpdateArrivalText()
        {
            string processName = "aerofly_fs_4";

            // ตั้งค่าตาม Cheat Engine
            int baseOffset = 0x016D8A88;
            int[] offsets = new int[] { 0xD0 , 0x358 , 0xB0 , 0x10 , 0x100 , 0x0 }; // เรียง Offsets ตามที่โชว์ใน CE

            // ดึงค่า Value ข้อความ
            string ceValue = Form1.GetCEStringValue(processName, baseOffset, offsets, 4);

            // แสดงผลบนหน้าจอ Form
            VLTA_ARR_Status.Text = ceValue;
        }

        private void StatusUpdateAircraftNameText()
        {
            string processName = "aerofly_fs_4";

            // ตั้งค่าตาม Cheat Engine 
            int baseOffset = 0x016D8A88;

            // สำหรับชื่อเครื่องบิน
            int[] offsets = new int[] { 0x98, 0x88, 0x1F0, 0x40, 0x10, 0x40 , 0x0 , 0xB8 , 0x388}; // เรียง Offsets ตามที่โชว์ใน CE
            // สำหรับชื่อลายสติณกเกอร์เครื่องบิน
            int[] offsetslivery = new int[] { 0x98, 0x38, 0x20, 0xB8, 0x208, 0x28, 0x48, 0xD0, 0x38 };


            // ดึงค่า Value ข้อความ
            int ceValue = Form1.GetCEIntValue(processName, baseOffset, offsets);

            int ceValueLivery = Form1.GetCEIntValue(processName, baseOffset, offsetslivery);

            // แสดงผลบนหน้าจอ Form
            //      VLTA_Name_Aircraft.Text = ceValue.ToString()+"\n" + ceValueLivery.ToString();

            switch (ceValue)
            {
                case 959525729:
                    VLTA_Name_Aircraft.Text = "Airbus A319";
                    break;

                case 808596321:
                    VLTA_Name_Aircraft.Text = "Airbus A320";
                    break;

                case 825373537:
                    VLTA_Name_Aircraft.Text = "Airbus A321";
                    break;

                case 808792929:
                    VLTA_Name_Aircraft.Text = "Airbus A350-1000";
                    break;

                case 808989537:
                    VLTA_Name_Aircraft.Text = "Airbus A380";
                    break;

                default:
                    VLTA_Name_Aircraft.Text = ceValue.ToString();
                    break;
            }
        }


        #region Helper Function: เดิน Pointer Chain
        /// <summary>
        /// คำนวณหา Memory Address สุดท้ายจาก Pointer Chain ตาม Cheat Engine
        /// </summary>
        private static IntPtr GetFinalAddress(IntPtr processHandle, IntPtr baseAddress, int baseOffset, int[] offsets)
        {
            IntPtr currentAddress = IntPtr.Add(baseAddress, baseOffset);
            byte[] buffer = new byte[8]; // รองรับทั้ง 32-bit และ 64-bit Pointer
            IntPtr bytesRead;

            if (offsets == null || offsets.Length == 0)
                return currentAddress;

            // เดินตาม Offset ตัวที่ 0 ถึง N-2
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                if (!ReadProcessMemory(processHandle, currentAddress, buffer, IntPtr.Size, out bytesRead))
                    return IntPtr.Zero;

                long nextAddress = (IntPtr.Size == 8)
                    ? BitConverter.ToInt64(buffer, 0)
                    : BitConverter.ToInt32(buffer, 0);

                currentAddress = new IntPtr(nextAddress + offsets[i]);
            }

            // อ่าน Pointer ตัวสุดท้าย แล้วบวกด้วย Offset ตัวสุดท้าย
            if (!ReadProcessMemory(processHandle, currentAddress, buffer, IntPtr.Size, out bytesRead))
                return IntPtr.Zero;

            long finalPointer = (IntPtr.Size == 8)
                ? BitConverter.ToInt64(buffer, 0)
                : BitConverter.ToInt32(buffer, 0);

            return new IntPtr(finalPointer + offsets[offsets.Length - 1]);
        }
        #endregion

        #region 1. อ่านค่าเป็น 4 Bytes (Int32)
        public static int GetCEIntValue(string processName, int baseOffset, int[] offsets)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return 0;

            Process process = processes[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            if (processHandle == IntPtr.Zero) return 0;

            try
            {
                IntPtr finalAddress = GetFinalAddress(processHandle, process.MainModule.BaseAddress, baseOffset, offsets);
                if (finalAddress == IntPtr.Zero) return 0;

                byte[] valueBuffer = new byte[4]; // 4 Bytesสำหรับ Int32
                IntPtr bytesRead;
                if (ReadProcessMemory(processHandle, finalAddress, valueBuffer, 4, out bytesRead))
                {
                    return BitConverter.ToInt32(valueBuffer, 0);
                }

                return 0;
            }
            finally
            {
                CloseHandle(processHandle);
            }
        }
        #endregion

        #region 2. อ่านค่าเป็น String (ข้อความ)
        public static string GetCEStringValue(string processName, int baseOffset, int[] offsets, int stringLength = 32, Encoding encoding = null)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return string.Empty;

            Process process = processes[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            if (processHandle == IntPtr.Zero) return string.Empty;

            try
            {
                IntPtr finalAddress = GetFinalAddress(processHandle, process.MainModule.BaseAddress, baseOffset, offsets);
                if (finalAddress == IntPtr.Zero) return string.Empty;

                byte[] stringBuffer = new byte[stringLength];
                IntPtr bytesRead;

                if (ReadProcessMemory(processHandle, finalAddress, stringBuffer, stringLength, out bytesRead))
                {
                    if (encoding == null) encoding = Encoding.UTF8;

                    // แปลง Byte Array เป็น String และตัด Null Terminator ('\0') ออก
                    string result = encoding.GetString(stringBuffer);
                    int nullIndex = result.IndexOf('\0');
                    return nullIndex >= 0 ? result.Substring(0, nullIndex) : result;
                }

                return string.Empty;
            }
            finally
            {
                CloseHandle(processHandle);
            }
        }
        #endregion

        #region 3. อ่านค่าเป็น Float (ทศนิยม 4 Bytes) - แถมเผื่อไว้
        public static float GetCEFloatValue(string processName, int baseOffset, int[] offsets)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return 0f;

            Process process = processes[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            if (processHandle == IntPtr.Zero) return 0f;

            try
            {
                IntPtr finalAddress = GetFinalAddress(processHandle, process.MainModule.BaseAddress, baseOffset, offsets);
                if (finalAddress == IntPtr.Zero) return 0f;

                byte[] valueBuffer = new byte[4];
                IntPtr bytesRead;
                if (ReadProcessMemory(processHandle, finalAddress, valueBuffer, 4, out bytesRead))
                {
                    return BitConverter.ToSingle(valueBuffer, 0);
                }

                return 0f;
            }
            finally
            {
                CloseHandle(processHandle);
            }
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void Quit_Volanta_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panelVolantaStyle.Visible = false;
        }
    }
}