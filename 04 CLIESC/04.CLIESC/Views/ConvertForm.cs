using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _02.CLIESC.Views
{
    public class ConvertForm : Form
    {
        public ComboBox CmbType { get; private set; }
        public TextBox TxtValue { get; private set; }
        public Button BtnConvert { get; private set; }
        public Button BtnLogout { get; private set; }
        public Label LblResult { get; private set; }

        private ComboBox _cmbCategory;
        private Dictionary<string, List<string>> _categoryMap;

        public ConvertForm(string user)
        {
            Text = "Panel de Conversiones";
            ClientSize = new Size(700, 480);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            DoubleBuffered = true;

            CargarFondo();
            ConstruirIU();
            _userName = user;
        }

        private string _userName;
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
            // Header
            Panel header = new Panel
            {
                Size = new Size(660, 45),
                Location = new Point(20, 10),
                BackColor = Color.FromArgb(230, 255, 255, 255),
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 660, 45, 15, 15))
            };
            Controls.Add(header);

            header.Controls.Add(new Label
            {
                Text = "Conversión de Unidades",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(91, 44, 142),
                Location = new Point(20, 10),
                AutoSize = true,
                BackColor = Color.Transparent
            });

            BtnLogout = new GradientButton
            {
                Text = "Cerrar sesión",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(530, 9),
                Size = new Size(110, 28),
                Color1 = Color.FromArgb(59, 130, 246),
                Color2 = Color.FromArgb(37, 99, 235)
            };
            header.Controls.Add(BtnLogout);

            // Bloque 1 – Categoría
            Panel catCard = CrearTarjetaConTexto(new Rectangle(20, 70, 660, 75), Color.White,
                Color.FromArgb(59, 130, 246), "📋 Categoría de Conversión", 20);
            Controls.Add(catCard);

            _cmbCategory = new ComboBox
            {
                Location = new Point(15, 42),
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            _cmbCategory.Items.AddRange(new[] { "📏 Longitud", "🌡️ Temperatura", "⚖️ Peso" });
            _cmbCategory.SelectedIndex = 0;
            _cmbCategory.SelectedIndexChanged += (s, e) => ActualizarSubTipos();
            catCard.Controls.Add(_cmbCategory);

            // Bloque 2 – Tipo
            Panel typeCard = CrearTarjetaConTexto(new Rectangle(20, 160, 660, 75), Color.White,
                Color.FromArgb(249, 115, 22), "🔄 Tipo de Conversión", 20);
            Controls.Add(typeCard);

            CmbType = new ComboBox
            {
                Location = new Point(15, 42),
                Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            typeCard.Controls.Add(CmbType);

            // Bloque 3 – Valor
            Panel valueCard = CrearTarjetaConTexto(new Rectangle(20, 250, 660, 150), Color.White,
                Color.FromArgb(16, 185, 129), "✏️ Ingrese el Valor", 20);
            Controls.Add(valueCard);

            TxtValue = new TextBox
            {
                Location = new Point(15, 42),
                Width = 200,
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };
            TxtValue.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
            };
            valueCard.Controls.Add(TxtValue);

            BtnConvert = new GradientButton
            {
                Text = "CONVERTIR",
                Location = new Point(230, 42),
                Size = new Size(130, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Color1 = Color.FromArgb(20, 184, 166),
                Color2 = Color.FromArgb(13, 148, 136)
            };
            valueCard.Controls.Add(BtnConvert);

            LblResult = new Label
            {
                Text = "Resultado: ---",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 118, 110),
                Location = new Point(15, 95),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            valueCard.Controls.Add(LblResult);

            // FAB
            Button btnClear = new Button
            {
                Text = "🗑️",
                Font = new Font("Segoe UI", 16),
                Size = new Size(50, 50),
                Location = new Point(ClientSize.Width - 70, ClientSize.Height - 70),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(249, 115, 22),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, 50, 50, 25, 25));
            btnClear.Click += (s, e) => { TxtValue.Clear(); LblResult.Text = "Resultado: ---"; };
            Controls.Add(btnClear);
            btnClear.BringToFront();

            Label credito = new Label
            {
                Text = "👾 Hecho por: Ariel R. y Anthony V.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Location = new Point(240, ClientSize.Height - 25),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            Controls.Add(credito);

            _categoryMap = new Dictionary<string, List<string>>
            {
                { "📏 Longitud", new List<string> { "Metros → Pies", "Kilómetros → Millas", "Centímetros → Pulgadas", "Pulgadas → Centímetros", "Pies → Metros" } },
                { "🌡️ Temperatura", new List<string> { "Celsius → Fahrenheit", "Celsius → Kelvin", "Celsius → Rankine", "Celsius → Réaumur", "Fahrenheit → Celsius" } },
                { "⚖️ Peso", new List<string> { "Kilogramos → Libras", "Gramos → Onzas", "Toneladas → Kilogramos", "Libras → Kilogramos", "Onzas → Gramos" } }
            };
            ActualizarSubTipos();
        }

        private void ActualizarSubTipos()
        {
            if (_cmbCategory.SelectedItem is string cat && _categoryMap.ContainsKey(cat))
            {
                CmbType.DataSource = _categoryMap[cat].ToList();
            }
        }

        public int ObtenerIdConversion()
        {
            if (_cmbCategory.SelectedItem is string cat && CmbType.SelectedItem is string sub)
            {
                int baseIdx = 0;
                switch (cat)
                {
                    case "📏 Longitud": baseIdx = 0; break;
                    case "⚖️ Peso": baseIdx = 5; break;
                    case "🌡️ Temperatura": baseIdx = 10; break;
                    default: baseIdx = 0; break;
                }
                return baseIdx + CmbType.SelectedIndex + 1; // +1 porque el enum empieza en 1
            }
            return 1;
        }

        public double? TryGetInput()
        {
            if (double.TryParse(TxtValue.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
                return v;
            MessageBox.Show("Ingrese un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        public void ShowResult(string text) => LblResult.Text = "Resultado: " + text;

        private Panel CrearTarjetaConTexto(Rectangle bounds, Color backColor, Color headerColor, string headerText, int radius)
        {
            Panel pnl = new Panel
            {
                Location = bounds.Location,
                Size = bounds.Size,
                BackColor = backColor,
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, bounds.Width, bounds.Height, radius, radius))
            };

            Panel header = new Panel
            {
                Size = new Size(bounds.Width, 30),
                Location = new Point(0, 0),
                BackColor = headerColor
            };
            header.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, bounds.Width, 30, radius, radius));

            // Dibujar el texto directamente sobre la cabecera
            header.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Font font = new Font("Segoe UI", 10, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(headerText, font, brush, new PointF(15, 6));
                }
            };

            pnl.Controls.Add(header);
            return pnl;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
    }
}