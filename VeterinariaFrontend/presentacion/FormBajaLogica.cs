using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria_Backend.dominio;
using Veterinaria_Backend.servicios;

namespace VeterinariaFrontend.presentacion
{
    public partial class FormBajaLogica : Form
    {
        IService service;
        public FormBajaLogica()
        {
            InitializeComponent();
            service = new VeterinariaService();
        }

        private async void FormBajaLogica_Load(object sender, EventArgs e)
        {
            await CargarCombo();
            
        }


        private async Task CargarCombo()
        {
            string url = "https://localhost:44320/api/Veterinaria/GetClientes";
            using (HttpClient cliente = new HttpClient())
            {
                var result = await cliente.GetAsync(url);
                var bodyJSON = await result.Content.ReadAsStringAsync();
                List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(bodyJSON);
                cboCliente.DataSource = clientes;
                cboCliente.ValueMember = "IdCliente";
                cboCliente.DisplayMember = "nombre";
                cboCliente.SelectedValueChanged += cboClientes_SelectedValueChanged;
                string mascota = cboCliente.SelectedValue.ToString();
                await CargarComboMascotas(mascota);

            }

        }




        private async void cboClientes_SelectedValueChanged(object sender, EventArgs e)
        {
            string mascota = cboCliente.SelectedValue.ToString();
            await CargarComboMascotas(mascota);
        }

        public async Task CargarComboMascotas(string id_cliente)


        {


            string url = "https://localhost:44320/api/Veterinaria/GetByFiltersMascota?id_cliente=" + id_cliente;
            using (HttpClient cliente = new HttpClient())
            {

                var result = await cliente.GetAsync(url);
                var bodyJSON = await result.Content.ReadAsStringAsync();

                List<Mascota> mascotas = JsonConvert.DeserializeObject<List<Mascota>>(bodyJSON);
                cboMascota.DataSource = mascotas;
                cboMascota.ValueMember = "idMascota";
                cboMascota.DisplayMember = "nombre";



            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
                if (EliminarPresupuesto())
                {
                    MessageBox.Show("La Mascota ha sido dada de baja", "BAJAS", MessageBoxButtons.OK);
                    btnEliminar.Focus();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error y la Mascota no se ha dado de baja", "BAJAS", MessageBoxButtons.OK);
                    btnEliminar.Focus();
                }
            }
            public bool EliminarPresupuesto()
            {
                int idM = Convert.ToInt32(cboMascota.SelectedValue);
                bool modificacion = false;
                if (MessageBox.Show("¿Desea dar de baja la Mascota?", "BAJAS",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {

                    modificacion = service.EliminarMascota(idM);
                }
                else
                {
                    btnSalir.Focus();
                    return modificacion;
                }
                return modificacion;
            

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
                this.Close();
            
        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void FormBajaLogica_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
