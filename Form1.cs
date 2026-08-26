using MetroFramework.Drawing.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/*
 * 
Document Outline
--------------------------
Form1

1.PanelTaskbar
2.statusaeroflyfs4
3.notificationopengame
4.panelVolantaStyle
5.panel1
6.panelVolantaEnabled
--------------------------

 */
namespace FS4_Flight_Tracker
{
    public partial class Form1 : Form
    {
        private string fs4flighttrackversion = "0.80";
        private string fs4maingameversion = "4.8.4.1";
        private bool toggleViewChangelog = false;

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

        private double depXlocked;
        private double depYlocked;
        private double depZlocked;

        private double arrXlocked;
        private double arrYlocked;
        private double arrZlocked;

        private double playerX;
        private double playerY;
        private double playerZ;

        private int switchColourStep = 0;
        private int switchHUDStep = 0;

        private string liveryname = "";
        private string aircraftname = "";

        // 1. นำเข้า API ของ Windows เพื่อควบคุมการลากหน้าต่าง
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;


        private String aircraftNameCustomHUD, aircraftLiveryCustomHUD, departureNameCustomHUD, arrivalNameCustomHUD;
        private double progressCustomHUD;

        private double vspeedstatus;
        public Form1()
        {
            InitializeComponent();
            InitializeSharedMemory();
            InitializeTelemetryTimer();
            this.FormBorderStyle = FormBorderStyle.None; // ซ่อนขอบฟอร์มเดิม  
        }



        private void MovementTimer_Tick(object sender, EventArgs e)
        {

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
            public double AIRCRAFT_VERTICAL_SPEED;
        }

        private void timerstatusaeroflyfs4_Tick(object sender, EventArgs e)
        {
            VersionText.Text = "Version : " + fs4flighttrackversion + "\r\n" + "Main Game : " + fs4maingameversion;
            VersionText2.Text = "Version : " + fs4flighttrackversion + "\r\n" + "Main Game : " + fs4maingameversion;
            StatusUpdateDepatureText();
            StatusUpdateArrivalText();
            StatusUpdateTimerClockText();
            StatusUpdatePlayerPositionText();
            StatusUpdateDepatureAndArrivalText();
            Custom_VLTA6_Debug_Status.Text = "PROGRESS : " + $"{progressCustomHUD:F2} %" + "\n" + "AIRCRAFT : " + aircraftname + "\n" + "LIVERY : " + liveryname + "\n" + "DEP : " + departureNameCustomHUD + "\n" + "ARR : " + arrivalNameCustomHUD;
            rungame();
            StatusUpdateThrottleText();
            VLTA_Name_Aircraft.Text = aircraftname + "\n" + liveryname;

            liveryname = textbox_liveryname.Text;
            aircraftname = comboBox_selectaircraft.Text;
            AircraftandLivery.Text = aircraftname + " | " + liveryname;
            //           StatusUpdateEnginePowerText();
        }

        private void rungame()
        {
            string targetAppName = "aerofly_fs_4";

            if (IsAppRunning(targetAppName))
            {
                // เปิดเกมแล้ว
                statusaeroflyfs4.Text = "✔️ Aerofly FS 4 Connected";
                statusaeroflyfs4.ForeColor = Color.Lime;
                Restart.Visible = true;
                pleaserestart.Visible = true;
            }
            else
            {
                // ปิดเกมแล้ว
                statusaeroflyfs4.Text = "❌ Aerofly FS 4 Disconnected";
                statusaeroflyfs4.ForeColor = Color.Red;
                notificationopengame.Visible = true;
                PanelTaskbar.Visible = true;
                Restart.Visible = false;
                pleaserestart.Visible = false;
                panel1.Visible = false;
                statusaeroflyfs4.Visible = true;
            }
        }

        // ฟังก์ชันสำหรับเช็กสถานะ
        private bool IsAppRunning(string processName)
        {
            // ค้นหา Process ตามชื่อ (ไม่ต้องใส่ .exe)
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }


        private void InitializeSharedMemory()
        {
            try
            {
                // เปิดการเชื่อมต่อ Shared Memory
                mmf = MemoryMappedFile.OpenExisting(MAP_NAME);
                accessor = mmf.CreateViewAccessor();
                statusaeroflyfs4.ForeColor = Color.Lime;
                notificationopengame.Visible = false;
                panel1.Visible = true;
            }
            catch (FileNotFoundException)
            {
                statusaeroflyfs4.ForeColor = Color.Red;
                notificationopengame.Visible = true;
            }
            catch (Exception ex)
            {
                statusaeroflyfs4.Text = $"Error ({ex.Message})";
                statusaeroflyfs4.ForeColor = Color.Red;
            }
        }

