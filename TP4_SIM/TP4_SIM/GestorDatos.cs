using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_SIM
{
    class GestorDatos
    {
        private int[,] demandaXSemana;
        private double[,] tiempoEntregaXSemana;
        private int[,] bicicletasDañadas;
        private double[,] matrizDatos;
        private Random random = new Random();

        public GestorDatos()
        {
            this.demandaXSemana = new int[4, 3];
            this.tiempoEntregaXSemana = new double[3, 3];
            this.bicicletasDañadas = new int[2, 3];           
        }

        //public void CargarDatos(uint cantSimulaciones)
        //{
        //    this.matrizDatos = new double[cantSimulaciones, 13];
        //    uint filas = (uint) this.matrizDatos.GetLength(0);

        //    this.InicializarPrimerFila();

        //    for (int i = 1; i < cantSimulaciones; i++)
        //    {
                
        //        this.matrizDatos[i, 0] = i + 1;
        //        this.matrizDatos[i, 1] = LogicaStock(i, (int)matrizDatos[i - 1, 5]);
        //        this.matrizDatos[i, 2] = this.GenerarRandom(1, 100, false);
        //        this.matrizDatos[i, 3] = this.CantidadDemanda((int) this.matrizDatos[i, 2]);
        //        this.matrizDatos[i, 4] = this.LogicaRandomTiempoDeEntregaPedido(this.matrizDatos[i, 1], this.matrizDatos[i - 1, 5]);
        //        this.matrizDatos[i, 5] = this.LogicaTiempoDeEntregaPedido(this.matrizDatos[i, 4], (int) this.matrizDatos[i - 1, 5]);
        //        this.matrizDatos[i, 6] = this.LogicaRandomBicicletasRotas(this.matrizDatos[i - 1, 5]);
        //        this.matrizDatos[i, 7] = this.LogicaCantBicicletasRotas(this.matrizDatos[i, 6], i);
        //        this.matrizDatos[i, 8] = matrizDatos[i, 1] * 3;
        //        this.matrizDatos[i, 9] = (matrizDatos[i, 4] == -1) ? 0 : 20;
        //        this.matrizDatos[i, 10] = (matrizDatos[i, 1] - matrizDatos[i, 3] < 0) ? (matrizDatos[i, 1] - matrizDatos[i, 3]) * -1 * 5 : 0;
        //        this.matrizDatos[i, 11] = matrizDatos[i, 8] + matrizDatos[i, 9] + matrizDatos[i, 10];
        //        this.matrizDatos[i, 12] = matrizDatos[i, 11] + matrizDatos[i - 1, 12];
        //    }
        //}

        public void CargarDatos(uint cantSimulaciones, int desde)
        {
            uint hasta = 100;
            double[,] datos = new double[101, 13];
            
            uint nroExp = 1;
            int stock = 7;
            double rnd1 = this.GenerarRandom(1, 100, false); ;
            int cantDemanda = this.CantidadDemanda((int)rnd1); ;
            double rnd2 = -1.0;
            int semamaLlegaPedido = -1;
            double rnd3 = -1.0;
            int cantBicisRotas = 0;
            int costoTenencia = stock * 3;
            int costoPedido = 0;
            int costoAgotamiento = 0;
            int costoTotal = costoTenencia;
            int costoAcumulado = costoTotal;
            int cont = 0;           

            for(int i = 1; i <= cantSimulaciones; i++)
            {

                if (nroExp >= desde && cont <= hasta)
                {
                    if ((cantSimulaciones - desde) < 100)
                    {
                        hasta = (uint)(cantSimulaciones - desde);
                    }
                    datos[cont, 0] = nroExp;
                    datos[cont, 1] = stock;
                    datos[cont, 2] = rnd1;
                    datos[cont, 3] = cantDemanda;
                    datos[cont, 4] = rnd2;
                    datos[cont, 5] = semamaLlegaPedido;
                    datos[cont, 6] = rnd3;
                    datos[cont, 7] = cantBicisRotas;
                    datos[cont, 8] = costoTenencia;
                    datos[cont, 9] = costoPedido;
                    datos[cont, 10] = costoAgotamiento;
                    datos[cont, 11] = costoTotal;
                    datos[cont, 12] = costoAcumulado;
                 
                    cont++;
                }

                if (i != cantSimulaciones)
                {
                    nroExp = (uint)(i + 1);
                    stock = LogicaStock(stock, cantDemanda, semamaLlegaPedido);
                    rnd1 = this.GenerarRandom(1, 100, false);
                    cantDemanda = this.CantidadDemanda((int)rnd1);
                    rnd2 = LogicaRandomTiempoDeEntregaPedido(stock, semamaLlegaPedido);
                    semamaLlegaPedido = (int)this.LogicaTiempoDeEntregaPedido(rnd2, semamaLlegaPedido);
                    rnd3 = this.LogicaRandomBicicletasRotas(semamaLlegaPedido);
                    cantBicisRotas = (int)this.LogicaCantBicicletasRotas(rnd3, ref stock);
                    costoTenencia = (stock - cantDemanda < 0) ? 0 : (stock - cantDemanda) * 3;
                    costoPedido = (rnd2 == -1) ? 0 : 20;
                    costoAgotamiento = (stock - cantDemanda < 0) ? (stock - cantDemanda) * -1 * 5 : 0;
                    costoTotal = costoTenencia + costoPedido + costoAgotamiento;
                    costoAcumulado += costoTotal;
                }                         
            }
            this.matrizDatos = datos;
        }

        private double LogicaCantBicicletasRotas(double random, ref int stock)
        {
            if (random < 70) return 0;
            else
            {
                stock--;
                return 1;
            }
        }

        //private double LogicaCantBicicletasRotas(double random, int posicion)
        //{
        //    if (random < 70) return 0;
        //    else
        //    {
        //        this.matrizDatos[posicion, 1]--;
        //        return 1;
        //    }
        //}

        private double LogicaRandomBicicletasRotas(double semanaPedido)
        {
            if (semanaPedido == 0) return GenerarRandom(1, 100, false);
            else return -1;
        }

        private double LogicaRandomTiempoDeEntregaPedido(double stock,double cantSemanas)
        {
            double retorno = 0.0;

            if (cantSemanas != -1) return -1;

            if (stock <= 2) retorno = GenerarRandom(1, 9999, true);
            else retorno = -1;
            

            return retorno;
        }

        private double LogicaTiempoDeEntregaPedido(double random, int valorAnterior)
        {
            double retorno = -1;

            if (valorAnterior > 0) retorno = valorAnterior - 1;
            else
            {
                if (valorAnterior == 0 && random == -1)
                {
                    retorno = -1;
                }
                else if (valorAnterior == -1 && random != -1) retorno = TiempoDeEntregaPedido(random);
            }
            return retorno;
        }

        private int TiempoDeEntregaPedido(double random)
        {
            if (random < this.tiempoEntregaXSemana[0, 2]) return 1;
            else if (random < this.tiempoEntregaXSemana[1, 2]) return 2;
            else return 3;
        }

        private int LogicaStock(int stock, int cantDemanda,int semanasPedido)
        {
            if (semanasPedido == 1) return 6;
            if (stock - cantDemanda < 0)
            {
                return 0;
            }
            else
            {
                return stock - cantDemanda;
            }
        }

        //private double LogicaStock(int posision, int semanasPedido)
        //{ 
        //    if (semanasPedido == 1) return 6;          
        //    if (this.matrizDatos[posision - 1, 1] - this.matrizDatos[posision - 1, 3] < 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return this.matrizDatos[posision - 1, 1] - this.matrizDatos[posision - 1, 3];
        //    }
        //}

        //private void InicializarPrimerFila()
        //{
        //    this.matrizDatos[0, 0] = 0 + 1;
        //    this.matrizDatos[0, 1] = 7;
        //    this.matrizDatos[0, 2] = this.GenerarRandom(1, 100, false);
        //    this.matrizDatos[0, 3] = this.CantidadDemanda((int)this.matrizDatos[0, 2]);
        //    this.matrizDatos[0, 4] = -1;
        //    this.matrizDatos[0, 5] = -1;
        //    this.matrizDatos[0, 8] = matrizDatos[0, 1] * 3;
        //    this.matrizDatos[0, 9] = (matrizDatos[0, 4] == -1) ? 0 : 20;
        //    this.matrizDatos[0, 10] = (matrizDatos[0, 1] - matrizDatos[0, 3] < 0) ? (matrizDatos[0, 1] - matrizDatos[0, 3]) * -1 * 5 : 0;
        //    this.matrizDatos[0, 11] = matrizDatos[0, 8] + matrizDatos[0, 9] + matrizDatos[0, 10];
        //    this.matrizDatos[0, 12] = matrizDatos[0, 11];
        //}
        private double GenerarRandom(int desde, int hasta, bool conDecimales)
        {
            if (conDecimales == false) return this.random.Next(desde, hasta);
            else
            {
                return (double)this.random.Next(desde, hasta) / (double)10000; ;
            }
        }

        //private Array ResizeArray(Array arr, uint filas, uint columnas)
        //{
        //    double[,] temp = new double[filas, columnas];
        //    long length = arr.Length <= temp.Length ? arr.Length : temp.Length;
        //    Array.Copy(arr, 0, temp, 0, length);
        //    return temp;
        //}

        public int CantidadDemanda(int random)
        {
            if (random < this.demandaXSemana[0, 2]) return 0;
            else if (random < this.demandaXSemana[1, 2]) return 1;
            else if (random < this.demandaXSemana[2, 2]) return 2;
            else return 3;

        }

        public double[,] GetDatos()
        {
            return matrizDatos;
        }

        ///<summary>
        ///Carga las tablas con sus probabilidades.
        ///</summary>     
        public void CargarMatrices()
        {
            this.demandaXSemana[0, 0] = 0;
            this.demandaXSemana[0, 1] = 50;
            this.demandaXSemana[0, 2] = 50;

            this.demandaXSemana[1, 0] = 1;
            this.demandaXSemana[1, 1] = 15;
            this.demandaXSemana[1, 2] = 65;

            this.demandaXSemana[2, 0] = 2;
            this.demandaXSemana[2, 1] = 25;
            this.demandaXSemana[2, 2] = 90;

            this.demandaXSemana[3, 0] = 3;
            this.demandaXSemana[3, 1] = 10;
            this.demandaXSemana[3, 2] = 100;

            this.tiempoEntregaXSemana[0, 0] = 1;
            this.tiempoEntregaXSemana[0, 1] = 0.3;
            this.tiempoEntregaXSemana[0, 2] = 0.3;

            this.tiempoEntregaXSemana[1, 0] = 2;
            this.tiempoEntregaXSemana[1, 1] = 0.4;
            this.tiempoEntregaXSemana[1, 2] = 0.7;

            this.tiempoEntregaXSemana[2, 0] = 3;
            this.tiempoEntregaXSemana[2, 1] = 0.3;
            this.tiempoEntregaXSemana[2, 2] = 1.0;

            this.bicicletasDañadas[0, 0] = 0;
            this.bicicletasDañadas[0, 1] = 70;
            this.bicicletasDañadas[0, 2] = 70;

            this.bicicletasDañadas[1, 0] = 3;
            this.bicicletasDañadas[1, 1] = 30;
            this.bicicletasDañadas[1, 2] = 100;
        }
    }
}
