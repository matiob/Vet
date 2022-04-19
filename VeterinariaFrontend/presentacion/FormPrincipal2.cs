using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static VeterinariaFrontend.presentacion.FormNuevaAtencion;

namespace VeterinariaFrontend.presentacion
{
    public partial class FormPrincipal2 : Form
    {
        private int tamanioBordes = 2;

        public FormPrincipal2()
        {
            InitializeComponent();
            this.Padding = new Padding(tamanioBordes);
            this.BackColor = Color.FromArgb(98, 102, 244);
        }

        private void FormPrincipal2_Load(object sender, EventArgs e)
        {
            Login NForm = new Login();
            NForm.ShowDialog();
            
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelManinTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Realmente desea salir del programa?", "SALIR",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void CollapseMenu()
        {
            if (this.panelMainLeft.Width > 150)
            {
                panelMainLeft.Width = 40;
                pictureBox1.Visible = false;
                button1.Dock = DockStyle.Left;
                button1.Padding = new Padding(10);
                


                foreach (Button menuButton in panelMainLeft.Controls.OfType<Button>())
                {
                    menuButton.Text = "";

                }
            }
            else 
            {
                panelMainLeft.Width = 200;
                pictureBox1.Visible = true;
                button1.Dock= DockStyle.None;

                foreach (Button menuButton in panelMainLeft.Controls.OfType<Button>())
                {
                    menuButton.Text = menuButton.Tag.ToString();

                }

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormNuevaAtencion NForm = new FormNuevaAtencion(Acciones.CREAR, 0);
            NForm.ShowDialog();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            FormPrincipal Form = new FormPrincipal();
            Form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transacciones form = new Transacciones();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormBajaLogica form = new FormBajaLogica();
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("112733 - Giménez, Joaquín" + "\n" +
                "112804 - Cacciamano,Joaquin" + "\n" +
                "112962 - Gonzalo, Nicolás" + "\n" +
                "113207 – Buraschi Pire, Mateo", "GRUPO 4 - INTEGRANTES",MessageBoxButtons.OK);
                                                               
        }

        private void panelMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelMainLeft_Paint(object sender, PaintEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Reportes.Form1().ShowDialog();
        }
    }
}