        private void InitializeTelemetryTimer()
        {
            timer = new Timer();
            timer.Interval = 16; // อัปเดตประมาณ 30 FPS (33ms)
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

                accessor.Read<AeroflyBridgeData>(2, out AeroflyBridgeData data5);

                double altitudestatus = data.Roll * (10.31493 / Math.PI); // 10.31493

                double speedstatus = data2.IndicatedAirspeed * (6.1075 / Math.PI); //* (6.1075 / Math.PI); // 6.1075

                double rollstatus = data3.Roll * (191 / Math.PI); // 6.1075

                double pitchstatus = data4.Airspeed * (500 / Math.PI); // 6.1075

                double vs = data5.AIRCRAFT_VERTICAL_SPEED * (1000 / Math.PI); ;


                if (speedstatus > 30)
                {
                    VLTA_SPD_HideNumber.Visible = false;
                    VLTA_SPD.Visible = true;

                    Custom_VLTA2_Speed_Status.Visible = true;
                    Custom_VLTA2_Speed_HideNumber_Status.Visible = false;
                }
                else if (speedstatus < 30)
                {
                    VLTA_SPD_HideNumber.Visible = true;
                    VLTA_SPD.Visible = false;

                    Custom_VLTA2_Speed_Status.Visible = false;
                    Custom_VLTA2_Speed_HideNumber_Status.Visible = true;
                }

                // แสดงผล
                AltitideStatus.Text = "Altitude : " + $"{altitudestatus:F0}";

                SpeedStatus.Text = "Speed : " + $"{speedstatus:F0}";

                // Volanta Style
                VLTA_SPD.Text = "SPD: " + $"{speedstatus:F0}" + "kts";
                VLTA_ALT.Text = "ALT: " + $"{altitudestatus:F0}" + "ft";

                Custom_VLTA2_Speed_Status.Text = "SPEED : " + $"{speedstatus:F0}" + " KNOTS";
                Custom_VLTA3_Altitude_Status.Text = "ALTITUDE : " + $"{altitudestatus:F0}" + " FT";

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

        private void timerreadingmemory_Tick(object sender, EventArgs e)
        {

        }

        private void StatusUpdateDepatureText()
        {
            string processName = "aerofly_fs_4";

            // ตั้งค่าตาม Cheat Engine
            int baseOffset = 0x0173BB88;
            int[] offsets = new int[] { 0x8, 0x358, 0xC8, 0x10 }; // เรียง Offsets ตามที่โชว์ใน CE

            // ดึงค่า Value ข้อความ
            string ceValue = Form1.GetCEStringValue(processName, baseOffset, offsets, 4);

            // แสดงผลบนหน้าจอ Form
            DepartureText.Text = ceValue;
            VLTA_DEP_Status.Text = ceValue;

            departureNameCustomHUD = ceValue;
        }
        private void StatusUpdateArrivalText()
        {
            string processName = "aerofly_fs_4";

            // ตั้งค่าตาม Cheat Engine
            int baseOffset = 0x0173BB60;
            int[] offsets = new int[] { 0x358, 0x98, 0x10, 0x110 }; // เรียง Offsets ตามที่โชว์ใน CE

            // ดึงค่า Value ข้อความ
            string ceValue = Form1.GetCEStringValue(processName, baseOffset, offsets, 4);

            // แสดงผลบนหน้าจอ Form
            VLTA_ARR_Status.Text = ceValue;

            arrivalNameCustomHUD = ceValue;
            ArrivalText.Text = ceValue;
        }
        private void StatusUpdateDepatureAndArrivalText()
        {
            string processName = "aerofly_fs_4";

            // Departure

            int baseOffsetDep = 0x0173BB88;
            int[] offsetsDep = new int[] { 0x8, 0x358, 0xC8, 0x10 };

            // Arrival
            int baseOffsetArr = 0x0173BB60;
            int[] offsetsArr = new int[] { 0x358, 0x98, 0x10, 0x110 };

            string ceValueDep = Form1.GetCEStringValue(processName, baseOffsetDep, offsetsDep, 4);
            string ceValueArr = Form1.GetCEStringValue(processName, baseOffsetArr, offsetsArr, 4);

            Custom_VLTA1_DEP_and_ARR_Status.Text = ceValueDep + " - " + ceValueArr;
        }

        private void StatusUpdateTimerClockText()
        {
            string processName = "aerofly_fs_4";

            // 0. กำหนด Pointer สำหรับ Double สั้น (เช่น 23.999999999)
            int baseOffset = 0x0182F768;
            int[] offsets = new int[] { 0x5E0, 0x20, 0x20, 0x0, 0xF0, 0x70 };

            // 1. อ่านค่า Double จาก Memory
            double rawTime = Form1.GetCEDoubleValue(processName, baseOffset, offsets);

            // 2. คุมให้อยู่ในช่วง 0.0 ถึง 23.9999
            double minTime = 0.0;
            double maxTime = 23.9999;
            double timeValue = rawTime;

            if (timeValue < minTime)
            {
                timeValue = minTime;
            }
            else if (timeValue > maxTime)
            {
                timeValue = maxTime;
            }

            // 3. ดึงชั่วโมงและนาทีจาก TimeSpan
            TimeSpan timeSpan = TimeSpan.FromHours(timeValue);
            int hours24 = timeSpan.Hours;
            int minutes = timeSpan.Minutes;

            // เช็คเศษทศนิยม ถ้าเข้าใกล้ .9999 ให้ปรับเป็น 59 นาที
            double fraction = timeValue - Math.Truncate(timeValue);
            if (fraction >= 0.9999)
            {
                minutes = 59;
            }

            // 4. คำนวณหา AM / PM และแปลงชั่วโมงเป็นระบบ 12 ชั่วโมง
            string designator = "";
            int hours12 = hours24;

            if (hours24 >= 12)
            {
                designator = "PM";
                if (hours24 > 12)
                {
                    hours12 = hours24 - 12; // เช่น 13:00 -> 1:00 PM
                }
            }
            else
            {
                designator = "AM";
                if (hours24 == 0)
                {
                    hours12 = 12; // 00:00 -> 12:00 AM
                }
            }

            // 5. จัดข้อความแสดงผล (เช่น "11:59 PM" หรือ "12:00 AM")
            string ceValue = $"{hours12:D2}:{minutes:D2} {designator}";

            // นำไปใช้งานกับ Text
            VLTA_TIME.Text = "UTC: " + ceValue;

            Custom_VLTA5_Clock_Status.Text = ceValue;

        }

        private void StatusUpdatePlayerPositionText()
        {
            string processName = "aerofly_fs_4";

            // Player Position
            int baseOffsetPosX = 0x0173BB68;
            int[] offsetsPosX = new int[] { 0x20, 0xF0, 0x8, 0x90, 0x0, 0x0, 0x2A8 };

            int baseOffsetPosY = 0x016D8A88;
            int[] offsetsPosY = new int[] { 0x10, 0x40, 0x0, 0x28, 0x8, 0x0, 0x2B0 };

            int baseOffsetPosZ = 0x0181B898;
            int[] offsetsPosZ = new int[] { 0x5C8, 0xD0, 0x308, 0x150, 0x658, 0x0, 0x2B8 };

            double playerPosX = Form1.GetCEDoubleValue(processName, baseOffsetPosX, offsetsPosX);
            double playerPosY = Form1.GetCEDoubleValue(processName, baseOffsetPosY, offsetsPosY);
            double playerPosZ = Form1.GetCEDoubleValue(processName, baseOffsetPosZ, offsetsPosZ);

            playerX = playerPosX;
            playerY = playerPosY;
            playerZ = playerPosZ;

            // Departure Position
            int baseOffsetDepPosX = 0x0173BB68;
            int[] offsetsDepPosX = new int[] { 0x20, 0xD0, 0x358, 0x28, 0x90, 0x0, 0x30 };
            int baseOffsetDepPosY = 0x016D8A88;
            int[] offsetsDepPosY = new int[] { 0xD0, 0x358, 0x28, 0x60, 0x0, 0x8, 0x30 };
            int baseOffsetDepPosZ = 0x016D8A88;
            int[] offsetsDepPosZ = new int[] { 0xD0, 0x0, 0x348, 0x98, 0x0, 0x50 };

            double playerDepPosX = Form1.GetCEDoubleValue(processName, baseOffsetDepPosX, offsetsDepPosX);
            double playerDepPosY = Form1.GetCEDoubleValue(processName, baseOffsetDepPosY, offsetsDepPosY);
            double playerDepPosZ = Form1.GetCEDoubleValue(processName, baseOffsetDepPosZ, offsetsDepPosZ);

            depXlocked = playerDepPosX;
            depYlocked = playerDepPosY;
            depZlocked = playerDepPosZ;

            // Arrival Position
            int baseOffsetArrPosX = 0x0182F768;
            int[] offsetsArrPosX = new int[] { 0x60, 0x20, 0x20, 0x0, 0x348, 0xE0, 0x468 };
            int baseOffsetArrPosY = 0x016D8A88;
            int[] offsetsArrPosY = new int[] { 0xD0, 0x358, 0x28, 0x60, 0x0, 0x8, 0x30 };
            int baseOffsetArrPosZ = 0x0182F768;
            int[] offsetsArrPosZ = new int[] { 0x8B0, 0x20, 0x20, 0x358, 0xF8, 0x4B0, 0x18 };

            double playerArrPosX = Form1.GetCEDoubleValue(processName, baseOffsetArrPosX, offsetsArrPosX);
            double playerArrPosY = Form1.GetCEDoubleValue(processName, baseOffsetArrPosY, offsetsArrPosY);
            double playerArrPosZ = Form1.GetCEDoubleValue(processName, baseOffsetArrPosZ, offsetsArrPosZ);

            arrXlocked = playerArrPosX;
            arrYlocked = playerArrPosY;
            arrZlocked = playerArrPosZ;


            // ==========================================================
            // ส่วนที่เพิ่ม: คำนวณระยะทางและเปอร์เซ็นต์ (Progress 0 - 100%)
            // ==========================================================

            // 1. คำนวณระยะทางรวมทั้งหมด (Departure -> Arrival) และ ระยะทางที่บินมาแล้ว (Departure -> Player)
            double dxTotal = arrXlocked - depXlocked;
            double dyTotal = arrYlocked - depYlocked;
            double dzTotal = arrZlocked - depZlocked;
            double totalDistanceMeters = Math.Sqrt(dxTotal * dxTotal + dyTotal * dyTotal + dzTotal * dzTotal);

            double dxCurrent = playerX - depXlocked;
            double dyCurrent = playerY - depYlocked;
            double dzCurrent = playerZ - depZlocked;
            double currentDistanceMeters = Math.Sqrt(dxCurrent * dxCurrent + dyCurrent * dyCurrent + dzCurrent * dzCurrent);

            // 2. คำนวณเปอร์เซ็นต์ Progress (0 ถึง 100%)
            double progressPercent = 0.0;
            if (totalDistanceMeters > 0)
            {
                progressPercent = (currentDistanceMeters / totalDistanceMeters) * 100.0;
            }

            // ล็อกขอบเขตไม่ให้ต่ำกว่า 0% หรือเกิน 100%
            if (progressPercent < 0.0) progressPercent = 0.0;
            if (progressPercent > 100.0) progressPercent = 100.0;

            // 3. คำนวณระยะทางที่เหลือ (แปลงเป็น กิโลเมตร)
            double dxRemaining = arrXlocked - playerX;
            double dyRemaining = arrYlocked - playerY;
            double dzRemaining = arrZlocked - playerZ;
            double remainingMeters = Math.Sqrt(dxRemaining * dxRemaining + dyRemaining * dyRemaining + dzRemaining * dzRemaining);
            double remainingKm = remainingMeters / 1000.0; // แปลงเมตรเป็น กม.



            int mincurrentsizepanelVolantastyle = 0;
            int maxcurrentsizepanelVolantastyle = 860;

            int mincurrentsizepanelVolantaCustomStyle = 0;
            int maxcurrentsizepanelVolantaCustomStyle = 1000;


            // คำนวณหาค่า current จาก progressPercent (0.0 ถึง 100.0)
            int currentsizepanelVolantastyle = (int)Math.Round((progressPercent / 100.0) * maxcurrentsizepanelVolantastyle);

            int currentsizepanelVolantaCustomstyle = (int)Math.Round((progressPercent / 100.0) * maxcurrentsizepanelVolantaCustomStyle);


            // กำหนดขอบเขตความปลอดภัย (Clamp)
            if (currentsizepanelVolantastyle < mincurrentsizepanelVolantastyle) currentsizepanelVolantastyle = mincurrentsizepanelVolantastyle;
            if (currentsizepanelVolantastyle > maxcurrentsizepanelVolantastyle) currentsizepanelVolantastyle = maxcurrentsizepanelVolantastyle;

            if (currentsizepanelVolantaCustomstyle < mincurrentsizepanelVolantaCustomStyle) currentsizepanelVolantaCustomstyle = mincurrentsizepanelVolantaCustomStyle;
            if (currentsizepanelVolantaCustomstyle > maxcurrentsizepanelVolantaCustomStyle) currentsizepanelVolantaCustomstyle = maxcurrentsizepanelVolantaCustomStyle;


            PlayerPosition.Text = "Player Position:" + "\n" + "X = " + playerPosX + "\n" + "Y = " + playerPosY + "\n" + "Z = " + playerPosZ;
            DeparturePosition.Text = "Departure Position:" + "\n" + "X = " + playerDepPosX + "\n" + "Y = " + playerDepPosY + "\n" + "Z = " + playerDepPosZ;
            ArrivalPosition.Text = "Arrival Position:" + "\n" + "X = " + playerArrPosX + "\n" + "Y = " + playerArrPosY + "\n" + "Z = " + playerArrPosZ;

            // Label แสดง Progress และระยะทางที่เหลือ (สร้าง Label ใหม่เพิ่มใน Form เช่น lblProgress และ lblRemainingKm)
            lblProgress.Text = $"Flight Progress: {progressPercent:F2} %";
            lblCurrent.Text = "Remaining Distance: " + $"{remainingKm:F2}" + " NM";

            ProgressBarStatus.Size = new Size(currentsizepanelVolantastyle, 7);
            ProgressSlider.Value = currentsizepanelVolantaCustomstyle;

            Custom_VLTA4_Progress_Status.Text = $"PROGRESS: {progressPercent:F2} %";

            progressCustomHUD = progressPercent;
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



        #region 1.2. อ่านค่าเป็น 8 Bytes (Int32)
        public static long GetCEIntValue8Byte(string processName, int baseOffset, int[] offsets)
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

                byte[] valueBuffer = new byte[8]; // 1. เปลี่ยนขนาด Buffer เป็น 8 Bytes สำหรับ Int64
                IntPtr bytesRead;
                if (ReadProcessMemory(processHandle, finalAddress, valueBuffer, 8, out bytesRead)) // 2. อ่านข้อมูลขนาด 8 Bytes
                {
                    return BitConverter.ToInt64(valueBuffer, 0); // 3. แปลงเป็น Int64 (long)
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

        #region 4. อ่านค่าเป็น Double - แถมเผื่อไว้
        public static double GetCEDoubleValue(string processName, int baseOffset, int[] offsets)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0) return 0.0;

            Process process = processes[0];
            IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            if (processHandle == IntPtr.Zero) return 0.0;

            try
            {
                IntPtr finalAddress = GetFinalAddress(processHandle, process.MainModule.BaseAddress, baseOffset, offsets);
                if (finalAddress == IntPtr.Zero) return 0.0;

                byte[] valueBuffer = new byte[8]; // อ่านข้อมูลขนาด 8 Bytes สำหรับ Double
                IntPtr bytesRead;
                if (ReadProcessMemory(processHandle, finalAddress, valueBuffer, 8, out bytesRead))
                {
                    return BitConverter.ToDouble(valueBuffer, 0); // แปลง Byte เป็น Double
                }

                return 0.0;
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
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            PanelTaskbar.Visible = false;
            panelVolantaStyle.Visible = true;
            statusaeroflyfs4.Visible = false;
        }

        private void Quit_Volanta_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            PanelTaskbar.Visible = true;
            statusaeroflyfs4.Visible = true;
            panelVolantaStyle.Visible = false;
        }

        private void Switch_Color_Background_Click(object sender, EventArgs e)
        {
            switch (switchColourStep)
            {
                case 0:
                    panelVolantaStyle.BackColor = Color.Black;
                    Switch_Color_Background.ForeColor = Color.Black;
                    Quit_Volanta.ForeColor = Color.Black;
                    Next_HUD.ForeColor = Color.Black;
                    break;
                case 1:
                    panelVolantaStyle.BackColor = Color.Blue;
                    Switch_Color_Background.ForeColor = Color.Blue;
                    Quit_Volanta.ForeColor = Color.Blue;
                    Next_HUD.ForeColor = Color.Blue;
                    break;
                case 2:
                    panelVolantaStyle.BackColor = Color.Transparent;
                    Switch_Color_Background.ForeColor = Color.Transparent; ;
                    Quit_Volanta.ForeColor = Color.Transparent;
                    Next_HUD.ForeColor = Color.Transparent;
                    break;
                default:
                    panelVolantaStyle.BackColor = Color.Lime;
                    Switch_Color_Background.ForeColor = Color.Lime;
                    Quit_Volanta.ForeColor = Color.Lime;
                    Next_HUD.ForeColor = Color.Lime;
                    switchColourStep = -1; // Resets cycle
                    break;
            }
            switchColourStep++;
        }

        private void Next_HUD_Click(object sender, EventArgs e)
        {
            switch (switchHUDStep)
            {
                case 0:
                    ImageCustomHUD1.Visible = true;
                    ImageHUDVolantaStyle.Visible = false;
                    break;
                default:
                    ImageCustomHUD1.Visible = false;
                    ImageHUDVolantaStyle.Visible = true;
                    switchHUDStep = -1;
                    break;
            }
            switchHUDStep++;
        }

        private void PanelTaskbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ButtonChangelog_Click(object sender, EventArgs e)
        {
            // Reverse the boolean state
            toggleViewChangelog = !toggleViewChangelog;

            if (toggleViewChangelog)
            {
                ViewChangelogList.Visible = true;
                ButtonChangelog.Text = "Close\nChangelog";
            }
            else
            {
                ViewChangelogList.Visible = false;
                ButtonChangelog.Text = "View\nChangelog";
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void ButtonExitProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            // Shuts down the application and immediately starts a new instance
            Application.Restart();

            // Prevents further code execution and background threads from hanging
            Environment.Exit(0);
        }

        private void textbox_liveryname_TextChanged(object sender, EventArgs e)
        {
            // 1. กำหนดรายการคำห้ามใช้ที่ต้องการตรวจจับ
            string[] badWords = { "fuck", "f***", "fu**", "fuc*", "fucky", "fuckyou", "fuckyous", "gay", "nigga", "nigger", "n1664", "ni664", "nig64", "nigg4", "n1gga", "n16ga", "n166a", "n1gga", "shit", "5h17", "sh1t", "sh*t", "bitch", "b1tch", "b17ch", "ass", "asshole", "assholes", "pussy", "pu55y", "nigg3r", "n1663r", "dick", "d1ck" };

            string input = textbox_liveryname.Text;

            // 2. ตรวจหาคำห้ามใช้แบบไม่สนตัวพิมพ์เล็ก-ใหญ่ (Case-Insensitive)
            string foundWord = badWords.FirstOrDefault(word =>
                input.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);

            // ตัวอย่าง: ตรวจจับว่ามีคำว่า "badword" พิมพ์เข้ามาหรือไม่
            if (foundWord != null)
            {
                label_blocklanguage.Text = "You specified something that was blocked\r\nby the language filter. Please try again.\r\n";
                label_blocklanguage.Visible = true;
                StartVolantaStyle.Visible = false;
            }
            else
            {
                StartVolantaStyle.Visible = true;
                label_blocklanguage.Visible = false;
            }
        }

        // ฟังก์ชันช่วยหา Base Address ของ DLL
        public static IntPtr GetModuleBaseAddress(Process process, string moduleName)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    return module.BaseAddress;
                }
            }
            return IntPtr.Zero;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // Code to run when toggled ON
                this.TransparencyKey = Color.Lime;
                Switch_Color_Background.FlatStyle = FlatStyle.Standard;
                Custom_VLTA6_Debug_Status.BackColor = Color.Transparent;
                Custom_VLTA7_Throttle_and_Engine_Text.BackColor = Color.Transparent;
                Custom_VLTA7_1_Throttle_Status.BackColor = Color.Transparent;
            }
            else
            {
                // Code to run when toggled OFF
                this.TransparencyKey = Color.Empty;
                Switch_Color_Background.FlatStyle = FlatStyle.Flat;
                //190,0,0,0
                Custom_VLTA6_Debug_Status.BackColor = Color.FromArgb(190, 0, 0, 0);
                Custom_VLTA7_Throttle_and_Engine_Text.BackColor = Color.FromArgb(190, 0, 0, 0);
                Custom_VLTA7_1_Throttle_Status.BackColor = Color.FromArgb(190, 0, 0, 0);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                // Code to run when toggled ON
                this.TopMost = true;
            }
            else
            {
                // Code to run when toggled OFF
                this.TopMost = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                // Code to run when toggled ON
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                // Code to run when toggled OFF
                this.FormBorderStyle = FormBorderStyle.None;
            }

        }

        private IntPtr ResolveCheatEnginePointer(IntPtr hProc, Process proc, string modName, int baseOff, int[] chainOffsets, bool is64Bit)
        {
            // หา Module Base Address
            IntPtr moduleBase = GetModuleBaseAddress(proc, modName);
            if (moduleBase == IntPtr.Zero) return IntPtr.Zero;

            // จุดเริ่มต้น: "Module.dll" + Offset Base
            IntPtr currentAddress = IntPtr.Add(moduleBase, baseOff);

            int pointerSize = is64Bit ? 8 : 4;
            byte[] buffer = new byte[pointerSize];

            // วนลูปอ่าน Pointer ตาม Offsets
            for (int i = 0; i < chainOffsets.Length; i++)
            {
                // อ่านค่า Address จาก Pointer ปัจจุบัน
                if (!ReadProcessMemory(hProc, currentAddress, buffer, buffer.Length, out _))
                {
                    return IntPtr.Zero; // Pointer หลุด/Address เสีย
                }

                // ดึงค่า Pointer Address
                long dereferencedAddress = is64Bit ? BitConverter.ToInt64(buffer, 0) : BitConverter.ToInt32(buffer, 0);

                // ถ้า Pointer ชี้ไปที่ 0x0 (Null Pointer) แสดงว่ายังไม่พร้อมใช้งาน
                if (dereferencedAddress == 0) return IntPtr.Zero;

                // บวก Offset ของชั้นนั้นเข้าไปเพื่อรออ่านในรอบถัดไป
                currentAddress = new IntPtr(dereferencedAddress + chainOffsets[i]);
            }

            return currentAddress; // ได้ Address ปลายทางจริงๆ
        }

        private void StatusUpdateThrottleText()
        {
            string processName = "aerofly_fs_4";
            string moduleName = "AeroflyBridge.dll";
            int baseOffset = 0x000D9510;
            int[] offsets = new int[] { 0x80, 0x1D0, 0x0, 0x50, 0x68, 0x8, 0x240 };

            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                Custom_VLTA4_Throttle_Status.Text = "Process Not Found";
                return;
            }

            IntPtr hProcess = OpenProcess(0x0010, false, processes[0].Id);
            IntPtr targetAddress = ResolveCheatEnginePointer(hProcess, processes[0], moduleName, baseOffset, offsets, true);

            if (targetAddress != IntPtr.Zero)
            {
                byte[] buffer = new byte[8];
                if (ReadProcessMemory(hProcess, targetAddress, buffer, buffer.Length, out _))
                {
                    // 1. อ่านค่า Current ดิบจาก RAM (0.00 ถึง 1.00)
                    double current = BitConverter.ToDouble(buffer, 0);

                    // 2. กำหนดค่า Min และ Max เป็น double
                    double min = 0.0;
                    double max = 100.0;

                    int minthrottle = 0;
                    int maxthrottle = 152;

                    // 3. คำนวณแปลงค่า Current ดิบให้อยู่ในช่วง Min ถึง Max
                    double clampedCurrent = Math.Max(0.0, Math.Min(current, 1.0));
                    double calculatedCurrent = min + (clampedCurrent * (max - min));


                    double calculatedCurrentThrottle = maxthrottle + (clampedCurrent * (minthrottle - maxthrottle));
                    int currentthrottle = (int)Math.Round((calculatedCurrentThrottle / maxthrottle) * maxthrottle);

                    // 4. นำไปแสดงผลบน Label.Text
                    Custom_VLTA4_Throttle_Status.Text = $"THROTTLE: {calculatedCurrent:F2}%"; // เช่น Current: 75.00%
                    ThrottleTest.Text = $"Throttle : {calculatedCurrent:F2}%";
                    Custom_VLTA7_1_Throttle_Status.Text = $"{calculatedCurrent:F2}%";
                    ProgressBarThrottleWhite.Size = new Size(currentthrottle, 10);
                }
                else
                {
                    Custom_VLTA4_Throttle_Status.Text = "Read Error";
                    ThrottleTest.Text = "Read Error";
                }
            }
            else
            {
                Custom_VLTA4_Throttle_Status.Text = "Bad Pointer";
                ThrottleTest.Text = "Bad Pointer";
            }
            CloseHandle(hProcess);

            string supportAircraftTxt = comboBox_selectaircraft.Text.Trim(); // ดึงข้อความจาก TextBox และตัดช่องว่าง

            switch (supportAircraftTxt)
            {
                case "Airbus A319":
                    SupportTrue();
                    break;

                case "Airbus A320":
                    SupportTrue();
                    break;

                case "Airbus A320neo":
                    SupportTrue();
                    break;

                case "Airbus A321":
                    SupportTrue();
                    break;

                case "Airbus A321XLR":
                    SupportTrue();
                    break;

                case "Airbus A350-1000":
                    SupportTrue();
                    break;

                case "Airbus A380":
                    SupportTrue();
                    break;

                case "Boeing 737-500":
                    SupportTrue();
                    break;

                case "Boeing 737-800":
                    SupportTrue();
                    break;

                case "Boeing 737-900ER":
                    SupportTrue();
                    break;

                case "Boeing 737 MAX9":
                    SupportTrue();
                    break;

                case "Boeing 747-400":
                    SupportTrue();
                    break;

                case "Boeing 777-300ER":
                    SupportTrue();
                    break;

                case "Boeing 777F":
                    SupportTrue();
                    break;

                case "Boeing 787-10":
                    SupportTrue();
                    break;

                case "Boeing 787-9":
                    SupportTrue();
                    break;

                default:
                    SupportFalse();
                    break;
            }

            void SupportTrue()
            {
                Custom_VLTA7_1_Throttle_Status.Visible = true;
                Custom_VLTA7_Throttle_and_Engine_Text.Visible = true;
                ProgressBarThrottleWhite.Visible = true;
                ProgressBarThrottleRed.Visible = true;
                Custom_VLTA4_Throttle_Status.Visible = true;
                Custom_VLTA4_Progress_Status.Visible = false;
            }
            void SupportFalse()
            {
                Custom_VLTA7_1_Throttle_Status.Visible = false;
                Custom_VLTA7_Throttle_and_Engine_Text.Visible = false;
                ProgressBarThrottleWhite.Visible = false;
                ProgressBarThrottleRed.Visible = false;
                Custom_VLTA4_Throttle_Status.Visible = false;
                Custom_VLTA4_Progress_Status.Visible = true;
            }
        }
        /*
        private void StatusUpdateEnginePowerText()
        {
            string processName = "aerofly_fs_4";


            int baseOffset = 0x016D8A88;
            int[] offsets = new int[] { 0xD8 , 0x40 , 0x0 , 0x140 , 0x28 , 0x0 , 0x360 };

            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                Custom_VLTA7_2_Engine_Status.Text = "Process Not Found";
                return;
            }

            Process process = processes[0];
            IntPtr hProcess = OpenProcess(0x0010, false, process.Id);

            // 1. ดึง Base Address ของ Process หลักโดยตรง (ไม่ต้องใช้ moduleName)
            IntPtr mainModuleBase = process.MainModule.BaseAddress;

            // 2. Resolve Pointer จาก Base Address + baseOffset
            IntPtr targetAddress = ResolvePointer(hProcess, mainModuleBase + baseOffset, offsets);

            if (targetAddress != IntPtr.Zero)
            {
                byte[] buffer = new byte[8];
                if (ReadProcessMemory(hProcess, targetAddress, buffer, buffer.Length, out _))
                {
                    double current = BitConverter.ToDouble(buffer, 0);

                    double min = 0.0;
                    double max = 100.0;

                    int minenginepower = 0;
                    int maxenginepower = 152;

                    double clampedCurrent = Math.Max(0.0, Math.Min(current, 1.0));
                    double calculatedCurrent = min + (clampedCurrent * (max - min));

                    // คำนวณความกว้างของ ProgressBar (พิกัด 0 ถึง 152)
                    double calculatedCurrentEnginePower = maxenginepower + (clampedCurrent * (minenginepower - maxenginepower));
                    int currentEnginePower = (int)Math.Round(calculatedCurrentEnginePower);

                    // แสดงผล
                    Custom_VLTA7_2_Engine_Status.Text = $"{calculatedCurrent:F2}%";
                    ProgressBarEngineWhite.Size = new Size(currentEnginePower, 10);
                }
                else
                {
                    Custom_VLTA7_2_Engine_Status.Text = "Read Error";
                }
            }
            else
            {
                Custom_VLTA7_2_Engine_Status.Text = "Bad Pointer";
            }

            CloseHandle(hProcess);
        }
        */

        public static IntPtr ResolvePointer(IntPtr hProcess, IntPtr baseAddress, int[] offsets)
        {
            byte[] buffer = new byte[8]; // รองรับ 64-bit Process
            IntPtr currentAddress = baseAddress;

            for (int i = 0; i < offsets.Length; i++)
            {
                if (!ReadProcessMemory(hProcess, currentAddress, buffer, buffer.Length, out _))
                    return IntPtr.Zero;

                IntPtr nextAddress = (IntPtr)BitConverter.ToInt64(buffer, 0);
                if (nextAddress == IntPtr.Zero)
                    return IntPtr.Zero;

                currentAddress = nextAddress + offsets[i];
            }

            return currentAddress;
        }
    }
}