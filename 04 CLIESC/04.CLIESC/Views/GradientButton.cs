using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _02.CLIESC.Views
{
    public class GradientButton : Button
    {
        public Color Color1 { get; set; } = Color.FromArgb(139, 92, 246);
        public Color Color2 { get; set; } = Color.FromArgb(20, 184, 166);

        public GradientButton()
        {
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10, FontStyle.Bold);
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            UseVisualStyleBackColor = false;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color1, Color2, LinearGradientMode.Horizontal))
            {
                GraphicsPath path = RoundedRect(rect, 12);
                pevent.Graphics.FillPath(brush, path);
            }
            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}