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
using Veterinaria_Backend.dominio;
using Veterinaria_Backend.servicios;

namespace VeterinariaFrontend.presentacion
{
    public partial class Transacciones : Form
    {

        public enum Acciones
        {
            CREAR,
            READ,
            UPDATE,
            DELETE
        }
        private Atencion atencion;
        private Mascota mascota;
        private IService servicio;
        private Acciones modo;
        public Transacciones()
        {
            InitializeComponent();
            mascota = new Mascota();
            atencion = new Atencion();
            servicio = new VeterinariaService();
            this.modo = modo;
        }



        private void Transacciones_Load(object sender, EventArgs e)
        {
            SetearCampos();
            CargarComboCliente();
            CargarComboServicio();
            CargarComboTipo();
        }


        public bool ValidarCampos()
        {
            if (txtNombre.Text == string.Empty || txtNombre.Text == "NOMBRE MASCOTA")
            {
                MessageBox.Show("Debe ingresar un Nombre", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtNombre.Focus();
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
            if (cboServicio.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Servicio", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cboServicio.Focus();
                return false;
            }
            if (cboCliente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Cliente", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cboCliente.Focus();
                return false;
            }
            if (cboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Tipo de Mascota", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cboTipo.Focus();
                return false;
            }

            return true;
        }

        public void SetearCampos()
        {
            //lblMascotaNro.Text += servicio.ProximoNro();
            txtFecha.Text = DateTime.Today.ToString("dd/MM/yyyy");
           cboCliente.SelectedIndex = -1;
            
            cboTipo.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;
            
        }

        public void CargarComboCliente()
        {
            List<Cliente> lista = servicio.CargarComboCliente();
            cboCliente.DataSource = lista;
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "IdCliente";
            cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void CargarComboTipo()
        {
            List<Tipo> lista = servicio.CargarComboTipo();
            cboTipo.DataSource = lista;
            cboTipo.DisplayMember = "NombreTipo";
            cboTipo.ValueMember = "IdTipo";
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void CargarComboServicio()
        {
            List<Servicio> lista = servicio.CargarComboServicio();
            cboServicio.DataSource = lista;
            cboServicio.DisplayMember = "Service";
            cboServicio.ValueMember = "IdServicio";
            cboServicio.DropDownStyle = ComboBoxStyle.DropDownList;
        }




        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir del Alta", "CANCELAR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        //private void btnAgregar_Click(object sender, EventArgs e)
        //{
            
            
        

        //}

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTranTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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
                txtDetalle.ForeColor = Color.DimGray;
            }

        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {

            if (txtNombre.Text == "NOMBRE MASCOTA")
            {
                txtNombre.Text = "";
                txtNombre.ForeColor = Color.LightGray;
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {

            if (txtNombre.Text == "")
            {
                txtNombre.Text = "NOMBRE MASCOTA";
                txtNombre.ForeColor = Color.DimGray;
            }

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

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {
                string theDate = dtpNacimiento.Value.ToString("dd-MM-yyyy");
                mascota.Nombre = txtNombre.Text;
                mascota.IdCliente = Convert.ToInt32(cboCliente.SelectedValue);
                mascota.FechaNac = dtpNacimiento.Value;//theDate;
                mascota.Tipos.IdTipo = Convert.ToInt32(cboTipo.SelectedValue);
                //Servicio serv = (Servicio)cboServicio.SelectedItem;
                
                atencion.Importe = Convert.ToDouble(txtImporte.Text);
                atencion.Fecha = Convert.ToDateTime(txtFecha.Text);
                atencion.Detalles = txtDetalle.Text;
                //mascota.AgregarAtencion(atencion);
            }

            if (modo.Equals(Acciones.CREAR))
            {
                if (servicio.ConfirmarMascota(mascota,atencion))
                {
                    MessageBox.Show("La mascota y sus atenciones se grabaron con éxito", "GRABAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Se ha producido un error al grabar", "GRABAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //btnCancelar.Focus();
                }
                //if (modo.Equals(Acciones.UPDATE))
                //{
                //    if (servicio.ActualizarMascota(mascota))
                //    {
                //        MessageBox.Show("La mascota y sus atenciones se editaron con éxito", "EDITAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        this.Dispose();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Se ha producido un error al editar", "EDITAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        btnCancelar.Focus();
                //    }
                //}
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            txtImporte.Focus();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            txtImporte.Focus();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
