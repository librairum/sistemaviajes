using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Objetos.Empleado;

namespace Objetos
{
    public class ModeloEmpleado
    {
        

        public SqlDataReader leerUsuario()
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand("Select * from Usuario", con.Open());
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public Boolean ValidarUser(Usuario u)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand("Select NombreUsuario from Usuario where NombreUsuario = @Usuario", con.Open());
            cmd.Parameters.AddWithValue("@Usuario", u.nombreusuario);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true) {con.Close(); return true; }
            else {con.Close(); return false;}
        }
        public Boolean ValidarDni(Empleado e)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand("Select Dni from Empleado where Dni = @dni", con.Open());
            cmd.Parameters.AddWithValue("@dni", e.dni);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true) { con.Close(); return true; }
            else { con.Close(); return false; }
        }
        public string  Validar(Usuario u)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand("Select NombreUsuario from Usuario where (NombreUsuario = @Usuario and Contrasena = @Contrasena)", con.Open());
            cmd.Parameters.AddWithValue("@Usuario", u.nombreusuario);
            cmd.Parameters.AddWithValue("@Contrasena", u.contrasena);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if(dr.FieldCount < 2)
                {
                    return dr["NombreUsuario"].ToString();
                } else {
                    return "";
                }

            } else {
                return "";
            }
            
        }
        //public DataTable listarEmpleado()
        //{
        //    Conexion con = new Conexion();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter cmd = new SqlDataAdapter("Select e.ID,e.Nombres,e.Apellido,e.Dni,c.Nombre as NombreCargo from Empleado as e inner join Cargo as c on e.IDCargo = c.ID", con.Open());
        //    cmd.Fill(dt);
        //    con.Close();
        //    return dt;
        //}
        //public DataTable buscarEmpleado(string filtro, string nombre)
        //{
        //    Conexion con = new Conexion();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("buscarEmpleado", con.Open());
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@filtro", filtro);
        //    cmd.Parameters.AddWithValue("@nombre", nombre);
        //    SqlDataAdapter dr = new SqlDataAdapter(cmd);
        //    dr.Fill(dt);
        //    con.Close();
        //    Dictionary<string, object> parametros = new Dictionary<string, object>();
        //    parametros.Add("@filtro")


        //    return dt;
        //}
        public int guardarEmpleado(Empleado e,Usuario u)
        {
            //poblar los parametros
            Dictionary<string,object> parametros = new Dictionary<string, object>();
            parametros.Add("@Nombres", e.nombres);
            parametros.Add("@Apellidos", e.apellidos);
            parametros.Add("@Dni", e.dni);
            parametros.Add("@FechaNacimiento", e.fechanacimiento);
            parametros.Add("@Genero", e.genero);
            parametros.Add("@IdCargo", e.idcargo);
            parametros.Add("@Nombreusuario", u.nombreusuario);
            parametros.Add("@Contrasena", u.contrasena);
            parametros.Add("@Tipo", u.tipo); 
            int estado = MetodoGenerico.Insertar(parametros, "Spu_SVB_Ins_Empleado");
            //sucedio un valor de ingreso a la tabla
            return estado;
      
        }
        public DataTable listarcargos()
        {
            Conexion con = new Conexion();
            DataTable dt = new DataTable();
            SqlDataAdapter cmd = new SqlDataAdapter("select ID,Nombre from Cargo", con.Open());
            cmd.Fill(dt);
            con.Close();
            return dt;
        }
        public List <EmpleadoDTO> listarEmpleado(string tipo, string valor )
        {
            //Conexion con = new Conexion();
            //SqlCommand cmd = new SqlCommand("SPU_SVB_TRAE_EMPLEADO", con.Open());
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@id", id);
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@tipo", tipo);
            parametros.Add("@valor", valor);
             List<EmpleadoDTO> lista =  MetodoGenerico.ListarDatos<EmpleadoDTO>("SPU_SVB_TRAE_EMPLEADO", parametros);
            //EmpleadoDTO dato = lista[0];

           
            return lista;

        }
        
        public int editarEmpleado(Empleado e, Usuario u)
        {
            
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@idempleado", e.id);
            parametros.Add("@nombres", e.nombres);
            parametros.Add("@apellidos", e.apellidos);
            parametros.Add("@Dni", e.dni);
            parametros.Add("@fechanacimiento", e.fechanacimiento);
            parametros.Add("@genero", e.genero);
            parametros.Add("@idcargo", e.idcargo);
            parametros.Add("@nombreusuario", u.nombreusuario);
            parametros.Add("@contrasena", u.contrasena);
            parametros.Add("@tipocuenta", u.tipo);
            int estado = MetodoGenerico.Actualizar(parametros, "spu_svb_Upd_Empleado");
            /*
             @idempleado int,       
@nombres VARCHAR(100),       
@apellidos VARCHAR(100),       
@Dni char(8),       
@fechanacimiento date,       
@genero  char(1),    
@idcargo int,    
@nombreusuario varchar(50),    
@contrasena varchar(50),    
@tipocuenta varchar(50)   
             */

            return estado;
        }
        public void eliminarEmpleado(int id)
        {
            Conexion con = new Conexion();
            SqlCommand cmd = new SqlCommand("eliminarEmpleado", con.Open());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

    }
}
