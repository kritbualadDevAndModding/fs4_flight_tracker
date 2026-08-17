
namespace FS4_Flight_Tracker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusaeroflyfs4 = new System.Windows.Forms.Label();
            this.timerstatusaeroflyfs4 = new System.Windows.Forms.Timer(this.components);
            this.SpeedStatus = new System.Windows.Forms.Label();
            this.AltitideStatus = new System.Windows.Forms.Label();
            this.PitchStatus = new System.Windows.Forms.Label();
            this.RollStatus = new System.Windows.Forms.Label();
            this.AircraftandLivery = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblXYZ = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.DeparturePosition = new System.Windows.Forms.Label();
            this.ArrivalPosition = new System.Windows.Forms.Label();
            this.PlayerPosition = new System.Windows.Forms.Label();
            this.DepartureText = new System.Windows.Forms.Label();
            this.ArrivalText = new System.Windows.Forms.Label();
            this.StartVolantaStyle = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.timerreadingmemory = new System.Windows.Forms.Timer(this.components);
            this.panelVolantaEnabled = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelVolantaStyle = new System.Windows.Forms.Panel();
            this.Switch_Color_Background = new System.Windows.Forms.Button();
            this.Quit_Volanta = new System.Windows.Forms.Button();
            this.Next_HUD = new System.Windows.Forms.Button();
            this.ImageCustomHUD1 = new System.Windows.Forms.Panel();
            this.ProgressBarThrottleWhite = new System.Windows.Forms.Panel();
            this.ProgressBarThrottleRed = new System.Windows.Forms.Panel();
            this.Custom_VLTA1_DEP_and_ARR_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA2_Speed_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA2_Speed_HideNumber_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA3_Altitude_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA4_Throttle_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA5_Clock_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA6_Debug_Status = new System.Windows.Forms.Label();
            this.Custom_VLTA7_Debug_Status = new System.Windows.Forms.Label();
            this.ImageHUDVolantaStyle = new System.Windows.Forms.Panel();
            this.ProgressBarStatus = new System.Windows.Forms.Panel();
            this.ProgressBar = new System.Windows.Forms.Panel();
            this.VLTA_ARR_Text = new System.Windows.Forms.Label();
            this.VLTA_DEP_Text = new System.Windows.Forms.Label();
            this.VLTA_ARR_Status = new System.Windows.Forms.Label();
            this.VLTA_DEP_Status = new System.Windows.Forms.Label();
            this.VLTA_ALT = new System.Windows.Forms.Label();
            this.VLTA_TIME = new System.Windows.Forms.Label();
            this.VLTA_Name_Aircraft = new System.Windows.Forms.Label();
            this.VLTA_SPD = new System.Windows.Forms.Label();
            this.VLTA_SPD_HideNumber = new System.Windows.Forms.Label();
            this.PanelTaskbar = new System.Windows.Forms.Panel();
            this.ButtonExitProgram = new System.Windows.Forms.Button();
            this.TaskbarName = new System.Windows.Forms.Label();
            this.notificationopengame = new System.Windows.Forms.Panel();
            this.ButtonChangelog = new System.Windows.Forms.Button();
            this.ViewChangelogList = new System.Windows.Forms.RichTextBox();
            this.VersionText = new System.Windows.Forms.Label();
            this.pleaserestart = new System.Windows.Forms.Label();
            this.pleaseopenaerofly = new System.Windows.Forms.Label();
            this.textbox_liveryname = new System.Windows.Forms.TextBox();
            this.label_liveryname = new System.Windows.Forms.Label();
            this.comboBox_selectaircraft = new System.Windows.Forms.ComboBox();
            this.label_aircraftname = new System.Windows.Forms.Label();
            this.label_blocklanguage = new System.Windows.Forms.Label();
            this.ProgressSlider = new CustomSliderApp.ImageSlider();
            this.panel1.SuspendLayout();
            this.panelVolantaEnabled.SuspendLayout();
            this.panelVolantaStyle.SuspendLayout();
            this.ImageCustomHUD1.SuspendLayout();
            this.ImageHUDVolantaStyle.SuspendLayout();
            this.PanelTaskbar.SuspendLayout();
            this.notificationopengame.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusaeroflyfs4
            // 
            this.statusaeroflyfs4.BackColor = System.Drawing.Color.Black;
            this.statusaeroflyfs4.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusaeroflyfs4.ForeColor = System.Drawing.Color.Red;
            this.statusaeroflyfs4.Location = new System.Drawing.Point(0, 37);
            this.statusaeroflyfs4.Name = "statusaeroflyfs4";
            this.statusaeroflyfs4.Size = new System.Drawing.Size(884, 33);
            this.statusaeroflyfs4.TabIndex = 0;
            this.statusaeroflyfs4.Text = "❌ Aerofly FS 4 Disconnected";
            this.statusaeroflyfs4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerstatusaeroflyfs4
            // 
            this.timerstatusaeroflyfs4.Enabled = true;
            this.timerstatusaeroflyfs4.Tick += new System.EventHandler(this.timerstatusaeroflyfs4_Tick);
            // 
            // SpeedStatus
            // 
            this.SpeedStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedStatus.ForeColor = System.Drawing.Color.White;
            this.SpeedStatus.Location = new System.Drawing.Point(515, 69);
            this.SpeedStatus.Name = "SpeedStatus";
            this.SpeedStatus.Size = new System.Drawing.Size(361, 36);
            this.SpeedStatus.TabIndex = 1;
            this.SpeedStatus.Text = "Speed";
            this.SpeedStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AltitideStatus
            // 
            this.AltitideStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AltitideStatus.ForeColor = System.Drawing.Color.White;
            this.AltitideStatus.Location = new System.Drawing.Point(515, 105);
            this.AltitideStatus.Name = "AltitideStatus";
            this.AltitideStatus.Size = new System.Drawing.Size(361, 36);
            this.AltitideStatus.TabIndex = 2;
            this.AltitideStatus.Text = "Altitude";
            this.AltitideStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PitchStatus
            // 
            this.PitchStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PitchStatus.ForeColor = System.Drawing.Color.White;
            this.PitchStatus.Location = new System.Drawing.Point(515, 141);
            this.PitchStatus.Name = "PitchStatus";
            this.PitchStatus.Size = new System.Drawing.Size(361, 36);
            this.PitchStatus.TabIndex = 3;
            this.PitchStatus.Text = "Pitch";
            this.PitchStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RollStatus
            // 
            this.RollStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollStatus.ForeColor = System.Drawing.Color.White;
            this.RollStatus.Location = new System.Drawing.Point(515, 177);
            this.RollStatus.Name = "RollStatus";
            this.RollStatus.Size = new System.Drawing.Size(361, 36);
            this.RollStatus.TabIndex = 4;
            this.RollStatus.Text = "Roll";
            this.RollStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AircraftandLivery
            // 
            this.AircraftandLivery.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AircraftandLivery.ForeColor = System.Drawing.Color.White;
            this.AircraftandLivery.Location = new System.Drawing.Point(515, 213);
            this.AircraftandLivery.Name = "AircraftandLivery";
            this.AircraftandLivery.Size = new System.Drawing.Size(361, 36);
            this.AircraftandLivery.TabIndex = 5;
            this.AircraftandLivery.Text = "AircraftandLivery";
            this.AircraftandLivery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label_aircraftname);
            this.panel1.Controls.Add(this.comboBox_selectaircraft);
            this.panel1.Controls.Add(this.label_liveryname);
            this.panel1.Controls.Add(this.textbox_liveryname);
            this.panel1.Controls.Add(this.lblProgress);
            this.panel1.Controls.Add(this.lblXYZ);
            this.panel1.Controls.Add(this.lblCurrent);
            this.panel1.Controls.Add(this.DeparturePosition);
            this.panel1.Controls.Add(this.ArrivalPosition);
            this.panel1.Controls.Add(this.PlayerPosition);
            this.panel1.Controls.Add(this.DepartureText);
            this.panel1.Controls.Add(this.ArrivalText);
            this.panel1.Controls.Add(this.StartVolantaStyle);
            this.panel1.Controls.Add(this.SpeedStatus);
            this.panel1.Controls.Add(this.label_blocklanguage);
            this.panel1.Controls.Add(this.AltitideStatus);
            this.panel1.Controls.Add(this.PitchStatus);
            this.panel1.Controls.Add(this.RollStatus);
            this.panel1.Controls.Add(this.AircraftandLivery);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 498);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(525, 260);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(347, 25);
            this.lblProgress.TabIndex = 15;
            this.lblProgress.Text = "lblProgress Text:";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblXYZ
            // 
            this.lblXYZ.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXYZ.ForeColor = System.Drawing.Color.White;
            this.lblXYZ.Location = new System.Drawing.Point(9, 74);
            this.lblXYZ.Name = "lblXYZ";
            this.lblXYZ.Size = new System.Drawing.Size(279, 31);
            this.lblXYZ.TabIndex = 17;
            this.lblXYZ.Text = "lblXYZ Text:";
            // 
            // lblCurrent
            // 
            this.lblCurrent.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.ForeColor = System.Drawing.Color.White;
            this.lblCurrent.Location = new System.Drawing.Point(521, 285);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(351, 25);
            this.lblCurrent.TabIndex = 16;
            this.lblCurrent.Text = "lblCurrent Text:";
            this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DeparturePosition
            // 
            this.DeparturePosition.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeparturePosition.ForeColor = System.Drawing.Color.White;
            this.DeparturePosition.Location = new System.Drawing.Point(8, 229);
            this.DeparturePosition.Name = "DeparturePosition";
            this.DeparturePosition.Size = new System.Drawing.Size(252, 90);
            this.DeparturePosition.TabIndex = 13;
            this.DeparturePosition.Text = "Departure Position:\r\nX =\r\nY = \r\nZ =";
            this.DeparturePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ArrivalPosition
            // 
            this.ArrivalPosition.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArrivalPosition.ForeColor = System.Drawing.Color.White;
            this.ArrivalPosition.Location = new System.Drawing.Point(8, 339);
            this.ArrivalPosition.Name = "ArrivalPosition";
            this.ArrivalPosition.Size = new System.Drawing.Size(252, 90);
            this.ArrivalPosition.TabIndex = 14;
            this.ArrivalPosition.Text = "Arrival Position:\r\nX =\r\nY = \r\nZ =";
            this.ArrivalPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerPosition
            // 
            this.PlayerPosition.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerPosition.ForeColor = System.Drawing.Color.White;
            this.PlayerPosition.Location = new System.Drawing.Point(8, 129);
            this.PlayerPosition.Name = "PlayerPosition";
            this.PlayerPosition.Size = new System.Drawing.Size(252, 90);
            this.PlayerPosition.TabIndex = 12;
            this.PlayerPosition.Text = "Player Position:\r\nX =\r\nY = \r\nZ =";
            this.PlayerPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DepartureText
            // 
            this.DepartureText.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DepartureText.ForeColor = System.Drawing.Color.White;
            this.DepartureText.Location = new System.Drawing.Point(686, 378);
            this.DepartureText.Name = "DepartureText";
            this.DepartureText.Size = new System.Drawing.Size(190, 29);
            this.DepartureText.TabIndex = 11;
            this.DepartureText.Text = "Departure Text:";
            this.DepartureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ArrivalText
            // 
            this.ArrivalText.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArrivalText.ForeColor = System.Drawing.Color.White;
            this.ArrivalText.Location = new System.Drawing.Point(705, 407);
            this.ArrivalText.Name = "ArrivalText";
            this.ArrivalText.Size = new System.Drawing.Size(171, 39);
            this.ArrivalText.TabIndex = 10;
            this.ArrivalText.Text = "Arrival Text:";
            this.ArrivalText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StartVolantaStyle
            // 
            this.StartVolantaStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.StartVolantaStyle.Location = new System.Drawing.Point(340, 452);
            this.StartVolantaStyle.Name = "StartVolantaStyle";
            this.StartVolantaStyle.Size = new System.Drawing.Size(189, 46);
            this.StartVolantaStyle.TabIndex = 8;
            this.StartVolantaStyle.Text = "Start Volanta Style";
            this.StartVolantaStyle.UseVisualStyleBackColor = true;
            this.StartVolantaStyle.Click += new System.EventHandler(this.button1_Click);
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.Color.Transparent;
            this.Restart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Restart.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restart.ForeColor = System.Drawing.Color.White;
            this.Restart.Location = new System.Drawing.Point(686, 452);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(198, 46);
            this.Restart.TabIndex = 9;
            this.Restart.Text = "RESTART";
            this.Restart.UseVisualStyleBackColor = false;
            this.Restart.Visible = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // timerreadingmemory
            // 
            this.timerreadingmemory.Tick += new System.EventHandler(this.timerreadingmemory_Tick);
            // 
            // panelVolantaEnabled
            // 
            this.panelVolantaEnabled.BackColor = System.Drawing.Color.Black;
            this.panelVolantaEnabled.Controls.Add(this.label1);
            this.panelVolantaEnabled.Location = new System.Drawing.Point(0, -1);
            this.panelVolantaEnabled.Name = "panelVolantaEnabled";
            this.panelVolantaEnabled.Size = new System.Drawing.Size(884, 468);
            this.panelVolantaEnabled.TabIndex = 12;
            this.panelVolantaEnabled.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(884, 468);
            this.label1.TabIndex = 0;
            this.label1.Text = "Volanta Opened";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelVolantaStyle
            // 
            this.panelVolantaStyle.BackColor = System.Drawing.Color.Lime;
            this.panelVolantaStyle.Controls.Add(this.Switch_Color_Background);
            this.panelVolantaStyle.Controls.Add(this.Quit_Volanta);
            this.panelVolantaStyle.Controls.Add(this.Next_HUD);
            this.panelVolantaStyle.Controls.Add(this.ImageCustomHUD1);
            this.panelVolantaStyle.Controls.Add(this.ImageHUDVolantaStyle);
            this.panelVolantaStyle.Location = new System.Drawing.Point(0, 0);
            this.panelVolantaStyle.Name = "panelVolantaStyle";
            this.panelVolantaStyle.Size = new System.Drawing.Size(997, 594);
            this.panelVolantaStyle.TabIndex = 14;
            this.panelVolantaStyle.Visible = false;
            // 
            // Switch_Color_Background
            // 
            this.Switch_Color_Background.BackColor = System.Drawing.Color.Transparent;
            this.Switch_Color_Background.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Switch_Color_Background.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Switch_Color_Background.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Switch_Color_Background.ForeColor = System.Drawing.Color.Lime;
            this.Switch_Color_Background.Location = new System.Drawing.Point(-1, 403);
            this.Switch_Color_Background.Name = "Switch_Color_Background";
            this.Switch_Color_Background.Size = new System.Drawing.Size(180, 46);
            this.Switch_Color_Background.TabIndex = 15;
            this.Switch_Color_Background.Text = "Switch Color\r\nBackground";
            this.Switch_Color_Background.UseVisualStyleBackColor = false;
            this.Switch_Color_Background.Click += new System.EventHandler(this.Switch_Color_Background_Click);
            // 
            // Quit_Volanta
            // 
            this.Quit_Volanta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Quit_Volanta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Quit_Volanta.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Quit_Volanta.ForeColor = System.Drawing.Color.Lime;
            this.Quit_Volanta.Location = new System.Drawing.Point(-1, 452);
            this.Quit_Volanta.Name = "Quit_Volanta";
            this.Quit_Volanta.Size = new System.Drawing.Size(180, 46);
            this.Quit_Volanta.TabIndex = 14;
            this.Quit_Volanta.Text = "Quit Volanta Style";
            this.Quit_Volanta.UseVisualStyleBackColor = true;
            this.Quit_Volanta.Click += new System.EventHandler(this.Quit_Volanta_Click);
            // 
            // Next_HUD
            // 
            this.Next_HUD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Next_HUD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Next_HUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.Next_HUD.ForeColor = System.Drawing.Color.Lime;
            this.Next_HUD.Location = new System.Drawing.Point(705, 452);
            this.Next_HUD.Name = "Next_HUD";
            this.Next_HUD.Size = new System.Drawing.Size(180, 46);
            this.Next_HUD.TabIndex = 18;
            this.Next_HUD.Text = "Next HUD";
            this.Next_HUD.UseVisualStyleBackColor = true;
            this.Next_HUD.Click += new System.EventHandler(this.Next_HUD_Click);
            // 
            // ImageCustomHUD1
            // 
            this.ImageCustomHUD1.BackColor = System.Drawing.Color.Transparent;
            this.ImageCustomHUD1.BackgroundImage = global::FS4_Flight_Tracker.Properties.Resources.CustomVolantaHUDStyle;
            this.ImageCustomHUD1.Controls.Add(this.ProgressBarThrottleWhite);
            this.ImageCustomHUD1.Controls.Add(this.ProgressBarThrottleRed);
            this.ImageCustomHUD1.Controls.Add(this.ProgressSlider);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA1_DEP_and_ARR_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA2_Speed_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA2_Speed_HideNumber_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA3_Altitude_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA4_Throttle_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA5_Clock_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA6_Debug_Status);
            this.ImageCustomHUD1.Controls.Add(this.Custom_VLTA7_Debug_Status);
            this.ImageCustomHUD1.Location = new System.Drawing.Point(12, 12);
            this.ImageCustomHUD1.Name = "ImageCustomHUD1";
            this.ImageCustomHUD1.Size = new System.Drawing.Size(860, 446);
            this.ImageCustomHUD1.TabIndex = 17;
            // 
            // ProgressBarThrottleWhite
            // 
            this.ProgressBarThrottleWhite.BackColor = System.Drawing.Color.White;
            this.ProgressBarThrottleWhite.Location = new System.Drawing.Point(708, 81);
            this.ProgressBarThrottleWhite.Name = "ProgressBarThrottleWhite";
            this.ProgressBarThrottleWhite.Size = new System.Drawing.Size(152, 10);
            this.ProgressBarThrottleWhite.TabIndex = 13;
            // 
            // ProgressBarThrottleRed
            // 
            this.ProgressBarThrottleRed.BackColor = System.Drawing.Color.Red;
            this.ProgressBarThrottleRed.Location = new System.Drawing.Point(708, 81);
            this.ProgressBarThrottleRed.Name = "ProgressBarThrottleRed";
            this.ProgressBarThrottleRed.Size = new System.Drawing.Size(152, 10);
            this.ProgressBarThrottleRed.TabIndex = 14;
            // 
            // Custom_VLTA1_DEP_and_ARR_Status
            // 
            this.Custom_VLTA1_DEP_and_ARR_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA1_DEP_and_ARR_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA1_DEP_and_ARR_Status.Location = new System.Drawing.Point(730, 1);
            this.Custom_VLTA1_DEP_and_ARR_Status.Name = "Custom_VLTA1_DEP_and_ARR_Status";
            this.Custom_VLTA1_DEP_and_ARR_Status.Size = new System.Drawing.Size(129, 27);
            this.Custom_VLTA1_DEP_and_ARR_Status.TabIndex = 4;
            this.Custom_VLTA1_DEP_and_ARR_Status.Text = "VTBD - VTCC";
            this.Custom_VLTA1_DEP_and_ARR_Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Custom_VLTA2_Speed_Status
            // 
            this.Custom_VLTA2_Speed_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA2_Speed_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA2_Speed_Status.Location = new System.Drawing.Point(1, 1);
            this.Custom_VLTA2_Speed_Status.Name = "Custom_VLTA2_Speed_Status";
            this.Custom_VLTA2_Speed_Status.Size = new System.Drawing.Size(197, 27);
            this.Custom_VLTA2_Speed_Status.TabIndex = 6;
            this.Custom_VLTA2_Speed_Status.Text = "SPEED : 320 KNOTS";
            this.Custom_VLTA2_Speed_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Custom_VLTA2_Speed_HideNumber_Status
            // 
            this.Custom_VLTA2_Speed_HideNumber_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F);
            this.Custom_VLTA2_Speed_HideNumber_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA2_Speed_HideNumber_Status.Location = new System.Drawing.Point(1, 1);
            this.Custom_VLTA2_Speed_HideNumber_Status.Name = "Custom_VLTA2_Speed_HideNumber_Status";
            this.Custom_VLTA2_Speed_HideNumber_Status.Size = new System.Drawing.Size(197, 27);
            this.Custom_VLTA2_Speed_HideNumber_Status.TabIndex = 10;
            this.Custom_VLTA2_Speed_HideNumber_Status.Text = "SPEED : - KNOTS";
            this.Custom_VLTA2_Speed_HideNumber_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Custom_VLTA3_Altitude_Status
            // 
            this.Custom_VLTA3_Altitude_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA3_Altitude_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA3_Altitude_Status.Location = new System.Drawing.Point(204, 1);
            this.Custom_VLTA3_Altitude_Status.Name = "Custom_VLTA3_Altitude_Status";
            this.Custom_VLTA3_Altitude_Status.Size = new System.Drawing.Size(209, 27);
            this.Custom_VLTA3_Altitude_Status.TabIndex = 7;
            this.Custom_VLTA3_Altitude_Status.Text = "ALTITUDE : 32000 FT";
            this.Custom_VLTA3_Altitude_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Custom_VLTA4_Throttle_Status
            // 
            this.Custom_VLTA4_Throttle_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA4_Throttle_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA4_Throttle_Status.Location = new System.Drawing.Point(532, 2);
            this.Custom_VLTA4_Throttle_Status.Name = "Custom_VLTA4_Throttle_Status";
            this.Custom_VLTA4_Throttle_Status.Size = new System.Drawing.Size(193, 27);
            this.Custom_VLTA4_Throttle_Status.TabIndex = 8;
            this.Custom_VLTA4_Throttle_Status.Text = "THROTTLE: 100.00%";
            this.Custom_VLTA4_Throttle_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Custom_VLTA5_Clock_Status
            // 
            this.Custom_VLTA5_Clock_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA5_Clock_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA5_Clock_Status.Location = new System.Drawing.Point(419, 1);
            this.Custom_VLTA5_Clock_Status.Name = "Custom_VLTA5_Clock_Status";
            this.Custom_VLTA5_Clock_Status.Size = new System.Drawing.Size(107, 27);
            this.Custom_VLTA5_Clock_Status.TabIndex = 9;
            this.Custom_VLTA5_Clock_Status.Text = "12:00 PM";
            this.Custom_VLTA5_Clock_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Custom_VLTA6_Debug_Status
            // 
            this.Custom_VLTA6_Debug_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Custom_VLTA6_Debug_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA6_Debug_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA6_Debug_Status.Location = new System.Drawing.Point(0, 62);
            this.Custom_VLTA6_Debug_Status.Name = "Custom_VLTA6_Debug_Status";
            this.Custom_VLTA6_Debug_Status.Size = new System.Drawing.Size(423, 90);
            this.Custom_VLTA6_Debug_Status.TabIndex = 11;
            this.Custom_VLTA6_Debug_Status.Text = "PROGRESS : 25.50%\r\nBoeing 737-800\r\nNok Air\r\nVTBD\r\nVTCC";
            // 
            // Custom_VLTA7_Debug_Status
            // 
            this.Custom_VLTA7_Debug_Status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Custom_VLTA7_Debug_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Custom_VLTA7_Debug_Status.ForeColor = System.Drawing.Color.White;
            this.Custom_VLTA7_Debug_Status.Location = new System.Drawing.Point(437, 62);
            this.Custom_VLTA7_Debug_Status.Name = "Custom_VLTA7_Debug_Status";
            this.Custom_VLTA7_Debug_Status.Size = new System.Drawing.Size(423, 90);
            this.Custom_VLTA7_Debug_Status.TabIndex = 12;
            this.Custom_VLTA7_Debug_Status.Text = "THROTTLE";
            this.Custom_VLTA7_Debug_Status.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ImageHUDVolantaStyle
            // 
            this.ImageHUDVolantaStyle.BackColor = System.Drawing.Color.Transparent;
            this.ImageHUDVolantaStyle.BackgroundImage = global::FS4_Flight_Tracker.Properties.Resources.VolantaHUDStyle;
            this.ImageHUDVolantaStyle.Controls.Add(this.ProgressBarStatus);
            this.ImageHUDVolantaStyle.Controls.Add(this.ProgressBar);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_ARR_Text);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_DEP_Text);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_ARR_Status);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_DEP_Status);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_ALT);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_TIME);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_Name_Aircraft);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_SPD);
            this.ImageHUDVolantaStyle.Controls.Add(this.VLTA_SPD_HideNumber);
            this.ImageHUDVolantaStyle.Location = new System.Drawing.Point(12, 12);
            this.ImageHUDVolantaStyle.Name = "ImageHUDVolantaStyle";
            this.ImageHUDVolantaStyle.Size = new System.Drawing.Size(860, 446);
            this.ImageHUDVolantaStyle.TabIndex = 13;
            // 
            // ProgressBarStatus
            // 
            this.ProgressBarStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(66)))), ((int)(((byte)(254)))));
            this.ProgressBarStatus.Location = new System.Drawing.Point(1, 49);
            this.ProgressBarStatus.Name = "ProgressBarStatus";
            this.ProgressBarStatus.Size = new System.Drawing.Size(435, 7);
            this.ProgressBarStatus.TabIndex = 8;
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(26)))), ((int)(((byte)(49)))));
            this.ProgressBar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ProgressBar.Location = new System.Drawing.Point(1, 49);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(860, 7);
            this.ProgressBar.TabIndex = 9;
            // 
            // VLTA_ARR_Text
            // 
            this.VLTA_ARR_Text.BackColor = System.Drawing.Color.Transparent;
            this.VLTA_ARR_Text.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_ARR_Text.ForeColor = System.Drawing.Color.Black;
            this.VLTA_ARR_Text.Location = new System.Drawing.Point(539, 1);
            this.VLTA_ARR_Text.Name = "VLTA_ARR_Text";
            this.VLTA_ARR_Text.Size = new System.Drawing.Size(32, 41);
            this.VLTA_ARR_Text.TabIndex = 6;
            this.VLTA_ARR_Text.Text = "ARR";
            this.VLTA_ARR_Text.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VLTA_DEP_Text
            // 
            this.VLTA_DEP_Text.BackColor = System.Drawing.Color.Transparent;
            this.VLTA_DEP_Text.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_DEP_Text.ForeColor = System.Drawing.Color.Black;
            this.VLTA_DEP_Text.Location = new System.Drawing.Point(291, 0);
            this.VLTA_DEP_Text.Name = "VLTA_DEP_Text";
            this.VLTA_DEP_Text.Size = new System.Drawing.Size(32, 42);
            this.VLTA_DEP_Text.TabIndex = 7;
            this.VLTA_DEP_Text.Text = "DEP";
            this.VLTA_DEP_Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VLTA_ARR_Status
            // 
            this.VLTA_ARR_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_ARR_Status.ForeColor = System.Drawing.Color.White;
            this.VLTA_ARR_Status.Location = new System.Drawing.Point(447, 0);
            this.VLTA_ARR_Status.Name = "VLTA_ARR_Status";
            this.VLTA_ARR_Status.Size = new System.Drawing.Size(122, 42);
            this.VLTA_ARR_Status.TabIndex = 3;
            this.VLTA_ARR_Status.Text = "ARR";
            this.VLTA_ARR_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VLTA_DEP_Status
            // 
            this.VLTA_DEP_Status.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_DEP_Status.ForeColor = System.Drawing.Color.White;
            this.VLTA_DEP_Status.Location = new System.Drawing.Point(291, 0);
            this.VLTA_DEP_Status.Name = "VLTA_DEP_Status";
            this.VLTA_DEP_Status.Size = new System.Drawing.Size(122, 42);
            this.VLTA_DEP_Status.TabIndex = 2;
            this.VLTA_DEP_Status.Text = "DEP";
            this.VLTA_DEP_Status.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VLTA_ALT
            // 
            this.VLTA_ALT.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_ALT.ForeColor = System.Drawing.Color.White;
            this.VLTA_ALT.Location = new System.Drawing.Point(143, 0);
            this.VLTA_ALT.Name = "VLTA_ALT";
            this.VLTA_ALT.Size = new System.Drawing.Size(145, 42);
            this.VLTA_ALT.TabIndex = 1;
            this.VLTA_ALT.Text = "ALT: 32000ft";
            this.VLTA_ALT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VLTA_TIME
            // 
            this.VLTA_TIME.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_TIME.ForeColor = System.Drawing.Color.White;
            this.VLTA_TIME.Location = new System.Drawing.Point(731, 0);
            this.VLTA_TIME.Name = "VLTA_TIME";
            this.VLTA_TIME.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VLTA_TIME.Size = new System.Drawing.Size(123, 42);
            this.VLTA_TIME.TabIndex = 4;
            this.VLTA_TIME.Text = "UTC: 12:00 PM";
            this.VLTA_TIME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VLTA_Name_Aircraft
            // 
            this.VLTA_Name_Aircraft.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_Name_Aircraft.ForeColor = System.Drawing.Color.White;
            this.VLTA_Name_Aircraft.Location = new System.Drawing.Point(572, 1);
            this.VLTA_Name_Aircraft.Name = "VLTA_Name_Aircraft";
            this.VLTA_Name_Aircraft.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VLTA_Name_Aircraft.Size = new System.Drawing.Size(152, 41);
            this.VLTA_Name_Aircraft.TabIndex = 5;
            this.VLTA_Name_Aircraft.Text = "Aircraft Name\r\nLivery Name";
            this.VLTA_Name_Aircraft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VLTA_SPD
            // 
            this.VLTA_SPD.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_SPD.ForeColor = System.Drawing.Color.White;
            this.VLTA_SPD.Location = new System.Drawing.Point(12, 0);
            this.VLTA_SPD.Name = "VLTA_SPD";
            this.VLTA_SPD.Size = new System.Drawing.Size(125, 42);
            this.VLTA_SPD.TabIndex = 0;
            this.VLTA_SPD.Text = "SPD: 999kts ";
            this.VLTA_SPD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VLTA_SPD_HideNumber
            // 
            this.VLTA_SPD_HideNumber.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VLTA_SPD_HideNumber.ForeColor = System.Drawing.Color.White;
            this.VLTA_SPD_HideNumber.Location = new System.Drawing.Point(12, 0);
            this.VLTA_SPD_HideNumber.Name = "VLTA_SPD_HideNumber";
            this.VLTA_SPD_HideNumber.Size = new System.Drawing.Size(125, 42);
            this.VLTA_SPD_HideNumber.TabIndex = 16;
            this.VLTA_SPD_HideNumber.Text = "SPD: ---kts ";
            this.VLTA_SPD_HideNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VLTA_SPD_HideNumber.Visible = false;
            // 
            // PanelTaskbar
            // 
            this.PanelTaskbar.BackColor = System.Drawing.Color.MediumBlue;
            this.PanelTaskbar.Controls.Add(this.ButtonExitProgram);
            this.PanelTaskbar.Controls.Add(this.TaskbarName);
            this.PanelTaskbar.Location = new System.Drawing.Point(0, 0);
            this.PanelTaskbar.Name = "PanelTaskbar";
            this.PanelTaskbar.Size = new System.Drawing.Size(884, 37);
            this.PanelTaskbar.TabIndex = 19;
            this.PanelTaskbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTaskbar_MouseDown);
            // 
            // ButtonExitProgram
            // 
            this.ButtonExitProgram.BackColor = System.Drawing.Color.Black;
            this.ButtonExitProgram.BackgroundImage = global::FS4_Flight_Tracker.Properties.Resources.ExitTaskbar;
            this.ButtonExitProgram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonExitProgram.FlatAppearance.BorderSize = 0;
            this.ButtonExitProgram.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonExitProgram.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.ButtonExitProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonExitProgram.Location = new System.Drawing.Point(850, 3);
            this.ButtonExitProgram.Name = "ButtonExitProgram";
            this.ButtonExitProgram.Size = new System.Drawing.Size(30, 30);
            this.ButtonExitProgram.TabIndex = 1;
            this.ButtonExitProgram.UseVisualStyleBackColor = false;
            this.ButtonExitProgram.Click += new System.EventHandler(this.ButtonExitProgram_Click);
            // 
            // TaskbarName
            // 
            this.TaskbarName.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TaskbarName.ForeColor = System.Drawing.Color.White;
            this.TaskbarName.Location = new System.Drawing.Point(3, 6);
            this.TaskbarName.Name = "TaskbarName";
            this.TaskbarName.Size = new System.Drawing.Size(343, 27);
            this.TaskbarName.TabIndex = 0;
            this.TaskbarName.Text = "FS 4 Flight Tracker";
            this.TaskbarName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TaskbarName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label2_MouseDown);
            // 
            // notificationopengame
            // 
            this.notificationopengame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.notificationopengame.Controls.Add(this.VersionText);
            this.notificationopengame.Controls.Add(this.ButtonChangelog);
            this.notificationopengame.Controls.Add(this.ViewChangelogList);
            this.notificationopengame.Controls.Add(this.pleaserestart);
            this.notificationopengame.Controls.Add(this.Restart);
            this.notificationopengame.Controls.Add(this.pleaseopenaerofly);
            this.notificationopengame.Location = new System.Drawing.Point(0, -1);
            this.notificationopengame.Name = "notificationopengame";
            this.notificationopengame.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.notificationopengame.Size = new System.Drawing.Size(886, 499);
            this.notificationopengame.TabIndex = 18;
            this.notificationopengame.Visible = false;
            // 
            // ButtonChangelog
            // 
            this.ButtonChangelog.BackColor = System.Drawing.Color.Transparent;
            this.ButtonChangelog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonChangelog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.ButtonChangelog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonChangelog.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonChangelog.ForeColor = System.Drawing.Color.White;
            this.ButtonChangelog.Location = new System.Drawing.Point(5, 367);
            this.ButtonChangelog.Name = "ButtonChangelog";
            this.ButtonChangelog.Size = new System.Drawing.Size(158, 61);
            this.ButtonChangelog.TabIndex = 13;
            this.ButtonChangelog.Text = "View\r\nChangelog";
            this.ButtonChangelog.UseVisualStyleBackColor = false;
            this.ButtonChangelog.Click += new System.EventHandler(this.ButtonChangelog_Click);
            // 
            // ViewChangelogList
            // 
            this.ViewChangelogList.BackColor = System.Drawing.Color.Black;
            this.ViewChangelogList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ViewChangelogList.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ViewChangelogList.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F);
            this.ViewChangelogList.ForeColor = System.Drawing.Color.White;
            this.ViewChangelogList.Location = new System.Drawing.Point(169, 142);
            this.ViewChangelogList.Name = "ViewChangelogList";
            this.ViewChangelogList.ReadOnly = true;
            this.ViewChangelogList.ShortcutsEnabled = false;
            this.ViewChangelogList.Size = new System.Drawing.Size(552, 286);
            this.ViewChangelogList.TabIndex = 12;
            this.ViewChangelogList.Text = resources.GetString("ViewChangelogList.Text");
            this.ViewChangelogList.Visible = false;
            // 
            // VersionText
            // 
            this.VersionText.BackColor = System.Drawing.Color.Transparent;
            this.VersionText.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionText.ForeColor = System.Drawing.Color.White;
            this.VersionText.Location = new System.Drawing.Point(7, 437);
            this.VersionText.Name = "VersionText";
            this.VersionText.Size = new System.Drawing.Size(209, 57);
            this.VersionText.TabIndex = 11;
            this.VersionText.Text = "Version X.XX\r\nMain Game X.X.X.X";
            this.VersionText.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pleaserestart
            // 
            this.pleaserestart.BackColor = System.Drawing.Color.Transparent;
            this.pleaserestart.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F);
            this.pleaserestart.ForeColor = System.Drawing.Color.Lime;
            this.pleaserestart.Location = new System.Drawing.Point(341, 447);
            this.pleaserestart.Name = "pleaserestart";
            this.pleaserestart.Size = new System.Drawing.Size(340, 52);
            this.pleaserestart.TabIndex = 10;
            this.pleaserestart.Text = "Please restart FS 4 Flight Tracker.";
            this.pleaserestart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pleaserestart.Visible = false;
            // 
            // pleaseopenaerofly
            // 
            this.pleaseopenaerofly.BackColor = System.Drawing.Color.Transparent;
            this.pleaseopenaerofly.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 30F);
            this.pleaseopenaerofly.ForeColor = System.Drawing.Color.Lime;
            this.pleaseopenaerofly.Location = new System.Drawing.Point(23, 75);
            this.pleaseopenaerofly.Name = "pleaseopenaerofly";
            this.pleaseopenaerofly.Size = new System.Drawing.Size(849, 59);
            this.pleaseopenaerofly.TabIndex = 0;
            this.pleaseopenaerofly.Text = "Please open Aerofly FS 4 first.";
            this.pleaseopenaerofly.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textbox_liveryname
            // 
            this.textbox_liveryname.BackColor = System.Drawing.Color.White;
            this.textbox_liveryname.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_liveryname.ForeColor = System.Drawing.Color.Black;
            this.textbox_liveryname.Location = new System.Drawing.Point(306, 366);
            this.textbox_liveryname.Name = "textbox_liveryname";
            this.textbox_liveryname.Size = new System.Drawing.Size(262, 30);
            this.textbox_liveryname.TabIndex = 18;
            this.textbox_liveryname.TextChanged += new System.EventHandler(this.textbox_liveryname_TextChanged);
            // 
            // label_liveryname
            // 
            this.label_liveryname.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_liveryname.ForeColor = System.Drawing.Color.White;
            this.label_liveryname.Location = new System.Drawing.Point(306, 324);
            this.label_liveryname.Name = "label_liveryname";
            this.label_liveryname.Size = new System.Drawing.Size(262, 39);
            this.label_liveryname.TabIndex = 19;
            this.label_liveryname.Text = "Livery Name";
            this.label_liveryname.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // comboBox_selectaircraft
            // 
            this.comboBox_selectaircraft.AllowDrop = true;
            this.comboBox_selectaircraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_selectaircraft.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 15F, System.Drawing.FontStyle.Bold);
            this.comboBox_selectaircraft.FormattingEnabled = true;
            this.comboBox_selectaircraft.IntegralHeight = false;
            this.comboBox_selectaircraft.ItemHeight = 23;
            this.comboBox_selectaircraft.Items.AddRange(new object[] {
            "Airbus A319",
            "Airbus A320",
            "Airbus A320neo",
            "Airbus A321",
            "Airbus A321XLR",
            "Airbus A350-1000",
            "Airbus A380",
            "Antares 21E",
            "ASG 29",
            "ASK 21",
            "Baron 58",
            "Bf 109E",
            "Boeing 737-500",
            "Boeing 737-800",
            "Boeing 737-900ER",
            "Boeing 737 MAX9",
            "Boeing 747-400",
            "Boeing 777-300ER",
            "Boeing 777F",
            "Boeing 787-10",
            "Boeing 787-9",
            "Camel",
            "Cessna 172",
            "Concorde",
            "Corsair",
            "CRJ-900LR",
            "Dr.I",
            "DR400",
            "EC135",
            "Extra 330",
            "F-15E",
            "F/A-18C",
            "Ju 52",
            "Jungmeister",
            "King Air",
            "Learjet 45",
            "MB-339",
            "Me 262",
            "P-38",
            "Pitts",
            "Q400",
            "R22",
            "Swift",
            "UH-60M"});
            this.comboBox_selectaircraft.Location = new System.Drawing.Point(306, 290);
            this.comboBox_selectaircraft.Name = "comboBox_selectaircraft";
            this.comboBox_selectaircraft.Size = new System.Drawing.Size(262, 31);
            this.comboBox_selectaircraft.Sorted = true;
            this.comboBox_selectaircraft.TabIndex = 22;
            // 
            // label_aircraftname
            // 
            this.label_aircraftname.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aircraftname.ForeColor = System.Drawing.Color.White;
            this.label_aircraftname.Location = new System.Drawing.Point(306, 239);
            this.label_aircraftname.Name = "label_aircraftname";
            this.label_aircraftname.Size = new System.Drawing.Size(262, 39);
            this.label_aircraftname.TabIndex = 23;
            this.label_aircraftname.Text = "Aircraft Name";
            this.label_aircraftname.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label_blocklanguage
            // 
            this.label_blocklanguage.AutoSize = true;
            this.label_blocklanguage.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 14F, System.Drawing.FontStyle.Bold);
            this.label_blocklanguage.ForeColor = System.Drawing.Color.White;
            this.label_blocklanguage.Location = new System.Drawing.Point(241, 442);
            this.label_blocklanguage.Name = "label_blocklanguage";
            this.label_blocklanguage.Size = new System.Drawing.Size(379, 42);
            this.label_blocklanguage.TabIndex = 24;
            this.label_blocklanguage.Text = "You specified something that was blocked\r\nby the language filter. Please try agai" +
    "n.";
            this.label_blocklanguage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label_blocklanguage.Visible = false;
            // 
            // ProgressSlider
            // 
            this.ProgressSlider.BackColor = System.Drawing.Color.Transparent;
            this.ProgressSlider.Location = new System.Drawing.Point(0, 30);
            this.ProgressSlider.Maximum = 1000;
            this.ProgressSlider.Name = "ProgressSlider";
            this.ProgressSlider.Size = new System.Drawing.Size(860, 29);
            this.ProgressSlider.TabIndex = 5;
            this.ProgressSlider.Text = "imageSlider1";
            this.ProgressSlider.ThumbImage = global::FS4_Flight_Tracker.Properties.Resources.CustomHUD1_AirplaneLogo;
            this.ProgressSlider.ThumbSize = new System.Drawing.Size(24, 24);
            this.ProgressSlider.TrackColor = System.Drawing.Color.White;
            this.ProgressSlider.TrackHeight = 5;
            this.ProgressSlider.TrackProgressColor = System.Drawing.Color.Red;
            this.ProgressSlider.Value = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(884, 497);
            this.Controls.Add(this.PanelTaskbar);
            this.Controls.Add(this.statusaeroflyfs4);
            this.Controls.Add(this.notificationopengame);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelVolantaStyle);
            this.Controls.Add(this.panelVolantaEnabled);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "FS4 Flight Tracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelVolantaEnabled.ResumeLayout(false);
            this.panelVolantaStyle.ResumeLayout(false);
            this.ImageCustomHUD1.ResumeLayout(false);
            this.ImageHUDVolantaStyle.ResumeLayout(false);
            this.PanelTaskbar.ResumeLayout(false);
            this.notificationopengame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label statusaeroflyfs4;
        private System.Windows.Forms.Timer timerstatusaeroflyfs4;
        private System.Windows.Forms.Label SpeedStatus;
        private System.Windows.Forms.Label AltitideStatus;
        private System.Windows.Forms.Label PitchStatus;
        private System.Windows.Forms.Label RollStatus;
        private System.Windows.Forms.Label AircraftandLivery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button StartVolantaStyle;
        private System.Windows.Forms.Label ArrivalText;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Timer timerreadingmemory;
        private System.Windows.Forms.Label DepartureText;
        private System.Windows.Forms.Panel panelVolantaEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelVolantaStyle;
        private System.Windows.Forms.Panel ImageHUDVolantaStyle;
        private System.Windows.Forms.Panel ProgressBarStatus;
        private System.Windows.Forms.Panel ProgressBar;
        private System.Windows.Forms.Label VLTA_ARR_Text;
        private System.Windows.Forms.Label VLTA_DEP_Text;
        private System.Windows.Forms.Label VLTA_ARR_Status;
        private System.Windows.Forms.Label VLTA_DEP_Status;
        private System.Windows.Forms.Label VLTA_ALT;
        private System.Windows.Forms.Label VLTA_TIME;
        private System.Windows.Forms.Label VLTA_Name_Aircraft;
        private System.Windows.Forms.Label VLTA_SPD;
        private System.Windows.Forms.Button Quit_Volanta;
        private System.Windows.Forms.Label PlayerPosition;
        private System.Windows.Forms.Label DeparturePosition;
        private System.Windows.Forms.Label ArrivalPosition;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblXYZ;
        private System.Windows.Forms.Button Switch_Color_Background;
        private System.Windows.Forms.Label VLTA_SPD_HideNumber;
        private System.Windows.Forms.Panel ImageCustomHUD1;
        private CustomSliderApp.ImageSlider ProgressSlider;
        private System.Windows.Forms.Label Custom_VLTA1_DEP_and_ARR_Status;
        private System.Windows.Forms.Label Custom_VLTA2_Speed_Status;
        private System.Windows.Forms.Label Custom_VLTA3_Altitude_Status;
        private System.Windows.Forms.Label Custom_VLTA4_Throttle_Status;
        private System.Windows.Forms.Label Custom_VLTA5_Clock_Status;
        private System.Windows.Forms.Button Next_HUD;
        private System.Windows.Forms.Label Custom_VLTA2_Speed_HideNumber_Status;
        private System.Windows.Forms.Panel PanelTaskbar;
        private System.Windows.Forms.Label TaskbarName;
        private System.Windows.Forms.Button ButtonExitProgram;
        private System.Windows.Forms.Label Custom_VLTA6_Debug_Status;
        private System.Windows.Forms.Panel notificationopengame;
        private System.Windows.Forms.Label pleaseopenaerofly;
        private System.Windows.Forms.Label pleaserestart;
        private System.Windows.Forms.Label VersionText;
        private System.Windows.Forms.RichTextBox ViewChangelogList;
        private System.Windows.Forms.Button ButtonChangelog;
        private System.Windows.Forms.Label Custom_VLTA7_Debug_Status;
        private System.Windows.Forms.Panel ProgressBarThrottleWhite;
        private System.Windows.Forms.Panel ProgressBarThrottleRed;
        private System.Windows.Forms.TextBox textbox_liveryname;
        private System.Windows.Forms.Label label_liveryname;
        private System.Windows.Forms.ComboBox comboBox_selectaircraft;
        private System.Windows.Forms.Label label_aircraftname;
        private System.Windows.Forms.Label label_blocklanguage;
    }
}

