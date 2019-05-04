using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP4_SIM
{
    public partial class Form1 : Form
    {
        private uint cantSimulaciones = 0;
        private uint desde, hasta = 0;
        private GestorDatos gestorDatos = null;
        private GestorTabla gestorTabla = null;
        private bool flag = false;

        public Form1()
        {
            InitializeComponent();

            gestorDatos = new GestorDatos();
            gestorDatos.CargarMatrices();
        }

        private void btn_comenzar_Click(object sender, EventArgs e)
        {
            if (ValidarTextBox()) return; //return solo corta el metodo

            if (flag == true)
            {
                gestorDatos.CargarDatos(this.cantSimulaciones, (int)desde);
                gestorTabla = new GestorTabla(this.dataGridView1);
                //gestorTabla.CompletarTabla(desde - 1, hasta - 1, gestorDatos.GetDatos());
                gestorTabla.CompletarTabla(gestorDatos.GetDatos());
                flag = false;
               
            }
            else
            {
                gestorTabla.CompletarTabla(gestorDatos.GetDatos());
            }
            
        }

        private void SoloNumeros(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LeerTextBoxSimulaciones()
        {
            this.cantSimulaciones = uint.Parse(this.txt_simulaciones.Text);
        }

        private void LeerTextBoxDesdeHasta()
        {
            this.desde = uint.Parse(this.txt_desde.Text);
        }

        private void txt_simulaciones_TextChanged(object sender, EventArgs e)
        {
            flag = true;
        }

        private void txt_desde_TextChanged(object sender, EventArgs e)
        {
            flag = true;
        }

        private bool ValidarTextBox()
        {
            if (this.txt_simulaciones.Text == "")
            {
                MessageBox.Show("No cargo la cantidad de simulaciones a realizar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            this.LeerTextBoxSimulaciones();


            if (this.txt_desde.Text == "")
            {
                if (cantSimulaciones <= 100)
                {
                    desde = 1;
                }    
                else
                {
                    desde = cantSimulaciones - 100;
                }                
            }
            else
            {
                LeerTextBoxDesdeHasta();

                if (desde > cantSimulaciones)
                {
                    MessageBox.Show("Ingrese rango válido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
            return false;
        }    
    }
}
