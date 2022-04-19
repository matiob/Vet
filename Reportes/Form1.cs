using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSet1.pa_detalle_atencion_siempre' Puede moverla o quitarla según sea necesario.
            this.pa_detalle_atencion_siempreTableAdapter.Fill(this.DataSet1.pa_detalle_atencion_siempre);

            this.reportViewer1.RefreshReport();
        }
    }
}
