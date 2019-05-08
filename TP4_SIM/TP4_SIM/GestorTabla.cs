using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP4_SIM
{
    class GestorTabla
    {
        private DataGridView tabla = null;

        public GestorTabla(DataGridView tabla)
        {
            this.tabla = tabla;
        }

        public void CompletarTabla(double[,] datos)
        {
            tabla.Rows.Clear();

            int fila = 0;
            for (uint i = 0; i < datos.GetLength(0); i++)
            {
                if (datos[i, 0] == 0) break;

                tabla.Rows.Add();
                tabla.Rows[fila].Cells[0].Value = datos[i, 0];
                tabla.Rows[fila].Cells[1].Value = datos[i, 1];
                tabla.Rows[fila].Cells[2].Value = datos[i, 2];
                tabla.Rows[fila].Cells[3].Value = datos[i, 3];
                tabla.Rows[fila].Cells[4].Value = CadenaAuxiliarTabla(datos[i, 4]);
                tabla.Rows[fila].Cells[5].Value = CadenaAuxiliarTabla(datos[i, 5]);
                tabla.Rows[fila].Cells[6].Value = CadenaAuxiliarTabla(datos[i, 6]);
                tabla.Rows[fila].Cells[7].Value = CadenaAuxiliarTabla(datos[i, 7]);
                tabla.Rows[fila].Cells[8].Value = datos[i, 8];
                tabla.Rows[fila].Cells[9].Value = datos[i, 9];
                tabla.Rows[fila].Cells[10].Value = datos[i, 10];
                tabla.Rows[fila].Cells[11].Value = datos[i, 11];
                tabla.Rows[fila].Cells[12].Value = datos[i, 12];

                fila++;
            }
        }

        public void CompletarTablaResultado()
        {

        }

        private string CadenaAuxiliarTabla(double dato)
        {
            if (dato.ToString().Equals("-1"))
            {
                return "En espera";
            }
            return dato.ToString();
        }
    }
}
