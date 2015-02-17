using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TW
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int[]> posiciones = new List<int[]>();
        private Agente.Agente Jugador;

        public MainWindow()
        {
            InitializeComponent();
            this.AddAgente();
            this.AddElementos(Colors.Black, 5);
            this.AddElementos(Colors.Gray, 5);
            this.AddElementos(Colors.LightGray, 10);

            /*this.Jugador = new Agente.Agente(Entorno);
            if (!this.Jugador.ValidarEntorno())
            {
                MessageBox.Show("El entorno no es viable para funcionar");
                Application.Current.Shutdown();
            }*/
            this.Jugador = new Agente.Agente(Entorno);
        }

        public void AddAgente()
        {
            Agente.Width = 25;
            Agente.Height = 25;
            Random rand = new Random();

            int[] xy = new int[2];
            xy[0] = rand.Next(0, 8);
            xy[1] = rand.Next(0, 8);
            Grid.SetRow(Agente, xy[0]);
            Grid.SetColumn(Agente, xy[1]);
            this.posiciones.Add(xy);
        }
        /// <summary>
        /// Agrega los elementos de pared, bloque y agujeros al entorno
        /// </summary>
        /// <param name="color">Color del elemento</param>
        /// <param name="cantidad">Cantidad de elementos que se generarán</param>
        public void AddElementos(Color color, int cantidad)
        {
            Random rand = new Random();
            int total = rand.Next(1, cantidad);
            int x = rand.Next(0, 8);
            int y = rand.Next(0, 8);
            for (int i = 0; i < total; i++)
            {
                Rectangle r = new Rectangle
                {
                    Fill = new SolidColorBrush(color)
                };
                var valor = this.Entorno.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);
                if (!(valor is Rectangle) && !(valor is Image))
                {
                    this.Entorno.Children.Add(r);
                    r.SetValue(Grid.ColumnProperty, x);
                    r.SetValue(Grid.RowProperty, y);
                }
                else
                {
                    i--;
                }
                x = rand.Next(0, 8);
                y = rand.Next(0, 8);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Jugador.MoverAgente(Entorno);
        }
    }
}