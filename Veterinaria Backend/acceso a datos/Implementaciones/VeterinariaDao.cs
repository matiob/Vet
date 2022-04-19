using Veterinaria_Backend.acceso_a_datos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Veterinaria_Backend.dominio;
using Veterinaria_Backend.servicios;

namespace Veterinaria_Backend.acceso_a_datos.Implementaciones
{
    class VeterinariaDao : IVeterinariaDao


    {
        private string connectionString = @"Data Source=PC-PC\SQLDEVELOPER;Initial Catalog=VETEFINAL;Integrated Security=True";

        public List<Cliente> GetClientes()
        {
            List<Cliente> lst = new List<Cliente>();
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("pa_consultar_cliente", cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            cnn.Close();

            foreach (DataRow row in table.Rows)
            {
                Cliente oCliente = new Cliente();
                oCliente.IdCliente = Convert.ToInt32(row["id_cliente"]);
                oCliente.Nombre = row["nombre"].ToString();

                lst.Add(oCliente);
            }

            return lst;
        }

        public List<Mascota> GetMascotas()
        {
            List<Mascota> lst = new List<Mascota>();
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("pa_consultar_mascotas", cnn);

            cmd.CommandType = CommandType.StoredProcedure;

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            cnn.Close();

            foreach (DataRow row in table.Rows)
            {
                Mascota oMascota = new Mascota();
                oMascota.IdMascota = Convert.ToInt32(row["id_mascota"]);
                oMascota.Nombre = row["nombre"].ToString();

                lst.Add(oMascota);
            }

            return lst;
        }


        public List<Mascota> GetByFiltersMascota(string id_cliente)

        {
            int id_cliente_int = Int32.Parse(id_cliente);
            List<Mascota> lstg = new List<Mascota>();
            List<Parametro> filtros = new List<Parametro>();
            SqlConnection cnng = new SqlConnection(connectionString);
            cnng.Open();
            SqlCommand cmda = new SqlCommand("pa_mascota_filtro", cnng);//pa_mascota_filtro", cnn);
            //cmda.Connection = cnng;    
            cmda.CommandType = CommandType.StoredProcedure;

            filtros.Add(new Parametro("@id_cliente", id_cliente));
            cmda.Parameters.AddWithValue("@id_cliente", id_cliente);
            DataTable dt = new DataTable();
            dt.Load(cmda.ExecuteReader());

            cnng.Close();

            foreach (DataRow row in dt.Rows)
            {
                Mascota oMascota = new Mascota();
                oMascota.IdMascota = Convert.ToInt32(row["id_mascota"]);
                oMascota.Nombre = row["nombre"].ToString();
                oMascota.IdCliente = Convert.ToInt32(row["id_cliente"]);

                lstg.Add(oMascota);
            }



            //    cnn.Close();

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        Mascota oMascota = new Mascota();
            //        oMascota.IdMascota = Convert.ToInt32(row["id_mascota"]);
            //        oMascota.Nombre = row["nombre"].ToString();
            //        oMascota.Cliente = row["id_cliente"].ToString();

            //        lst.Add(oMascota);
            //    }

            return lstg;

            //}
        }
        //public List<Mascota> GetByFiltersMascota(string id_cliente)
        //{
        //    List<Mascota> lst = new List<Mascota>();

        //    SqlConnection cnn = new SqlConnection(@"Data Source=localhost;Initial Catalog=Veterinaria_PII;Integrated Security=True");
        //    cnn.Open();
        //    SqlCommand cmd = new SqlCommand("pa_mascota_cliente", cnn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@id_cliente", id_cliente);
        //    DataTable dt = new DataTable();

        //    dt.Load(cmd.ExecuteReader());
        //    cnn.Close();



        //    cnn.Close();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        Mascota oMascota = new Mascota();
        //        oMascota.IdMascota = Convert.ToInt32(row["id_mascota"]);
        //        oMascota.Nombre = row["nombre"].ToString();
        //        oMascota.Cliente = row["id_cliente"].ToString();

        //        lst.Add(oMascota);
        //    }

        //    return lst;

        //}

        public int GetProximaAtencion()
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("pa_proximo_id", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@next";
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
            cnn.Close();

            return (int)param.Value;
        }

        public bool Login(string User, string Password)
        {
            bool b = false;
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            try
            {

                SqlCommand cmd = new SqlCommand("SP_LOGIN", cnn);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@USUARIO", User);
                cmd.Parameters.AddWithValue("@CONTRASENA", Password);

                SqlParameter param = new SqlParameter("@USUARIOS", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
                if ((int)param.Value == 1) b = true;
            }
            catch (SqlException)
            {
                b = false;
            }
            finally
            {
                if (cnn.State == ConnectionState.Open) cnn.Close();
            }
            return b;
        }


        public List<Atencion> ConsultarAtenciones(List<Parametro> criterios)
        {
            List<Atencion> listaAtencion = new List<Atencion>();
            SqlConnection conexion = new SqlConnection(connectionString);
            DataTable tabla = new DataTable();
            try
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("pa_consultar_atenciones", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                foreach (Parametro parametro in criterios)
                {
                    comando.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
                }
                tabla.Load(comando.ExecuteReader());

                foreach (DataRow registro in tabla.Rows)
                {
                    Servicio servicio = new Servicio();
                    servicio.Service = Convert.ToString(registro["servicio"]);
                    Atencion atencion = new Atencion(servicio);
                    atencion.IdAtencion = Convert.ToInt32(registro["atencion_nro"].ToString());
                    atencion.Fecha = Convert.ToDateTime(registro["fecha"].ToString());
                    atencion.Descripcion.Service = servicio.ToString();
                    atencion.Detalles = registro["detalle"].ToString();
                    atencion.Importe = Convert.ToDouble(registro["importe"].ToString());
                    atencion.IdMascota = Convert.ToInt32(registro["id_mascota"].ToString());
                    listaAtencion.Add(atencion);
                }
                conexion.Close();
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
            return listaAtencion;
        }


        public List<Cliente> ListarComboCliente()
        {

            List<Cliente> listCliente = new List<Cliente>();
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_consultar_cliente";
                tabla.Load(comando.ExecuteReader());
                foreach (DataRow registro in tabla.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(registro["id_cliente"].ToString());
                    cliente.Nombre = registro["nombre"].ToString();
                    listCliente.Add(cliente);
                }
                return listCliente;
            }
            catch (SqlException ex)
            {


                throw (ex);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }


        public List<Mascota> ListarComboMascota()
        {

            List<Mascota> listMascota = new List<Mascota>();
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_consultar_mascotas";
                tabla.Load(comando.ExecuteReader());
                foreach (DataRow registro in tabla.Rows)
                {
                    Mascota m = new Mascota();
                    m.IdCliente = Convert.ToInt32(registro["id_mascota"].ToString());
                    m.Nombre = registro["nombre"].ToString();
                    listMascota.Add(m);
                }
                return listMascota;
            }
            catch (SqlException ex)
            {


                throw (ex);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public List<Tipo> ListarComboTipo()
        {

            List<Tipo> listTipo = new List<Tipo>();
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_consultar_tiposMascotas";
                tabla.Load(comando.ExecuteReader());
                foreach (DataRow registro in tabla.Rows)
                {
                    Tipo tipo = new Tipo();
                    tipo.IdTipo = Convert.ToInt32(registro["id_tipo"].ToString());
                    tipo.NombreTipo = registro["tipo"].ToString();
                    listTipo.Add(tipo);
                }
                return listTipo;
            }
            catch (SqlException ex)
            {

                throw (ex);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }
        public List<Servicio> ListarComboServicio()
        {

            List<Servicio> listServicio = new List<Servicio>();
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_consultar_servicios";
                tabla.Load(comando.ExecuteReader());
                foreach (DataRow registro in tabla.Rows)
                {
                    Servicio servicio = new Servicio();
                    servicio.IdServicio = Convert.ToInt32(registro["id_servicio"].ToString());
                    servicio.Service = registro["servicio"].ToString();
                    listServicio.Add(servicio);
                }
                return listServicio;
            }
            catch (SqlException ex)
            {

                throw (ex);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }








        public bool Crear(Mascota oMascota, Atencion oAtencion)
        {
            SqlConnection conexion = new SqlConnection();
            SqlTransaction transaccion = null;
            bool vSalida = true;

            //try
            //{
                conexion.ConnectionString = connectionString;
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_insertar_mascota";
                comando.Parameters.AddWithValue("@nombre", oMascota.Nombre);
                comando.Parameters.AddWithValue("@fec_nac", oMascota.FechaNac);
                comando.Parameters.AddWithValue("@id_tipo", oMascota.Tipos.IdTipo);
                comando.Parameters.AddWithValue("@id_cliente", oMascota.IdCliente);
                SqlParameter psNroMascota = new SqlParameter("@id_mascota", SqlDbType.Int);
                psNroMascota.Direction = ParameterDirection.Output;
                comando.Parameters.Add(psNroMascota);
                comando.ExecuteNonQuery();
                oMascota.IdMascota = Convert.ToInt32(psNroMascota.Value);


            SqlCommand comandoAtencion = new SqlCommand();
            comandoAtencion.Connection = conexion;
            comandoAtencion.Transaction = transaccion;
            comandoAtencion.CommandType = CommandType.StoredProcedure;
            comandoAtencion.CommandText = "pa_insertar_atencion";
            //comandoAtencion.Parameters.AddWithValue("@descripcion", Att.Descripcion.IdServicio);
            comandoAtencion.Parameters.AddWithValue("@importe", oAtencion.Importe);
            comandoAtencion.Parameters.AddWithValue("@id_mascota", Convert.ToInt32(oMascota.IdMascota));
            comandoAtencion.Parameters.AddWithValue("@detalle", oAtencion.Detalles);
            comandoAtencion.ExecuteNonQuery();







            transaccion.Commit();
            //}
                

            


        
            //catch (Exception)
            //{
            //        transaccion.Rollback();
            //        return vSalida = false;
            //}
            //finally
            //{
            //        if (conexion.State == ConnectionState.Open)
            //        {
            //            conexion.Close();
            //        }
            //}

            return vSalida;
        }

        public bool SaveAtenciones(Atencion oAtencion)
        {
            bool flag = true;
            try
            {
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                SqlCommand cmd = new SqlCommand("pa_insertar_atencion", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_atencion", Convert.ToInt32(oAtencion.IdAtencion));
                //cmd.Parameters.AddWithValue("@fecha", Convert.ToDateTime(oAtencion.Fecha));
                //cmd.Parameters.AddWithValue("@descripcion", Convert.ToInt32(oAtencion.Descripcion));

                cmd.Parameters.AddWithValue("@importe", Convert.ToDouble(oAtencion.Importe));

                cmd.Parameters.AddWithValue("@id_mascota", Convert.ToInt32(oAtencion.IdMascota));

                cmd.Parameters.AddWithValue("@detalle", oAtencion.Detalles);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        public bool Delete(int mascotaNum)
        {
            bool registro = false;
            SqlConnection conexion = new SqlConnection();
            SqlTransaction transaccion = null;

            try
            {
                conexion.ConnectionString = connectionString;
                conexion.Open();
                transaccion = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = transaccion;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "pa_eliminar_mascota";
                comando.Parameters.AddWithValue("@m_nro", mascotaNum);
                registro = comando.ExecuteNonQuery() == 1;
                transaccion.Commit();

            }
            catch (Exception)
            {
                transaccion.Rollback();

            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return registro;
        }

        public Atencion ObtAtencionID(int numID)
        {
            Atencion atencion = new Atencion();
            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "pa_consultar_atencion_por_id";
            comando.Parameters.AddWithValue("@nro", numID);
            SqlDataReader lector = comando.ExecuteReader();
            
            while (lector.Read())
            {
                
                
                //Servicio servicio = new Servicio();
                //servicio.Service = lector["servicio"].ToString();
                
                atencion.IdAtencion = Convert.ToInt32(lector["atencion_nro"].ToString());
                atencion.Fecha = Convert.ToDateTime(lector["fecha"].ToString());
                atencion.Detalles = lector["detalle"].ToString();
                atencion.Importe = Convert.ToDouble(lector["importe"].ToString());
                atencion.IdMascota = Convert.ToInt32(lector["id_mascota"].ToString());
             

                
            }

            return atencion;


        }

































    }



}
     

