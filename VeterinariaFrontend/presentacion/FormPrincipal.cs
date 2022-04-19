using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria_Backend.dominio;
using VeterinariaFrontend.client;
using Veterinaria_Backend.servicios;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Data.SqlClient;
using static VeterinariaFrontend.presentacion.FormNuevaAtencion;
using System.Runtime.InteropServices;

namespace VeterinariaFrontend.presentacion
{

    public partial class FormPrincipal : Form
    {
        string id_cliente;

        IService service;






        public FormPrincipal()
        {
            InitializeComponent();
            service = new VeterinariaService();

        }


        private async void FormPrincipal_Load(object sender, EventArgs e)
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

        //private void btnConsultar_Click_1(object sender, EventArgs e)
        //{
        //    List<Parametro> criterios = new List<Parametro>();
        //    criterios.Add(new Parametro("@fecha_desde", dtpFechaDesdeC.Value));
        //    criterios.Add(new Parametro("@fecha_hasta", dtpFechaHastaC.Value));
        //    criterios.Add(new Parametro("@mascota", this.cboMascotas.SelectedValue));
        //    List<Atencion> listAtencion = servicio.ConsultarAtenciones(criterios);
        //    dgvAtenciones.Rows.Clear();
        //    foreach (Atencion atencion in listAtencion)
        //    {
        //        dgvAtenciones.Rows.Add(new object[] {atencion.IdAtencion, atencion.Fecha,
        //            atencion.Descripcion, atencion.Importe, atencion.IdMascota});
        //    }
        //}


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





        private void dgvAtenciones_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAtenciones.CurrentCell.ColumnIndex == 5)
            {
                int nroMascota = Convert.ToInt32(dgvAtenciones.CurrentRow.Cells["IdAtencion"].Value.ToString());
                FormNuevaAtencion oMascota = new FormNuevaAtencion(Acciones.READ, nroMascota);
                oMascota.ShowDialog();

            }
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {

            //Servicio servicio = new Servicio();


            List<Parametro> criterios = new List<Parametro>();
            criterios.Add(new Parametro("@fecha_desde", dtpFechaDesdeC.Value));
            criterios.Add(new Parametro("@fecha_hasta", dtpFechaHastaC.Value));
            criterios.Add(new Parametro("@mascota", this.cboMascota.SelectedValue));
            List<Atencion> listAtencion = service.ConsultarAtenciones(criterios);
            dgvAtenciones.Rows.Clear();
            foreach (Atencion atencion in listAtencion)
            {
                dgvAtenciones.Rows.Add(new object[] {atencion.IdAtencion, atencion.Fecha,
                            atencion.Detalles.ToString(), atencion.Importe, atencion.IdMascota});
            }
        }

        private void btnNuevaAtencion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void dgvAtenciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvAtenciones.CurrentCell.ColumnIndex == 5)
        //    {
        //        int nroMascota = Convert.ToInt32(dgvAtenciones.CurrentRow.Cells["ColIdMascota"].Value.ToString());
        //        FormNuevaAtencion oMascota = new FormNuevaAtencion(Acciones.READ, nroMascota);
        //        oMascota.ShowDialog();

        //    }
        //}



        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelLeft_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void dgvAtenciones_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            //ReleaseCapture();
            //SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void dgvAtenciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAtenciones.CurrentCell.ColumnIndex == 5)
            {
                int nroMascota = Convert.ToInt32(dgvAtenciones.CurrentRow.Cells["IdAtencion"].Value.ToString());
                FormNuevaAtencion oMascota = new FormNuevaAtencion(Acciones.READ, nroMascota);
                oMascota.ShowDialog();

            }
            if (dgvAtenciones.CurrentCell.ColumnIndex == 6)
            {
                int nroMascota = Convert.ToInt32(dgvAtenciones.CurrentRow.Cells["IdAtencion"].Value.ToString());
                FormNuevaAtencion oMascota = new FormNuevaAtencion(Acciones.UPDATE, nroMascota);
                oMascota.ShowDialog();


            }
        }
















        //private async Task CargarCombo()
        //{
        //    string url = "https://localhost:44320/api/Veterinaria/GetClientes";
        //    using (HttpClient cliente = new HttpClient())
        //    {
        //        var result = await cliente.GetAsync(url);
        //        var bodyJSON = await result.Content.ReadAsStringAsync();
        //        List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(bodyJSON);
        //        cboClientes.DataSource = clientes;
        //        cboClientes.ValueMember = "IdCliente";
        //        cboClientes.DisplayMember = "nombre";
        //    }

        //}

        //private void cboMascotas_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //public void CargarMascotas(string id_cliente)
        //{
        //    con.Open();
        //    SqlCommand comando = new SqlCommand("SELECT * FROM Mascotas WHERE id_cliente = "+id_cliente, con);
        //    //comando.Parameters.AddWithValue("id_cliente", id_cliente);
        //    SqlDataAdapter da = new SqlDataAdapter(comando);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    con.Close();

        //    DataRow dr = dt.NewRow();
        //    dr["nombre"] = "Selecciona una mascota";
        //    dt.Rows.InsertAt(dr, 0);

        //    cboMascotas.ValueMember = "id_mascota";
        //    cboMascotas.DisplayMember = "nombre";
        //    cboMascotas.DataSource = dt;



        //}


        //private void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboClientes.SelectedValue.ToString() != null)
        //    {
        //        string id_cliente = cboClientes.SelectedValue.ToString();
        //        CargarMascotas(id_cliente);
        //    }
        //}

        //public async Task CargarComboMascotas(string id_cliente)
        //{
        //    string url = "https://localhost:44320/api/Veterinaria/GetByFiltersMascota/" + id_cliente.ToString();
        //    using (HttpClient cliente = new HttpClient())
        //    {

        //        var result = await cliente.GetAsync(url);
        //        var bodyJSON = await result.Content.ReadAsStringAsync();

        //        List<Mascota> mascotas = JsonConvert.DeserializeObject<List<Mascota>>(bodyJSON);
        //        cboMascotas.DataSource = mascotas;
        //        cboMascotas.ValueMember = "idMascota";
        //        cboMascotas.DisplayMember = "nombre";
        //    }

        //}



        //private async void cboClientes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboClientes.SelectedValue.ToString() != null)
        //    {
        //        string id_cliente = cboClientes.SelectedValue.ToString();
        //        await CargarComboMascotas(id_cliente);
        //    }
        //}

    }
}
