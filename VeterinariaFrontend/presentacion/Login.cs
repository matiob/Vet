using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria_Backend.servicios;
using VeterinariaFrontend.client;

namespace VeterinariaFrontend.presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
           
        }
        private async void btbAcceder_Click(object sender, EventArgs e)
        {
            List<Parametro> lst = new List<Parametro>();


            if (!String.IsNullOrEmpty(txtUsuario.Text) && !String.IsNullOrEmpty(txtContrasenia.Text))
            {
                lst.Add(new Parametro("user", txtUsuario.Text));
                lst.Add(new Parametro("pass", txtContrasenia.Text));
            }
            else
            {
                MessageBox.Show("Ingrese un usuario y una contraseña!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            string obj = JsonConvert.SerializeObject(lst);
            string url = "https://localhost:44320/api/Veterinaria/login";

            var resultado = await ClienteSingleton.GetInstancia().PostAsync(url, obj);
            var res = JsonConvert.DeserializeObject(resultado);

            if (Convert.ToBoolean(res))
            {
                
                MessageBox.Show("Conectado!", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
              
            }
            else
            {
                MessageBox.Show("No tiene permisos!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void pbCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            { txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray; 
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;

            }
                
        }

        private void txtContrasenia_Enter(object sender, EventArgs e)
        {
            if (txtContrasenia.Text == "PASSWORD")
            {
                txtContrasenia.Text = "";
                txtContrasenia.ForeColor = Color.LightGray;
                txtContrasenia.UseSystemPasswordChar = true;
            }
        }

        private void txtContrasenia_Leave(object sender, EventArgs e)
        {
            if (txtContrasenia.Text == "")
            {
                txtContrasenia.Text = "PASSWORD";
                txtContrasenia.ForeColor = Color.DimGray;
                txtContrasenia.UseSystemPasswordChar = false;
            }
        }
    }
}
