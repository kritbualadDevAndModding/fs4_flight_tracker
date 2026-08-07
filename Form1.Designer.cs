
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
            this.statusaeroflyfs4 = new System.Windows.Forms.Label();
            this.timerstatusaeroflyfs4 = new System.Windows.Forms.Timer(this.components);
            this.SpeedStatus = new System.Windows.Forms.Label();
            this.AltitideStatus = new System.Windows.Forms.Label();
            this.PitchStatus = new System.Windows.Forms.Label();
            this.RollStatus = new System.Windows.Forms.Label();
            this.PositionStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RouteText = new System.Windows.Forms.Label();
            this.DestinationCoordText = new System.Windows.Forms.Label();
            this.FlightPlanLoad = new System.Windows.Forms.Button();
            this.LoadNameAircraft = new System.Windows.Forms.Button();
            this.AircraftName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusaeroflyfs4
            // 
            this.statusaeroflyfs4.BackColor = System.Drawing.Color.Black;
            this.statusaeroflyfs4.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusaeroflyfs4.ForeColor = System.Drawing.Color.Red;
            this.statusaeroflyfs4.Location = new System.Drawing.Point(0, 0);
            this.statusaeroflyfs4.Name = "statusaeroflyfs4";
            this.statusaeroflyfs4.Size = new System.Drawing.Size(776, 33);
            this.statusaeroflyfs4.TabIndex = 0;
            this.statusaeroflyfs4.Text = "❌ Aerofly FS 4 Disconnected";
            this.statusaeroflyfs4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerstatusaeroflyfs4
            // 
            this.timerstatusaeroflyfs4.Enabled = true;
            this.timerstatusaeroflyfs4.Tick += new System.EventHandler(this.timerstatusaeroflyfs4_Tick);
            // 
            // SpeedStatus
            // 
            this.SpeedStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedStatus.Location = new System.Drawing.Point(0, 67);
            this.SpeedStatus.Name = "SpeedStatus";
            this.SpeedStatus.Size = new System.Drawing.Size(126, 97);
            this.SpeedStatus.TabIndex = 1;
            this.SpeedStatus.Text = "Speed";
            this.SpeedStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AltitideStatus
            // 
            this.AltitideStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AltitideStatus.Location = new System.Drawing.Point(178, 67);
            this.AltitideStatus.Name = "AltitideStatus";
            this.AltitideStatus.Size = new System.Drawing.Size(126, 97);
            this.AltitideStatus.TabIndex = 2;
            this.AltitideStatus.Text = "Altitude";
            this.AltitideStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PitchStatus
            // 
            this.PitchStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PitchStatus.Location = new System.Drawing.Point(335, 67);
            this.PitchStatus.Name = "PitchStatus";
            this.PitchStatus.Size = new System.Drawing.Size(126, 97);
            this.PitchStatus.TabIndex = 3;
            this.PitchStatus.Text = "Pitch";
            this.PitchStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RollStatus
            // 
            this.RollStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollStatus.Location = new System.Drawing.Point(494, 67);
            this.RollStatus.Name = "RollStatus";
            this.RollStatus.Size = new System.Drawing.Size(126, 97);
            this.RollStatus.TabIndex = 4;
            this.RollStatus.Text = "Roll";
            this.RollStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PositionStatus
            // 
            this.PositionStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionStatus.Location = new System.Drawing.Point(650, 67);
            this.PositionStatus.Name = "PositionStatus";
            this.PositionStatus.Size = new System.Drawing.Size(126, 97);
            this.PositionStatus.TabIndex = 5;
            this.PositionStatus.Text = "Position";
            this.PositionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AircraftName);
            this.panel1.Controls.Add(this.LoadNameAircraft);
            this.panel1.Controls.Add(this.FlightPlanLoad);
            this.panel1.Controls.Add(this.DestinationCoordText);
            this.panel1.Controls.Add(this.RouteText);
            this.panel1.Controls.Add(this.statusaeroflyfs4);
            this.panel1.Controls.Add(this.SpeedStatus);
            this.panel1.Controls.Add(this.AltitideStatus);
            this.panel1.Controls.Add(this.PitchStatus);
            this.panel1.Controls.Add(this.RollStatus);
            this.panel1.Controls.Add(this.PositionStatus);
            this.panel1.Location = new System.Drawing.Point(12, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 429);
            this.panel1.TabIndex = 6;
            // 
            // RouteText
            // 
            this.RouteText.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RouteText.Location = new System.Drawing.Point(0, 164);
            this.RouteText.Name = "RouteText";
            this.RouteText.Size = new System.Drawing.Size(304, 70);
            this.RouteText.TabIndex = 6;
            this.RouteText.Text = "ROUTE";
            this.RouteText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DestinationCoordText
            // 
            this.DestinationCoordText.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DestinationCoordText.Location = new System.Drawing.Point(0, 234);
            this.DestinationCoordText.Name = "DestinationCoordText";
            this.DestinationCoordText.Size = new System.Drawing.Size(304, 70);
            this.DestinationCoordText.TabIndex = 7;
            this.DestinationCoordText.Text = "DestinationCoord";
            this.DestinationCoordText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FlightPlanLoad
            // 
            this.FlightPlanLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.FlightPlanLoad.Location = new System.Drawing.Point(313, 383);
            this.FlightPlanLoad.Name = "FlightPlanLoad";
            this.FlightPlanLoad.Size = new System.Drawing.Size(148, 46);
            this.FlightPlanLoad.TabIndex = 8;
            this.FlightPlanLoad.Text = "FlightPlanLoad";
            this.FlightPlanLoad.UseVisualStyleBackColor = true;
            this.FlightPlanLoad.Click += new System.EventHandler(this.button1_Click);
            // 
            // LoadNameAircraft
            // 
            this.LoadNameAircraft.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.LoadNameAircraft.Location = new System.Drawing.Point(578, 383);
            this.LoadNameAircraft.Name = "LoadNameAircraft";
            this.LoadNameAircraft.Size = new System.Drawing.Size(198, 46);
            this.LoadNameAircraft.TabIndex = 9;
            this.LoadNameAircraft.Text = "Load Name Aircraft";
            this.LoadNameAircraft.UseVisualStyleBackColor = true;
            this.LoadNameAircraft.Click += new System.EventHandler(this.LoadNameAircraft_Click);
            // 
            // AircraftName
            // 
            this.AircraftName.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AircraftName.Location = new System.Drawing.Point(313, 310);
            this.AircraftName.Name = "AircraftName";
            this.AircraftName.Size = new System.Drawing.Size(463, 70);
            this.AircraftName.TabIndex = 10;
            this.AircraftName.Text = "Aircraft Name:";
            this.AircraftName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FS4 Flight Tracker";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label statusaeroflyfs4;
        private System.Windows.Forms.Timer timerstatusaeroflyfs4;
        private System.Windows.Forms.Label SpeedStatus;
        private System.Windows.Forms.Label AltitideStatus;
        private System.Windows.Forms.Label PitchStatus;
        private System.Windows.Forms.Label RollStatus;
        private System.Windows.Forms.Label PositionStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label RouteText;
        private System.Windows.Forms.Label DestinationCoordText;
        private System.Windows.Forms.Button FlightPlanLoad;
        private System.Windows.Forms.Label AircraftName;
        private System.Windows.Forms.Button LoadNameAircraft;
    }
}

