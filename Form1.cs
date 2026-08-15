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


        // 1. นำเข้า API ของ Windows เพื่อควบคุมการลากหน้าต่าง
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;


        private String aircraftNameCustomHUD , aircraftLiveryCustomHUD , departureNameCustomHUD , arrivalNameCustomHUD;
        private double progressCustomHUD;
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
        }

        private void timerstatusaeroflyfs4_Tick(object sender, EventArgs e)
        {
            StatusUpdateDepatureText();
            StatusUpdateArrivalText();
            StatusUpdateAircraftNameText();
            StatusUpdateTimerClockText();
            StatusUpdatePlayerPositionText();
            StatusUpdateDepatureAndArrivalText();
            Custom_VLTA5_AircraftName_Status.Text = "PROGRESS : " + $"{progressCustomHUD:F2} %" + "\n" + "AIRCRAFT : " + aircraftNameCustomHUD + "\n" + "LIVERY : " + aircraftLiveryCustomHUD + "\n" + "DEP : "+ departureNameCustomHUD + "\n" + "ARR : " + arrivalNameCustomHUD;
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

                double speedstatus = data2.IndicatedAirspeed * (6.1075 / Math.PI); //* (6.1075 / Math.PI); // 6.1075

                double rollstatus = data3.Roll * (191 / Math.PI); // 6.1075

                double pitchstatus = data4.Airspeed * (500 / Math.PI); // 6.1075


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

                RollStatus.Text = "Roll : " + $"{rollstatus:F0}";

                PitchStatus.Text = "Pitch : " + $"{pitchstatus:F0}";

                // Volanta Style
                VLTA_SPD.Text = "SPD: " + $"{speedstatus:F0}" +"kts";
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
            string ceValue = Form1.GetCEStringValue(processName, baseOffset, offsets , 4);

            // แสดงผลบนหน้าจอ Form
            DepartureText.Text = ceValue;
            VLTA_DEP_Status.Text = ceValue;

            departureNameCustomHUD = ceValue;
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

            arrivalNameCustomHUD = ceValue;
        }
        private void StatusUpdateDepatureAndArrivalText()
        {
            string processName = "aerofly_fs_4";

            // Departure

            int baseOffsetDep = 0x0173BB88;
            int[] offsetsDep = new int[] { 0x8, 0x358, 0xC8, 0x10 };
            
            // Arrival
            int baseOffsetArr = 0x016D8A88;
            int[] offsetsArr = new int[] { 0xD0, 0x358, 0xB0, 0x10, 0x100, 0x0 };
            
            string ceValueDep = Form1.GetCEStringValue(processName, baseOffsetDep, offsetsDep, 4);
            string ceValueArr = Form1.GetCEStringValue(processName, baseOffsetArr, offsetsArr, 4);

            Custom_VLTA1_DEP_and_ARR_Status.Text = ceValueDep + " - " + ceValueArr;
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

            long ceValueLivery = Form1.GetCEIntValue8Byte(processName, baseOffset, offsetslivery);

            string aircraftName = "";
            string aircraftliveryname = "";

            switch (ceValueLivery)
            {
                /*
                
                case XXXXXXXXXXXXXXXXXXX:
                    aircraftliveryname = "XXXXXXXXXXX";
                    break;

                */

                // Airbus A319
                case 7453010347523466593:
                    aircraftliveryname = "Aer Lingus";
                    break;

                case 7953764200926570849:
                    aircraftliveryname = "Air France";
                    break;

                case 8247605414979594593:
                    aircraftliveryname = "Air Maurits";
                    break;

                case 7953754309717159009:
                    aircraftliveryname = "Allegiantt";
                    break;

                case 7953747721454972257:
                    aircraftliveryname = "American Airlines";
                    break;

                case 7953747742644860513:
                    aircraftliveryname = "Avianca";
                    break;

                case 7667766916028922465:
                    aircraftliveryname = "Azerbaijan";
                    break;

                case 6875711763198665058:
                    aircraftliveryname = "Bangkok Air";
                    break;

                case 7016448109311256674:
                    aircraftliveryname = "Bhutan Airlines";
                    break;

                case 7954879165879120482:
                    aircraftliveryname = "Braathens";
                    break;

                case 7954883517100421730:
                    aircraftliveryname = "Brazilian Air Force";
                    break;

                case 6874864031461634658:
                    aircraftliveryname = "Brazilian Air Force";
                    break;

                case 6874871727942890082:
                    aircraftliveryname = "British Airways";
                    break;

                case 8317134158379184738:
                    aircraftliveryname = "Brussels";
                    break;

                case 7161128437789386083:
                    aircraftliveryname = "Cebu Pacific";
                    break;

                case 7161128519192373347:
                    aircraftliveryname = "Chair";
                    break;

                case 7018120466395654243:
                    aircraftliveryname = "China Eastern";
                    break;

                case 7018142542595127651:
                    aircraftliveryname = "Cyprus";
                    break;

                case 7018142486508173923:
                    aircraftliveryname = "Czech Airlines";
                    break;

                case 7018142456729068900:
                    aircraftliveryname = "Delta";
                    break;

                case 7021790636159627876:
                    aircraftliveryname = "Drukair";
                    break;

                case 7022349226955989349:
                    aircraftliveryname = "easyJet";
                    break;

                case 6878234038880133477:
                    aircraftliveryname = "easyJet.com";
                    break;

                case 6877675448133642598:
                    aircraftliveryname = "Finnair";
                    break;

                case 7599664260938098023:
                    aircraftliveryname = "Germanwings";
                    break;

                case 7600231608837109096:
                    aircraftliveryname = "Hungarian Air Force";
                    break;

                case 7600212951683129961:
                    aircraftliveryname = "Iberia";
                    break;

                case 8607057704791405673:
                    aircraftliveryname = "ITA Airways";
                    break;

                case 8607057756365807980:
                    aircraftliveryname = "LATAM";
                    break;

                case 8317692663057249644:
                    aircraftliveryname = "Luftthansa";
                    break;

                case 7597696169466751088:
                    aircraftliveryname = "Philippine Airlines";
                    break;

                case 8028334204837785458:
                    aircraftliveryname = "Royal Jordanian";
                    break;

                case 8244227710998114162:
                    aircraftliveryname = "Royal Air";
                    break;

                case 8244227710997717363:
                    aircraftliveryname = "Scandinavian Airlines";
                    break;

                case 7019259560576642163:
                    aircraftliveryname = "Slovak Republic";
                    break;

                case 7019269490473529459:
                    aircraftliveryname = "Spirit";
                    break;

                case 7019269533439981427:
                    aircraftliveryname = "SWISS";
                    break;

                case 7019269533440434548:
                    aircraftliveryname = "TAP Portugal";
                    break;

                case 7019269537499605364:
                    aircraftliveryname = "Tibet";
                    break;

                case 8244227741196383604:
                    aircraftliveryname = "Tunisair";
                    break;

                case 8244230979785944693:
                    aircraftliveryname = "United";
                    break;

                case 8247051282627194742:
                    aircraftliveryname = "Volaris";
                    break;

                case 8241980343824707446:
                    aircraftliveryname = "Volotea";
                    break;

                case 8243679041994192246:
                    aircraftliveryname = "Vueling";
                    break;

                // Note : Eurowings not supported. idk why.


                // Airbus A320 / A320 neo
                case 8318832826298885473:
                    aircraftliveryname = "Aegean";
                    break;

                case 7453015797936907617:
                    aircraftliveryname = "Aegean";
                    break;

                case 8390043818026755425:
                    aircraftliveryname = "Aeroflot";
                    break;

                case 7019268356283984225:
                    aircraftliveryname = "Air Asia";
                    break;

                case 7814419776623765857:
                    aircraftliveryname = "airberlin";
                    break;

                case 8244227672309393761:
                    aircraftliveryname = "Air Cairo";
                    break;

                case 7956004992739076449:
                    aircraftliveryname = "Air China";
                    break;

                case 8318833934300506465:
                    aircraftliveryname = "Air Corsica";
                    break;

                case 7310590567722936673:
                    aircraftliveryname = "Air Cote d'Ivoire";
                    break;

                case 7020374503636232545:
                    aircraftliveryname = "Air Jamaica";
                    break;

                case 7594316270505453921:
                    aircraftliveryname = "Air India";
                    break;

                case 7017559766754027873:
                    aircraftliveryname = "Air Macau";
                    break;

                case 6879078480553863521:
                    aircraftliveryname = "Air New Zeland";
                    break;

                case 7167871828133833057:
                    aircraftliveryname = "Air Seychelles";
                    break;

                case 7956009382262761825:
                    aircraftliveryname = "Aircalin";
                    break;

                case 7019260660054322273:
                    aircraftliveryname = "Alitalia";
                    break;

                case 7953747721454710369:
                    aircraftliveryname = "All Nippon Airlines";
                    break;

                case 7956009382261648993:
                    aircraftliveryname = "All Nippon Airlines";
                    break;

                case 7163384699739206753:
                    aircraftliveryname = "Atlantic Airways";
                    break;

                case 7953754357213918561:
                    aircraftliveryname = "Austrian";
                    break;

                case 7953765296461806177:
                    aircraftliveryname = "Azores";
                    break;

                case 7953765296361536097:
                    aircraftliveryname = "Azul";
                    break;

                case 7953760941096395106:
                    aircraftliveryname = "Bamboo";
                    break;

                case 7953760924034425186:
                    aircraftliveryname = "Batik";
                    break;

                case 8097324114306687331:
                    aircraftliveryname = "Cathay Pacific";
                    break;

                case 8030867432600594531:
                    aircraftliveryname = "China Southern";
                    break;

                case 7741240723843082595:
                    aircraftliveryname = "Citilink";
                    break;

                case 7741250632248356707:
                    aircraftliveryname = "Condor";
                    break;

                case 7741240676782466404:
                    aircraftliveryname = "Delta";
                    break;

                case 7742366576538907236:
                    aircraftliveryname = "Druk Air";
                    break;

                case 7742925167335268709:
                    aircraftliveryname = "easyJet";
                    break;

                case 7453010395036546405:
                    aircraftliveryname = "Eurowings";
                    break;

                case 8316289750510429285:
                    aircraftliveryname = "Edelweiss";
                    break;

                case 8320807549082889318:
                    aircraftliveryname = "Fly Arystan";
                    break;

                case 8320808648846175334:
                    aircraftliveryname = "Flynas";
                    break;

                case 8243110641761481318:
                    aircraftliveryname = "Frontier";
                    break;

                case 8244227655246574951:
                    aircraftliveryname = "Gulf Air";
                    break;

                case 8243116057714844008:
                    aircraftliveryname = "Hainan Airlines";
                    break;
                    
                case 8315178114391305064:
                    aircraftliveryname = "Hong Kong Express";
                    break;

                case 8315159392528196201:
                    aircraftliveryname = "Iberia";
                    break;

                case 8244243082818907753:
                    aircraftliveryname = "Indigo";
                    break;

                case 8315159392527938665:
                    aircraftliveryname = "ITA Airways";
                    break;
                    
                case 8314052166922428778:
                    aircraftliveryname = "Jazeera";
                    break;

                case 8390876178460992874:
                    aircraftliveryname = "JetSmart";
                    break;

                case 8390876178159001964:
                    aircraftliveryname = "LATAM";
                    break;
                    
                case 8243662566316073324:
                    aircraftliveryname = "LATAM Green";
                    break;

                case 8390876208525763946:
                    aircraftliveryname = "Jetstar";
                    break;

                case 8244243082819559788:
                    aircraftliveryname = "LAN";
                    break;

                case 8317692632992866669:
                    aircraftliveryname = "Malta";
                    break;

                case 8315183577523449710:
                    aircraftliveryname = "Northwest";
                    break;

                case 8317692662771705200:
                    aircraftliveryname = "Peach";
                    break;

                case 8319122075099293040:
                    aircraftliveryname = "Pegasus";
                    break;

                case 8319122075501554800:
                    aircraftliveryname = "Play";
                    break;

                case 7954879191867275123:
                    aircraftliveryname = "S7";
                    break;

                case 8315183577523435379:
                    aircraftliveryname = "S7";
                    break;

                case 8319122075502731635:
                    aircraftliveryname = "Scandinavian Airlines";
                    break;
                    
                case 8319122079629665139:
                    aircraftliveryname = "Scoot flyscoot.com";
                    break;

                case 7954879191867287667:
                    aircraftliveryname = "Shenzhen Airlines";
                    break;

                case 7957404748500658547:
                    aircraftliveryname = "Sichuan Airlines";
                    break;

                case 8319122079630322547:
                    aircraftliveryname = "Sky";
                    break;

                case 8319120932923338867:
                    aircraftliveryname = "Spirit";
                    break;
                    
                case 7311994657274164339:
                    aircraftliveryname = "Starflyer";
                    break;

                case 7957404740095670131:
                    aircraftliveryname = "Swiss";
                    break;
                    
                case 7311994713126041459:
                    aircraftliveryname = "Swiss";
                    break;

                case 7957404740095926644:
                    aircraftliveryname = "TAM";
                    break;

                case 7957404740096123252:
                    aircraftliveryname = "TAP Portugal";
                    break;

                case 7311994713126494580:
                    aircraftliveryname = "TAP Portugal";
                    break;

                case 7957123243660634486:
                    aircraftliveryname = "Vietnam Airlines";
                    break;

                case 7308604897319283062:
                    aircraftliveryname = "Vietnam Airlines";
                    break;

                case 7305245833462770038:
                    aircraftliveryname = "Vistara";
                    break;

                case 7305245833144199542:
                    aircraftliveryname = "Viva";
                    break;

                case 7310302560134131574:
                    aircraftliveryname = "Volaris";
                    break;

                case 7306930319501129078:
                    aircraftliveryname = "Vueling";
                    break;

                case 7310021012563323255:
                    aircraftliveryname = "Wizz Air";
                    break;

                // Airbus A321 / A321 XLR

                case 7022085309369313633:
                    aircraftliveryname = "Air Busan";
                    break;

                case 7020655948548172129:
                    aircraftliveryname = "Air Canada";
                    break;
                    
                case 7953764261056112993:
                    aircraftliveryname = "Air Transat";
                    break;

                case 7953745543621604193:
                    aircraftliveryname = "Asiana";
                    break;

                case 7592913319521840995:
                    aircraftliveryname = "Condor";
                    break;

                case 8313489259901120355:
                    aircraftliveryname = "Condor";
                    break;
                    
                case 8313489200039880036:
                    aircraftliveryname = "Delta";
                    break;
                
                case 8244227664071386468:
                    aircraftliveryname = "Delta";
                    break;

                case 8244227745609508709:
                    aircraftliveryname = "EgyptAir";
                    break;

                case 7454136200184559205:
                    aircraftliveryname = "EVA Air";
                    break;

                case 7454136200437066086:
                    aircraftliveryname = "Finnair";
                    break;
                    
                case 8315159391455438186:
                    aircraftliveryname = "Jet2Holidays";
                    break;

                case 8315181394572895594:
                    aircraftliveryname = "Jet2Holidays";
                    break;

                case 7953745522431713897:
                    aircraftliveryname = "Iberia";
                    break;

                case 8315181395378201962:
                    aircraftliveryname = "jetBlue";
                    break;

                case 8318818614487836010:
                    aircraftliveryname = "Jetstar";
                    break;

                case 8317974210797270378:
                    aircraftliveryname = "Juneyao";
                    break;

                case 8317974159190942060:
                    aircraftliveryname = "LATAM";
                    break;

                case 8317692684214231404:
                    aircraftliveryname = "LATAM";
                    break;

                case 7378692205175010668:
                    aircraftliveryname = "Luftwaffe";
                    break;

                case 7526464355956580721:
                    aircraftliveryname = "Qanot Sharq";
                    break;

                case 7526486264668643697:
                    aircraftliveryname = "Qantas";
                    break;

                case 7813850272768418163:
                    aircraftliveryname = "Scandinavian Airlines";
                    break;
                case 8028334204837388659:
                    aircraftliveryname = "Scandinavian Airlines";
                    break;

                case 7813850272767241587:
                    aircraftliveryname = "Small Planet";
                    break;

                case 7813873349727711347:
                    aircraftliveryname = "Spirit";
                    break;

                case 8319100054835983731:
                    aircraftliveryname = "Sunclass";
                    break;
                    
                case 8319100085168863091:
                    aircraftliveryname = "SWISS";
                    break;
                    
                case 8319677328773445491:
                    aircraftliveryname = "SWISS";
                    break;

                case 8319677328689033844:
                    aircraftliveryname = "TransNusa";
                    break;

                case 8316023608551044468:
                    aircraftliveryname = "Turkish Airlines";
                    break;

                case 6874871727792485748:
                    aircraftliveryname = "Turkish Airlines";
                    break;

                case 6878234038795331958:
                    aircraftliveryname = "Vietjet Air";
                    break;

                case 6876259333091715446:
                    aircraftliveryname = "Vietnam Airlines";
                    break;

                case 6876259332774062454:
                    aircraftliveryname = "Viva Aerobus";
                    break;

                case 6877956995907217782:
                    aircraftliveryname = "Viva Aerobus";
                    break;

                case 6877956995906563958:
                    aircraftliveryname = "Volaris";
                    break;

                case 6874584755273561462:
                    aircraftliveryname = "Vueling";
                    break;

                case 6877675448335755639:
                    aircraftliveryname = "Wizz Air";
                    break;

                default:
                    aircraftliveryname = ceValueLivery.ToString();
                    break;

                    /*

                case XXXXXXXXXXXXXXXXXXX:
                    aircraftliveryname = "XXXXXXXXXXX";
                    break;
                    
                     */
            }

            switch (ceValue)
            {
                
                case 959525729:
                    aircraftName = "Airbus A319";
                    break;

                case 808596321:
                    aircraftName = "Airbus A320";
                    break;

                case 825373537:
                    aircraftName = "Airbus A321";
                    break;

                case 808792929:
                    aircraftName = "Airbus A350-1000";
                    break;

                case 808989537:
                    aircraftName = "Airbus A380";
                    break;

                case 926103394:
                    aircraftName = "Boeing 737";
                    break;
                
                case 926168930:
                    aircraftName = "Boeing 747";
                    break;
                
                case 926365538:
                    aircraftName = "Boeing 777";
                    break;
                
                case 926431074:
                    aircraftName = "Boeing 787";
                    break;
                
                case 1668181859:
                    aircraftName = "Concorde";
                    break;
                
                default:
                    VLTA_Name_Aircraft.Text = ceValue.ToString() + "\n";
                    break;

/*
                case XXXXXXXXX:
                    VLTA_Name_Aircraft.Text = "XXXXXXXXXXXX" + "\n";
                    break;
*/
            }
            VLTA_Name_Aircraft.Text = aircraftName + "\n" + aircraftliveryname;
            Custom_VLTA5_AircraftName_Status.Text = aircraftName + "\n" + aircraftliveryname;

            aircraftNameCustomHUD = aircraftName;
            aircraftLiveryCustomHUD = aircraftliveryname;
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
            int[] offsetsPosX = new int[] { 0x20, 0xF0, 0x8, 0x90, 0x0, 0x0 , 0x2A8 };

            int baseOffsetPosY = 0x016D8A88;
            int[] offsetsPosY = new int[] { 0x10, 0x40, 0x0, 0x28, 0x8 , 0x0, 0x2B0 };

            int baseOffsetPosZ = 0x0181B898;
            int[] offsetsPosZ = new int [] { 0x5C8 , 0xD0 , 0x308 , 0x150 , 0x658 , 0x0 , 0x2B8 };

            double playerPosX = Form1.GetCEDoubleValue(processName, baseOffsetPosX, offsetsPosX);
            double playerPosY = Form1.GetCEDoubleValue(processName, baseOffsetPosY, offsetsPosY);
            double playerPosZ = Form1.GetCEDoubleValue(processName, baseOffsetPosZ, offsetsPosZ);

            playerX = playerPosX;
            playerY = playerPosY;
            playerZ = playerPosZ;

            // Departure Position
            int baseOffsetDepPosX = 0x0173BB68;
            int[] offsetsDepPosX = new int[] { 0x20 , 0xD0 , 0x358 , 0x28 , 0x90 , 0x0 , 0x30 };
            int baseOffsetDepPosY = 0x016D8A88;
            int[] offsetsDepPosY = new int[] { 0xD0 , 0x358 , 0x28 , 0x60 , 0x0 , 0x8 , 0x30 };
            int baseOffsetDepPosZ = 0x016D8A88;
            int[] offsetsDepPosZ = new int[] { 0xD0 , 0x0 , 0x348 , 0x98 , 0x0 , 0x50 };

            double playerDepPosX = Form1.GetCEDoubleValue(processName, baseOffsetDepPosX, offsetsDepPosX);
            double playerDepPosY = Form1.GetCEDoubleValue(processName, baseOffsetDepPosY, offsetsDepPosY);
            double playerDepPosZ = Form1.GetCEDoubleValue(processName, baseOffsetDepPosZ, offsetsDepPosZ);

            depXlocked = playerDepPosX;
            depYlocked = playerDepPosY;
            depZlocked = playerDepPosZ;

            // Arrival Position
            int baseOffsetArrPosX = 0x0182F768;
            int[] offsetsArrPosX = new int[] { 0x60 , 0x20 , 0x20 , 0x0 , 0x348 , 0xE0 , 0x468 };
            int baseOffsetArrPosY = 0x016D8A88;
            int[] offsetsArrPosY = new int[] { 0xD0 , 0x358 , 0x28 , 0x60 , 0x0 , 0x8 , 0x30 };
            int baseOffsetArrPosZ = 0x0182F768;
            int[] offsetsArrPosZ = new int[] { 0x8B0 , 0x20 , 0x20 , 0x358 , 0xF8 , 0x4B0 , 0x18 };

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
            Custom_VLTA4_Progress_Status.Text = $"Progress: {progressPercent:F2} %";
            lblCurrent.Text = "Remaining Distance: " + $"{remainingKm:F2}" + " NM";

            
            lblXYZ.Text = currentsizepanelVolantastyle.ToString();

            ProgressBarStatus.Size = new Size(currentsizepanelVolantastyle, 7);
            ProgressSlider.Value = currentsizepanelVolantaCustomstyle;

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
        }

        private void Quit_Volanta_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            PanelTaskbar.Visible = true;
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
    }
}