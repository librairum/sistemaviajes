using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formularios.Utilitario
{
    public static class Util
    {

        //Vista cuadricula -> Datagridview
        public static string ObtenerTextoCeldaActual(DataGridView dgv, string nombreColumna)
        {
            string resultado = "";
            if (!nombreColumna.Equals(""))
            {
                object valorCelda = ObtenerValorCeldaActual(dgv, nombreColumna);
                if (valorCelda != null)
                {
                    resultado =  ConvertiraCadena(valorCelda);
                }
            }
            return resultado;
        }
        public static int ObtenerNumeroCeldaActual(DataGridView dgv, string nombreColumna)
        {
            int resultado = 0;
            if (!nombreColumna.Equals(""))
            {
                object valorCelda = ObtenerValorCeldaActual(dgv, nombreColumna);
                if (valorCelda != null)
                {
                    resultado = (int)(valorCelda);
                }
            }
            return resultado;
        }
        public static string ConvertiraCadena(object valor)
        {
            string resultado = "";
            if(valor!= null)
            {
                resultado = valor.ToString();
            }
            return resultado;
        }

        private static object ObtenerValorCeldaActual(DataGridView dgv,string nombreColumna)
        {
            return dgv.CurrentRow.Cells[nombreColumna].Value;
        }
        private static string IngresarMensaje(Mensaje tipo,string mensaje="")
        {
            string resultado = "";
            if(tipo == Mensaje.informativo)
            {
                if (mensaje != "")
                {
                    resultado = "Operacion exitosa";
                }
            }
                
            return resultado;
        }
        public static bool MostrarMensajeInformativo(string mensaje, int flag = 1)
        {
            bool processOK = false;

            if (string.IsNullOrEmpty(mensaje) == true)
            {
                mensaje = "";
            }
            if (flag == 0 || flag == 1)
            {
                MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                processOK = true;
            }
            //else if (flag == -1)
            else
            {
                MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                processOK = false;
            }
            return processOK;
        }
        public static bool MostrarMensajeAlerta(string mensaje)
        {
            bool processOK = false;
            if (string.IsNullOrEmpty(mensaje) == true)
            {
                mensaje = "";
            }
            //RadMessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return processOK;
        }
        public static bool MostrarMensajeError(string mensaje)
        {
            bool processOK = false;
            if (string.IsNullOrEmpty(mensaje) == true)
            {
                mensaje = "";
            }
            //RadMessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return processOK;
        }
        public static bool MostrarMensajePregunta(string mensaje)
        {
            bool processOK = false;

            processOK = MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes;
            //Si acepto ejecutar la accion, entonces el valor sera true;
            return processOK;
        }

        public static DataGridView CrearGrillaVista(DataGridView dgv)
        {
            // Limpiar columnas y deshabilitar la generación automática
            dgv.Columns.Clear();
            dgv.AutoGenerateColumns = false;

            // Deshabilitar la adición de nuevas filas y otras opciones
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToOrderColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = true;


            // Implementación de ToolTip
            dgv.CellMouseMove += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    var cellValue = dgv[e.ColumnIndex, e.RowIndex].Value;
                    if (cellValue != null)
                    {
                        dgv[e.ColumnIndex, e.RowIndex].ToolTipText = cellValue.ToString();
                    }
                }
            };

    

            return dgv;
        }

        public static void CrearColumnaTexto(DataGridView gridView, string caption, string field, int visibleIndex,
                            string formatString, int widthField = 0, bool readOnly = true,
                            bool allowEdit = false, bool visible = true, string textAlign = "")
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = caption,
                DataPropertyName = field,
                Name = field,
                Visible = visible,
                ReadOnly = readOnly
            };

            if (widthField != 0)
            {
                newColumn.Width = widthField;
                newColumn.MinimumWidth = widthField;
            }

            if (!string.IsNullOrEmpty(formatString))
            {
                newColumn.DefaultCellStyle.Format = formatString;
            }

            switch (textAlign.ToLower())
            {
                case "left":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case "center":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "right":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                default:
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    break;
            }

            gridView.Columns.Insert(visibleIndex, newColumn);
        }
        public static void CrearColumnaBoton(DataGridView gridView, string caption, string field, string text, string toolTip,
                            int visibleIndex, int widthField = 0, string textAlign = "")
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                HeaderText = caption,
                Name = "buttonColumn",
                Text = text,
                UseColumnTextForButtonValue = true,
                //UseColumnImageForButtonValue = true,
                Width = widthField,
                ToolTipText = toolTip

            };


            gridView.Columns.Insert(visibleIndex, buttonColumn);
        }


        public static void CrearColumnaImagen(DataGridView gridView, string caption, string field, int visibleIndex, Bitmap bitmapImage,
                                int widthField = 0, bool readOnly = true,
                                bool allowEdit = false, bool visible = true, string imageAlign = "")
        {
            DataGridViewImageColumn newColumn = new DataGridViewImageColumn
            {
                HeaderText = caption,
                DataPropertyName = field,
                Name = field,
                Visible = visible,
                ReadOnly = readOnly,
                Image = bitmapImage

            };

            newColumn.DefaultCellStyle.BackColor = Color.LightGray;


            if (widthField != 0)
            {
                newColumn.Width = widthField;
                newColumn.MinimumWidth = widthField;
            }

            switch (imageAlign.ToLower())
            {
                case "left":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case "center":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "right":
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                default:
                    newColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    break;
            }

            gridView.Columns.Insert(visibleIndex, newColumn);
        }

    }
}
