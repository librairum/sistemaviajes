using Formularios;
using Objetos;
using System;
using System.Windows.Forms;
using static Objetos.Empleado;
using Formularios.Utilitario;
namespace sistema_de_viajes
{
    public partial class Iniciarsecion : Form
    {
        private ModeloEmpleado me = new ModeloEmpleado();
        private Usuario u = new Usuario();
        private Mensaje _estadoMensaje;
        public Iniciarsecion()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (me.leerUsuario().HasRows == false)
            {
                TablaEmpleado te = new TablaEmpleado();
                te.Show();
                Util.MostrarMensajeInformativo("Bienvenido, registre su cuenta primero para crear su usuario e iniciar seccion");
                //MessageBox.Show("Bienvenido, registre su cuenta primero para crear su usuario e iniciar seccion");
            }
        }

        private void leer()
        {
            u.nombreusuario = txtusuario.Text;
            u.contrasena = txtcontrasena.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            leer();
            if (me.ValidarUser(u))
            {
                if (me.Validar(u) != "")
                {
                    Menuprincipal menu = new Menuprincipal(me.Validar(u));
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    //MessageBox.Show("Error en la contraseña");
                    Util.MostrarMensajeInformativo("Error en la contraseña");
                }
            }
            else
            {
                //MessageBox.Show("Error en el usuario");
                Util.MostrarMensajeError("Error en el usuario");
                //Util.MostrarMensajeInformativo("Error en el usuario");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtusuario_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtcontrasena.Focus();
            }
        }

        private void txtcontrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnIngresar.Focus();
            }
        }
    }
}
