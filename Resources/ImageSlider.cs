using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomSliderApp
{
    public class ImageSlider : Control
    {
        // Properties
        public int minimum = 0;
        public int maximum = 100;
        private int value = 50;
        private Image thumbImage = null;
        private Size thumbSize = new Size(24, 24);
        private Color trackColor = Color.LightGray;
        private Color trackProgressColor = Color.DodgerBlue;
        private int trackHeight = 6;

        private bool isDragging = false;

        // Events
        public event EventHandler ValueChanged;

        public ImageSlider()
        {
            // เปิดใช้งาน Double Buffer เพื่อลดอาการกะพริบ (Flicker)
            this.SetStyle(ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint |
                           ControlStyles.OptimizedDoubleBuffer |
                           ControlStyles.ResizeRedraw |
                           ControlStyles.SupportsTransparentBackColor, true);

            this.Size = new Size(200, 40);
            this.BackColor = Color.Transparent;
        }

        #region Public Properties

        [Category("Slider Properties"), DefaultValue(0)]
        public int Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                if (minimum > maximum) maximum = minimum;
                if (this.value < minimum) Value = minimum;
                Invalidate();
            }
        }

        [Category("Slider Properties"), DefaultValue(100)]
        public int Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                if (maximum < minimum) minimum = maximum;
                if (this.value > maximum) Value = maximum;
                Invalidate();
            }
        }

        [Category("Slider Properties"), DefaultValue(50)]
        public int Value
        {
            get => value;
            set
            {
                int newValue = Math.Max(minimum, Math.Min(maximum, value));
                if (this.value != newValue)
                {
                    this.value = newValue;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        [Category("Slider Properties")]
        public Image ThumbImage
        {
            get => thumbImage;
            set
            {
                thumbImage = value;
                Invalidate();
            }
        }

        [Category("Slider Properties")]
        public Size ThumbSize
        {
            get => thumbSize;
            set
            {
                thumbSize = value;
                Invalidate();
            }
        }

        [Category("Slider Properties")]
        public Color TrackColor
        {
            get => trackColor;
            set { trackColor = value; Invalidate(); }
        }

        [Category("Slider Properties")]
        public Color TrackProgressColor
        {
            get => trackProgressColor;
            set { trackProgressColor = value; Invalidate(); }
        }

        [Category("Slider Properties"), DefaultValue(6)]
        public int TrackHeight
        {
            get => trackHeight;
            set { trackHeight = value; Invalidate(); }
        }

        #endregion

        #region Drawing & Mouse Events

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int padding = Math.Max(thumbSize.Width, thumbSize.Height) / 2;
            int trackLength = Width - (padding * 2);
            int trackY = Height / 2 - (trackHeight / 2);

            // คำนวณตำแหน่ง X ของ Thumb
            float percent = (maximum == minimum) ? 0 : (float)(value - minimum) / (maximum - minimum);
            int thumbX = padding + (int)(percent * trackLength);
            int thumbY = Height / 2;

            // 1. วาดเส้นแทร็กพื้นหลัง (Track Background)
            using (GraphicsPath path = GetRoundedRectPath(new RectangleF(padding, trackY, trackLength, trackHeight), trackHeight))
            using (SolidBrush brush = new SolidBrush(trackColor))
            {
                g.FillPath(brush, path);
            }

            // 2. วาดเส้นแทร็กส่วนที่เลื่อนแล้ว (Progress Track)
            int progressWidth = thumbX - padding;
            if (progressWidth > 0)
            {
                using (GraphicsPath path = GetRoundedRectPath(new RectangleF(padding, trackY, progressWidth, trackHeight), trackHeight))
                using (SolidBrush brush = new SolidBrush(trackProgressColor))
                {
                    g.FillPath(brush, path);
                }
            }

            // 3. วาดหัวสไลเดอร์ (Thumb Image หรือ วงกลมกรณีไม่มีรูป)
            Rectangle thumbRect = new Rectangle(
                thumbX - (thumbSize.Width / 2),
                thumbY - (thumbSize.Height / 2),
                thumbSize.Width,
                thumbSize.Height
            );

            if (thumbImage != null)
            {
                g.DrawImage(thumbImage, thumbRect);
            }
            else
            {
                // ถ้ายังไม่ได้ใส่รูป ให้วาดวงกลมเริ่มต้นไว้ก่อน
                using (SolidBrush thumbBrush = new SolidBrush(trackProgressColor))
                {
                    g.FillEllipse(thumbBrush, thumbRect);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                UpdateValueFromMouse(e.X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                UpdateValueFromMouse(e.X);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            int padding = Math.Max(thumbSize.Width, thumbSize.Height) / 2;
            int trackLength = Width - (padding * 2);

            int clampedX = Math.Max(padding, Math.Min(Width - padding, mouseX));
            float percent = (float)(clampedX - padding) / trackLength;

            Value = minimum + (int)Math.Round(percent * (maximum - minimum));
        }

        private GraphicsPath GetRoundedRectPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
    }
}