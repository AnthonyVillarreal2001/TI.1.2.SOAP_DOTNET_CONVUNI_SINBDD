using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace _02.CLIESC.Views
{
    public class LoginForm : Form
    {
        public TextBox TxtUser { get; private set; }
        public TextBox TxtPass { get; private set; }
        public Button BtnLogin { get; private set; }

        public LoginForm()
        {
            Text = "Login - Conversor Multitarea";
            ClientSize = new Size(420, 320);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            CargarFondo();
            ConstruirIU();
        }

        private void CargarFondo()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "sullivan.jpg");
            if (File.Exists(path))
            {
                BackgroundImage = Image.FromFile(path);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void ConstruirIU()
        {
            // Tarjeta central blanca semitransparente (95% opacidad)
            Panel card = new Panel
            {
                Size = new Size(320, 220),
                Location = new Point(50, 50),
                BackColor = Color.FromArgb(242, 255, 255, 255),  // 95% = 242/255
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 320, 220, 20, 20))
            };
            Controls.Add(card);

            // Icono puerta (🚪)
            Label icono = new Label
            {
                Text = "🚪",
                Font = new Font("Segoe UI", 24, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(140, 15),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(139, 92, 246) // púrpura claro
            };
            card.Controls.Add(icono);

            // Título "¡Bienvenido!"
            Label titulo = new Label
            {
                Text = "¡Bienvenido!",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(91, 44, 142), // púrpura oscuro
                AutoSize = true,
                Location = new Point(90, 50),
                BackColor = Color.Transparent
            };
            card.Controls.Add(titulo);

            // Caja de texto Usuario (con borde púrpura claro)
            Panel borderUser = CrearBordeTextBox(card, new Point(60, 95));
            TxtUser = CrearTextBoxDentro(borderUser, "Usuario");
            card.Controls.Add(borderUser);

            // Caja de texto Contraseña
            Panel borderPass = CrearBordeTextBox(card, new Point(60, 130));
            TxtPass = CrearTextBoxDentro(borderPass, "Contraseña");
            TxtPass.UseSystemPasswordChar = true;
            card.Controls.Add(borderPass);

            // Botón INGRESAR con degradado
            BtnLogin = new GradientButton
            {
                Text = "INGRESAR",
                Location = new Point(95, 170),
                Size = new Size(130, 40),
                Color1 = Color.FromArgb(139, 92, 246),   // púrpura claro
                Color2 = Color.FromArgb(20, 184, 166)    // verde azulado
            };
            card.Controls.Add(BtnLogin);

            // Crédito
            Label credito = new Label
            {
                Text = "👾 Hecho por: Ariel R. y Anthony V.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Location = new Point(100, 280),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(credito);

            AcceptButton = BtnLogin;
        }

        private Panel CrearBordeTextBox(Control parent, Point location)
        {
            Panel border = new Panel
            {
                Size = new Size(200, 28),
                Location = location,
                BackColor = Color.FromArgb(139, 92, 246), // púrpura claro
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 200, 28, 10, 10))
            };
            return border;
        }

        private TextBox CrearTextBoxDentro(Panel border, string placeholder)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(2, 2),
                Size = new Size(196, 24),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(30, 41, 59), // gris oscuro
                BackColor = Color.White
            };
            txt.GotFocus += (s, e) => { if (txt.Text == placeholder) txt.Text = ""; };
            txt.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) txt.Text = placeholder; };
            txt.Text = placeholder;
            border.Controls.Add(txt);
            return txt;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}