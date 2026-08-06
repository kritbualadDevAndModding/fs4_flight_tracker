
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
            this.SuspendLayout();
            // 
            // statusaeroflyfs4
            // 
            this.statusaeroflyfs4.BackColor = System.Drawing.Color.Black;
            this.statusaeroflyfs4.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusaeroflyfs4.ForeColor = System.Drawing.Color.Red;
            this.statusaeroflyfs4.Location = new System.Drawing.Point(12, 9);
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
            this.SpeedStatus.Location = new System.Drawing.Point(12, 67);
            this.SpeedStatus.Name = "SpeedStatus";
            this.SpeedStatus.Size = new System.Drawing.Size(126, 97);
            this.SpeedStatus.TabIndex = 1;
            this.SpeedStatus.Text = "Speed";
            this.SpeedStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AltitideStatus
            // 
            this.AltitideStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AltitideStatus.Location = new System.Drawing.Point(184, 67);
            this.AltitideStatus.Name = "AltitideStatus";
            this.AltitideStatus.Size = new System.Drawing.Size(126, 97);
            this.AltitideStatus.TabIndex = 2;
            this.AltitideStatus.Text = "Altitude";
            this.AltitideStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PitchStatus
            // 
            this.PitchStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PitchStatus.Location = new System.Drawing.Point(339, 67);
            this.PitchStatus.Name = "PitchStatus";
            this.PitchStatus.Size = new System.Drawing.Size(126, 97);
            this.PitchStatus.TabIndex = 3;
            this.PitchStatus.Text = "Pitch";
            this.PitchStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RollStatus
            // 
            this.RollStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RollStatus.Location = new System.Drawing.Point(513, 67);
            this.RollStatus.Name = "RollStatus";
            this.RollStatus.Size = new System.Drawing.Size(126, 97);
            this.RollStatus.TabIndex = 4;
            this.RollStatus.Text = "Roll";
            this.RollStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PositionStatus
            // 
            this.PositionStatus.Font = new System.Drawing.Font("NeueHaasGroteskDisp Pro", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionStatus.Location = new System.Drawing.Point(662, 67);
            this.PositionStatus.Name = "PositionStatus";
            this.PositionStatus.Size = new System.Drawing.Size(126, 97);
            this.PositionStatus.TabIndex = 5;
            this.PositionStatus.Text = "Position";
            this.PositionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PositionStatus);
            this.Controls.Add(this.RollStatus);
            this.Controls.Add(this.PitchStatus);
            this.Controls.Add(this.AltitideStatus);
            this.Controls.Add(this.SpeedStatus);
            this.Controls.Add(this.statusaeroflyfs4);
            this.Name = "Form1";
            this.Text = "FS4 Flight Tracker";
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
    }
}

