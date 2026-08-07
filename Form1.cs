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

namespace FS4_Flight_Tracker
{
    public partial class Form1 : Form
    {

        // กำหนดชื่อให้ตรงกับ Shared Memory ของ Bridge DLL ที่คุณใช้
        private const string MAP_NAME = "AeroflyBridgeData";

        private MemoryMappedFile mmf;
        private MemoryMappedViewAccessor accessor;
        private Timer timer;

        // พิกัดปลายทางที่แกะได้จากไฟล์ .mme
        private double arrivalLat = 0;
        private double arrivalLon = 0;

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

            LoadAircraftName();

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

        private void FlightPlanLoaded()
        {
            // หาตำแหน่งโฟลเดอร์ Documents/Aerofly FS 4/main.mme
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string mmeFilePath = Path.Combine(myDocs, "Aerofly FS 4", "main.mcf");

            if (!File.Exists(mmeFilePath))
            {
                MessageBox.Show("ไม่พบไฟล์ main.mcf ของ Aerofly FS 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // อ่านเนื้อหาข้อความทั้งหมดในไฟล์ .mme
                string mmeContent = File.ReadAllText(mmeFilePath);

                // แกะเอาชื่อสนามบิน
                string departureICAO = ExtractValue(mmeContent, "departure_id");
                string arrivalICAO = ExtractValue(mmeContent, "destination_id");

                // แกะเอาพิกัด Lat/Lon ของสนามบินปลายทาง
                double.TryParse(ExtractValue(mmeContent, "destination_lat"), out arrivalLat);
                double.TryParse(ExtractValue(mmeContent, "destination_lon"), out arrivalLon);

                // แสดงผลบนหน้าจอ WinForms
                RouteText.Text = $"Route: {departureICAO} ➔ {arrivalICAO}";
                DestinationCoordText.Text = $"Dest Coord: {arrivalLat:F4}, {arrivalLon:F4}";

                MessageBox.Show($"โหลด Flight Plan {departureICAO} -> {arrivalICAO} เรียบร้อยแล้ว!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการอ่านไฟล์: " + ex.Message);
            }
        }

        // ฟังก์ชันช่วยค้นหาข้อความค่า Value จาก Tag ในไฟล์ .mme
        private string ExtractValue(string text, string key)
        {
            // ใช้ Regex ค้นหาข้อความรูปแบบ name="key" value="xxx"
            Match match = Regex.Match(text, $@"name=""{key}""\s+value=""([^""]+)""");
            return match.Success ? match.Groups[1].Value : "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlightPlanLoaded();
        }

        private void LoadNameAircraft_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadAircraftName()
        {
            string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string mcfPath = System.IO.Path.Combine(myDocs, "Aerofly FS 4", "main.mcf");

            // ดึงชื่อเครื่องบินดิบจากไฟล์ .mcf (เช่น b787_9)
            string rawAircraft = Form1.GetCurrentAircraftRaw(mcfPath);

            // แปลงชื่อเป็นชื่อเต็ม
            string fullName = ConvertAircraftName(rawAircraft);

            // โยนค่าลง .Text ของ Label เพื่อแสดงผลบนหน้าจอ
            AircraftName.Text = fullName;
        }

        private string ConvertAircraftName(string rawName)
        {
            switch (rawName)
            {
                case "a319":
                    return "Airbus A319";
                case "a320":
                    return "Airbus A320";
                default:
                    return rawName; // ถ้าไม่เจอในรายการ ให้แสดงชื่อเดิม
            }
        }
    }
}
