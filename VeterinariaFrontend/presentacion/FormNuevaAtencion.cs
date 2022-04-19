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
    public partial class FormNuevaAtencion : Form
    {
        private IService servicio;

        private Atencion nuevo;

        private int idAtencion;

        public enum Acciones
        {
            CREAR,
            READ,
            UPDATE,
            DELETE
        }



        private Acciones modo;



        public FormNuevaAtencion(Acciones modo, int num)
        {
            InitializeComponent();
            servicio = new VeterinariaService();

            lblNroAtencion.Text += servicio.GetProximaAtencion();
            this.modo = modo;

            if (modo.Equals(Acciones.READ))
            {
                cboCliente.Enabled = false;
                cboCliente.Visible = false;
                cboMascota.Enabled = false;
                btnGuardar.Enabled = false;
                btnGuardar.Visible = false;
                txtDetalle.Enabled = false;
                txtImporte.Enabled = false;
                txtFecha.Enabled = false;
                label1.Visible = false;

                this.Text = "VER ATENCION";
                this.CargarAtencion(num);
            }
            if (modo.Equals(Acciones.UPDATE))
            {
                cboCliente.Visible = false;
                cboCliente.Enabled = false;
                cboMascota.Enabled = false;
                btnGuardar.Enabled = true;
                txtDetalle.Enabled = true;
                txtImporte.Enabled = true;
                txtFecha.Enabled = false;
                this.Text = "EDITAR ATENCION";
                this.CargarAtencion(num);
            }



        }

        private async void FormNuevaAtencion_Load(object sender, EventArgs e)
        {
            await CargarCombo();
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
            if (modo.Equals(Acciones.CREAR))
            {
                cboCliente.Visible = true;
                cboCliente.Enabled = true;
                cboMascota.Enabled = true;
                btnGuardar.Enabled = true;
                txtDetalle.Enabled = true;
                txtImporte.Enabled = true;
                txtFecha.Enabled = true;
                this.Text = "CREAR ATENCION";

            }

        }

        public void CargarAtencion(int num)
        {

            Atencion atencion = new Atencion();
            servicio.CargarComboCliente();
            servicio.CargarComboMascota();
            atencion = servicio.ObtenerAtencionID(num);
            //cboCliente.SelectedValue = atencion.IdCliente;
            cboMascota.SelectedValue = atencion.IdMascota;
            txtDetalle.Text = atencion.Detalles.ToString();
            txtImporte.Text = atencion.Importe.ToString();
            txtFecha.Text = atencion.Fecha.ToString();
            lblNroAtencion.Text = "Atencion N°: " + atencion.IdAtencion.ToString();




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




        //private async Task CargarCombo()
        //{
        //    string url = "https://localhost:44320/api/Veterinaria/GetMascotas";
        //    using (HttpClient cliente = new HttpClient())
        //    {
        //        var result = await cliente.GetAsync(url);
        //        var bodyJSON = await result.Content.ReadAsStringAsync();
        //        List<Mascota> mascotas = JsonConvert.DeserializeObject<List<Mascota>>(bodyJSON);
        //        cboMascotas.DataSource = mascotas;
        //        cboMascotas.ValueMember = "IdMascota";
        //        cboMascotas.DisplayMember = "Nombre";
        //    }

        //}

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarAtencion();
        }

        private async void GuardarAtencion()
        {

            if (ValidarCampos())
            {

                Atencion oAtencion = new Atencion();
                //oAtencion.IdAtencion = servicio.GetProximaAtencion();
                oAtencion.IdMascota = Convert.ToInt32(cboMascota.SelectedValue.ToString());
                //oAtencion.Fecha = dtpFecha.Value;

                oAtencion.Importe = Convert.ToDouble(txtImporte.Text);
                //oAtencion.Descripcion = 1;//Convert.ToInt32(cboDescripcion.SelectedValue.ToString());
                oAtencion.Detalles = txtDetalle.Text;


                //bool guardado = servicio.SaveAtenciones(oAtencion);

                string data = JsonConvert.SerializeObject(oAtencion);

                bool result = await GrabarAtencionAsync(data);


                if (result)
                {
                    MessageBox.Show("Atención registrada con éxito!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("No pudo registrarse la atención", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                }



            }
        }
        private async Task<bool> GrabarAtencionAsync(string data)
        {
            string url = "https://localhost:44320/api/Veterinaria/SaveAtenciones";
            using (HttpClient client = new HttpClient())
            {

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                string response = await result.Content.ReadAsStringAsync();
                return response.Equals("Ok");
            }
        }


        public bool ValidarCampos()
        {
            if (txtDetalle.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Detalle", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtDetalle.Focus();
                return false;
            }
            if (txtImporte.Text == string.Empty || txtImporte.Text == "0")
            {
                MessageBox.Show("Debe ingresar una Importe", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtImporte.Focus();
                return false;


            }
            if (Convert.ToInt32(txtImporte.Text) == 500000)
            {
                MessageBox.Show("No es un importe valido", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtImporte.Focus();
                return false;
            }
            else
            {
                try
                {
                    Convert.ToInt32(txtImporte.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Debe ingresar un valor numérico", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    txtImporte.Focus();
                    return false;
                }
            }

            if (cboMascota.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una Mascota", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cboMascota.Focus();
                return false;
            }


            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImporte_Enter(object sender, EventArgs e)
        {
            if (txtImporte.Text == "0")
            {
                txtImporte.Text = "";
                txtImporte.ForeColor = Color.LightGray;
            }
        }

        private void txtImporte_Leave(object sender, EventArgs e)
        {
            if (txtImporte.Text == "")
            {
                txtImporte.Text = "0";
                txtImporte.ForeColor = Color.DimGray;
            }
        }

        private void txtDetalle_Enter(object sender, EventArgs e)
        {
            if (txtDetalle.Text == "DETALLE DE ATENCION...")
            {
                txtDetalle.Text = "";
                txtDetalle.ForeColor = Color.LightGray;
            }
        }

        private void txtDetalle_Leave(object sender, EventArgs e)
        {
            if (txtDetalle.Text == "")
            {
                txtDetalle.Text = "DETALLE DE ATENCION...";
                txtDetalle.ForeColor = Color.LightGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            txtImporte.Focus();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            txtImporte.Focus();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            GuardarAtencion();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void panelLeft_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
