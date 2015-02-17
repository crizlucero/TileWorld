using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TW.Agente
{
    class Agente
    {
        private Grid edoInicial { get; set; }
        private Grid edoActual { get; set; }
        private int[] SelBloque = new int[2];

        public Agente(Grid Entorno)
        {
            this.edoInicial = Entorno;
            this.edoActual = Entorno;
        }

        private void BuscarBloqueCercano()
        {
            for (int i = 4; i < 8; i++)
            {
            }
        }
        /// <summary>
        /// Mueve el agente de un lado a otro
        /// </summary>
        /// <param name="Entorno">Entorno de donde está el agente</param>
        /// <returns>Si es posible moverse</returns>
        public void MoverAgente(Grid Entorno)
        {
            this.edoActual = Entorno;

            int x = Grid.GetColumn(this.edoActual.Children[0]);
            int y = Grid.GetRow(this.edoActual.Children[0]);
            switch (this.SeleccionarMovimiento())
            {
                case 1: this.MoverArriba(x, --y); break;
                case 2: this.MoverAbajo(x, ++y); break;
                case 3: this.MoverIzquierda(--x, y); break;
                case 4: this.MoverDerecha(++x, y); break;
            }
            return;
            //Reglas
            //Arriba
            if (y != 0)
            {
                this.MoverArriba(x, --y);
            }
            //Abajo
            else if (y != 7)
            {
                this.MoverAbajo(x, ++y);
            }
            //Izquierda
            else if (x != 0)
            {
                this.MoverIzquierda(--x, y);
            }
            //Derecha
            else if (x != 7)
            {
                this.MoverDerecha(++x, y);
            }
        }

        private void SeleccionarBloque()
        {
            int ax = Grid.GetColumn(this.edoActual.Children[0]);
            int ay = Grid.GetRow(this.edoActual.Children[0]);

            int bx = int.MaxValue;
            int by = int.MaxValue;

            foreach (var bloque in this.edoActual.Children)
            {
                if (bloque is Rectangle)
                {
                    Rectangle block = (Rectangle)bloque;
                    if (block.Fill.ToString() == Colors.Gray.ToString())
                    {
                        if (Math.Abs((Grid.GetColumn(block) + Grid.GetRow(block)) - (ax + ay)) < Math.Abs((bx + by) - (ax + ay)))
                        {
                            bx = Grid.GetColumn(block);
                            by = Grid.GetRow(block);
                            this.SelBloque[0] = bx;
                            this.SelBloque[1] = by;
                        }
                    }
                }
            }
        }

        private int SeleccionarMovimiento()
        {
            this.SeleccionarBloque();
            int ax = Grid.GetColumn(this.edoActual.Children[0]);
            int ay = Grid.GetRow(this.edoActual.Children[0]);
            //Movimientos horizontales
            if ((Math.Abs(this.SelBloque[0] - ax) < Math.Abs(this.SelBloque[1] - ay)) && this.SelBloque[0] - ax != 0)
            {
                //Mover a la derecha
                if (this.SelBloque[0] - ax > 0)
                {
                    if (ax != 7) return 4;
                    else if (ay != 0) return 2;
                    else if (ay != 7) return 1;
                    else return 3;
                }
                else //Mover a la izquierda
                {
                    if (ax != 0) return 3;
                    else if (ay != 0) return 2;
                    else if (ay != 7) return 1;
                    else return 4;
                }
            }
            else //Movimientos verticales
            {
                //Mover para abajo
                if (this.SelBloque[1] - ay > 0)
                {
                    if (ay != 7) return 2;
                    else if (ax != 0) return 3;
                    else if (ax != 7) return 4;
                    return 1;
                }
                else //Mover para arriba
                {
                    if (ay != 0) return 1;
                    else if (ax != 0) return 3;
                    else if (ax != 7) return 4;
                    else return 2;
                }
            }
            return 0;
        }
        /// <summary>
        /// Movimiento hacia arriba del agente.
        /// Si puede mover un bloque, lo hace.
        /// </summary>
        private void MoverArriba(int x, int y)
        {
            var elemento = this.edoActual.Children.Cast<UIElement>().FirstOrDefault(i => Grid.GetColumn(i) == x && Grid.GetRow(i) == y);
            if (elemento is Rectangle)
            {
                if (((Rectangle)elemento).Fill.ToString() == Colors.Gray.ToString())
                {
                    if (Grid.GetRow(elemento) != 0)
                    {
                        //Mover bloque
                        Grid.SetRow(elemento, y);
                        Grid.SetRow(this.edoActual.Children[0], y);
                    }
                }
            }
            else
            {
                //Se moverá el agente
                Grid.SetRow(this.edoActual.Children[0], y);
            }
        }
        /// <summary>
        /// Movimiento hacia abajo del agente.
        /// Si puede mover un bloque, lo hace.
        /// </summary>
        private void MoverAbajo(int x, int y)
        {
            var elemento = this.edoActual.Children.Cast<UIElement>().FirstOrDefault(i => Grid.GetColumn(i) == x && Grid.GetRow(i) == y);
            if (elemento is Rectangle)
            {
                if (((Rectangle)elemento).Fill.ToString() == Colors.Gray.ToString())
                {
                    if (Grid.GetRow(elemento) != 0)
                    {
                        //Mover bloque
                        Grid.SetRow(elemento, y);
                        Grid.SetRow(this.edoActual.Children[0], y);
                    }
                }
            }
            else
            {
                //Se moverá el agente
                Grid.SetRow(this.edoActual.Children[0], y);
            }
        }
        /// <summary>
        /// Movimiento hacia la derecha del agente.
        /// Si puede mover un bloque, lo hace.
        /// </summary>
        private void MoverDerecha(int x, int y)
        {
            var elemento = this.edoActual.Children.Cast<UIElement>().FirstOrDefault(i => Grid.GetColumn(i) == x && Grid.GetRow(i) == y);
            if (elemento is Rectangle)
            {
                if (((Rectangle)elemento).Fill.ToString() == Colors.Gray.ToString())
                {
                    if (Grid.GetColumn(elemento) != 7)
                    {
                        //Mover bloque
                        Grid.SetColumn(elemento, x);
                        Grid.SetColumn(this.edoActual.Children[0], x);
                    }
                }
            }
            else
            {
                //Se moverá el agente
                Grid.SetColumn(this.edoActual.Children[0], x);
            }
        }
        /// <summary>
        /// Movimiento hacia la izquierda del agente.
        /// Si puede mover un bloque, lo hace.
        /// </summary>
        private void MoverIzquierda(int x, int y)
        {
            var elemento = this.edoActual.Children.Cast<UIElement>().FirstOrDefault(i => Grid.GetColumn(i) == x && Grid.GetRow(i) == y);
            if (elemento is Rectangle)
            {
                if (((Rectangle)elemento).Fill.ToString() == Colors.Gray.ToString())
                {
                    if (Grid.GetColumn(elemento) != 7)
                    {
                        //Mover bloque
                        Grid.SetColumn(elemento, x);
                        Grid.SetColumn(this.edoActual.Children[0], x);
                    }
                }
            }
            else
            {
                //Se moverá el agente
                Grid.SetColumn(this.edoActual.Children[0], x);
            }
        }
    }
}
