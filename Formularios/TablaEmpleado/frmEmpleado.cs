using Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static Objetos.Empleado;
using Formularios.Utilitario;
namespace sistema_de_viajes
{
    public partial class TablaEmpleado : Form
    {

        public TablaEmpleado()
        {
            InitializeComponent();
        }
        private Mantenimiento _estado;
        public Boolean nuevo { get; set; }
        String estado;
        ModeloEmpleado me = new ModeloEmpleado();
        Empleado em = new Empleado();
        //empleado em = new empleado();
        Usuario u = new Usuario();
        private void limpiar()
        {
            
            txtapellido.Clear();
            txtcontraseña.Clear();
            txtdni.Clear();
            txtnombre.Clear();
            txtusuario.Clear();
            cbcargo.SelectedIndex = -1;
            cbperfil.SelectedIndex = -1;
            cbgenero.SelectedIndex = -1;
            dtnacimiento.ResetText();
        }
        private Boolean validardatos()
        {
            
            if (txtapellido.Text != "" &&
                txtcontraseña.Text != "" &&
                txtdni.Text != "" &&
                txtnombre.Text != "" &&
                txtusuario.Text != "" &&
                cbcargo.SelectedValue != null &&
                cbperfil.Text != "" &&
                cbgenero.SelectedIndex != -1) { return true; }
            else return false;
            
        }
        private void datos()
        {
            em.id = idempleado;            
            em.nombres = txtnombre.Text;
            em.apellidos = txtapellido.Text;
            em.fechanacimiento = (DateTime)dtnacimiento.Value;
            em.idcargo = Convert.ToInt32(cbcargo.SelectedValue);
            em.dni = txtdni.Text;
            em.genero = cbgenero.SelectedValue.ToString();
            u.nombreusuario = txtusuario.Text;
            u.contrasena = txtcontraseña.Text;
            u.tipo = cbperfil.SelectedValue.ToString();
        }
        private void TablaEmpleado_Load(object sender, EventArgs e)
        {


            
            
            HabilitarMantenimiento(true);
            CargarPermisos();
            CargarCargos();
            CargarGenero();
            cbcargo.SelectedIndex = -1;
            //desactivartxt();
            ActivarControles(false);
            CargarGenero();

            CrearColumnas();
            CargarDatosEmpleados();
        }
        private void CrearColumnas()
        {
            //se personaliza la grilla o cuadricula
            DataGridView dgv =  Util.CrearGrillaVista(dgvdatos);
            //Agregaremos las columnas a la grilla o cuadricula
            bool eslecturasi = true, eslecturano = false, eseditablesi = true, eseditableno = false, esvisiblesi = true, esvisibleno = false;
            Util.CrearColumnaTexto(dgv, "idempleado", "idempleado", 0, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "apellidos", "apellidos", 1, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "nombres", "nombres", 2, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "dni", "dni", 3, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "genero", "genero", 4, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "fechanacimiento", "fechanacimiento", 5, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "nombreusuario", "nombreusuario", 6, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "contrasena", "contrasena", 7, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "tipocuenta", "tipocuenta", 8, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "nombretipocuenta", "nombretipocuenta", 9, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "idcargo", "idcargo", 10, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            Util.CrearColumnaTexto(dgv, "nombrecargo", "nombrecargo", 11, "", 100, eslecturasi, eseditableno, esvisiblesi, "");
            
            
        }
        private void CargarDatosEmpleados()
        {
            dgvdatos.DataSource = me.listarEmpleado("","");

        }
        private void HabilitarMantenimiento(bool  esModoMantenimiento)
        {
            btnnuevo.Enabled = esModoMantenimiento;
            btneditar.Enabled = esModoMantenimiento;
            btneliminar.Enabled = esModoMantenimiento;
            btnguardar.Enabled = !esModoMantenimiento;
            btncancelar.Enabled = !esModoMantenimiento;

        }
        private void CargarPermisos()
        {
            //cbpermisos.Items.Add("Admin");
            //cbpermisos.Items.Add("Usuario");
            List<TablaGeneral> lista = TablaGeneralDatos.ListarTablaGeneral("PERFILUSUARIO", "");
            cbperfil.DataSource = lista;
            cbperfil.DisplayMember = "descripcion";
            cbperfil.ValueMember = "valor";
        }
        private void CargarGenero()
        {
            //cbsexo.Items.Add("Masculino");
            //cbsexo.Items.Add("Femenino");

            TablaGeneral tbl = new TablaGeneral();
            cbgenero.DataSource = tbl.ListarGenero();
            cbgenero.ValueMember = "valor";
            cbgenero.DisplayMember = "descripcion";
        }
        private void CargarCargos()
        {
            cbcargo.DataSource = me.listarcargos();
            cbcargo.ValueMember = "ID";
            cbcargo.DisplayMember = "Nombre";
        }
        //private void desactivartxt()
        //{
        //    txtapellido.Enabled = false;
        //    txtcontraseña.Enabled = false;
        //    txtdni.Enabled = false;
        //    txtnombre.Enabled = false;
        //    txtusuario .Enabled = false;
        //    cbsexo .Enabled = false;
        //    cbcargo.Enabled = false;
        //    cbpermisos.Enabled = false;
        //    dtnacimiento .Enabled = false;
            
        //}
                
        
        private void ActivarControles(bool estado)
        {
            txtapellido.Enabled = estado;
            txtcontraseña.Enabled = estado;
            txtdni.Enabled = estado;
            txtnombre.Enabled = estado;
            txtusuario.Enabled = estado;
            cbcargo .Enabled = estado;
            cbperfil  .Enabled = estado;
            cbgenero .Enabled = estado;
            dtnacimiento.Enabled = estado;
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            
            limpiar();
            
            //estado = "g";
            _estado = Mantenimiento.nuevo;
            //btnguardar.Enabled=true;
            //btneditar.Enabled = false;
            //btneliminar.Enabled = false;
            HabilitarMantenimiento(false);
            ActivarControles(true);
            dgvdatos.Enabled = false;
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //btneditar.Enabled = false;
            //btnguardar.Enabled = false;
            //btnnuevo.Enabled = true;
            //btneliminar .Enabled = false;
            limpiar();
            ActivarControles(false);
            HabilitarMantenimiento(true);
            dgvdatos.Enabled = true;
            _estado = Mantenimiento.listar;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(validardatos())
            {
                if(_estado == Mantenimiento.editar)
                //if (estado.Equals("e"))
                {
                    string dni = em.dni;
                    string usuario = u.nombreusuario;
                    //Boolean v1 = false;
                    Boolean v2 = false;
                    datos();
                    //if (txtdni.Text != dni)
                    //{

                    //    if (me.ValidarDni(em))
                    //    {
                    //        MessageBox.Show("No puede insertar un dni asociado a otro empleado.");
                    //    } else {
                    //        v1 = true;
                    //    }

                    //} else {
                    //    v1 = true;
                    //}

                    //if (txtusuario.Text != usuario)
                    //{
                    //    if (me.ValidarUser(u)) {
                    //        MessageBox.Show("No se puede insertar un usuario asociado a otro empleado.");
                    //    } else {
                    //        v2 = true;
                    //    }

                    //} else {
                    //    v2 = true;
                    //}
                    
                    me.editarEmpleado(em, u);
                    limpiar();
                    //desactivartxt();
                    ActivarControles(false);
                    dgvdatos.DataSource = me.listarEmpleado("", "");
                    dgvdatos.Enabled = true;
                    HabilitarMantenimiento(true);

                    Util.MostrarMensajeInformativo("Registro actualizado");
                    //if (v2)
                    //{
                       
                    //}
                    
                }
                if(_estado == Mantenimiento.nuevo)
                //if (estado.Equals("g"))
                {
                    //me traer los datos de la entidad tipada
                    datos();
                    
                    //valida el dni contra lo que tenemos en la base de datos de la tabla empleado contra los registro 
                    if (me.ValidarDni(em)) {
                        //MessageBox.Show("No se guardo, no se puede haber Dni de empleados iguales");
                        Util.MostrarMensajeAlerta("El dni ingresado ya se encuentra asociado a un registrado a otro empleado.");
                    }
                    else if (me.ValidarUser(u))
                    {
                        Util.MostrarMensajeAlerta("El usuario ingresado ya se encuentra asociado a otro empleado");
                        //MessageBox.Show("No se guardo, no puede insertar un usuario anteriormente registro con otro empleado");
                    }
                    else
                    {
                        me.guardarEmpleado(em, u);
                        limpiar();
                        //desactivartxt();
                        ActivarControles(false);

                        dgvdatos.DataSource = me.listarEmpleado("","");

                        HabilitarMantenimiento(true);
                        //btneditar.Enabled = false;
                        //btnguardar.Enabled = false;
                        //btnnuevo.Enabled = true;
                        //btneliminar.Enabled = false;

                        dgvdatos.Enabled = true;
                        //MessageBox.Show("se guardo");
                        Util.MostrarMensajeInformativo("Registro guardado");
                    }
                }
            } else MessageBox.Show("No se guardo, faltan rellenar datos");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private int idempleado = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception e) {
            //    Util.MostrarMensajeError("Error al seleccionar registro "  e.Message);
            //}
            if (dgvdatos.CurrentRow.Cells[0].Value == null)
            {
                return;
            }

            idempleado = (int)dgvdatos.CurrentRow.Cells[0].Value;

            //Empleado dr = me.listarEmpleadoId(em.id);
            //selecciona la fila del control

            //EmpleadoDTO dr = me.listarEmpleado(tipo, valor);
            
            txtapellido.Text = Util.ObtenerTextoCeldaActual(dgvdatos, "apellidos");
            txtnombre.Text = Util.ObtenerTextoCeldaActual(dgvdatos, "nombres");
            txtdni.Text = Util.ObtenerTextoCeldaActual(dgvdatos, "dni");
            if(dgvdatos.CurrentRow.Cells["fechanacimiento"].Value != null)
            {
                dtnacimiento.Value = Convert.ToDateTime(dgvdatos.CurrentRow.Cells["fechanacimiento"].Value);
            }            
            txtusuario.Text = Util.ObtenerTextoCeldaActual(dgvdatos, "nombreusuario");
            txtcontraseña.Text = Util.ObtenerTextoCeldaActual(dgvdatos, "contrasena");
            cbcargo.SelectedValue = Util.ObtenerTextoCeldaActual(dgvdatos, "idcargo");
            cbperfil.SelectedValue = Util.ObtenerTextoCeldaActual(dgvdatos, "tipocuenta");
            cbgenero.SelectedValue = Util.ObtenerTextoCeldaActual(dgvdatos, "genero");
            
            //cbpermisos.SelectedIndex = dr.usuario.tipo;


            //cbsexo.SelectedValue = (dr["SexoEmpl"].ToString());
            //dtnacimiento.Value = (DateTime)dr["Nacimiento"];

            //btneditar.Enabled = true;
            //btneliminar.Enabled = true;

            //datos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            dgvdatos.Enabled = false;
            
            //estado = "e";
            _estado = Mantenimiento.editar;
            //btnguardar.Enabled = true;
            //btnnuevo.Enabled = false;
            HabilitarMantenimiento(false);
            ActivarControles(true);
            
        }
         
        //evento de boton buscar
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtfiltro.Text != "")
            {
                dgvdatos.DataSource = me.listarEmpleado(cbfiltro.Text, txtfiltro.Text);
            }
            else
            {
                dgvdatos.DataSource = me.listarEmpleado("","");
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            me.eliminarEmpleado(em.id);
            dgvdatos.DataSource = me.listarEmpleado("","");
            limpiar();
            ActivarControles(false);
            dgvdatos.Enabled = true;
            HabilitarMantenimiento(true);
            _estado = Mantenimiento.listar;
            Util.MostrarMensajeInformativo("Se elimno con exitosa");
            
            
        }
    }
}
