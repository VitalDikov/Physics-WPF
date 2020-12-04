using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Planets3
{


    class Object
    {       

        public double Rad;

        public bool IsStatic;
        public double Mass { get; }
        public MyVector Position { get; set; }
        public MyVector Velocity { get; set; }

        public Ellipse ellipse = new Ellipse();

        public Polyline track = new Polyline();

        public SolidColorBrush Col = new SolidColorBrush();
        public Object(double mass, double PosX, double PosY, double VelX, double VelY, double R, bool st, bool Red, bool Green)
        {

            if(Red)
                Col = new SolidColorBrush(Colors.Red);
            else if (Green)
                Col = new SolidColorBrush(Colors.Green);
            else
                Col = new SolidColorBrush(Colors.Blue);


            if (st)
            {
                this.Rad = 10;
                this.Col = new SolidColorBrush(Colors.Yellow); 
            }
            else
            {
                this.Rad = R;
            }

            this.Mass = mass;
            this.Position = new MyVector(PosX, PosY);
            this.Velocity = new MyVector(VelX, VelY);
            this.IsStatic = st;

            ellipse.Margin = new Thickness(PosX-Rad, PosY-Rad, 0, 0);
            ellipse.Height = 2 * Rad;
            ellipse.Width = 2 * Rad;
            ellipse.Fill = Col;
            ellipse.Opacity = 1;

            track.Points = new PointCollection();
            track.Points.Add(new Point(PosX-R, PosY-R));
            track.Stroke = Col;

        }

        public void move()
        {
                this.Position += 0.3*this.Velocity;
        }

        public void DrawCurcle()
        {
            track.Points.Add(new Point(Position.X-Rad, Position.Y-Rad));
            ellipse.Margin = new Thickness(Position.X-Rad, Position.Y-Rad, 0, 0);
        }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int NonStatic = 0;

        public int dt = 20;

        public const double G = 20;
        List<Object> Objects = new List<Object>();

        public MainWindow()
        {
            InitializeComponent();

        }





        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {


           Thread myThread1 = new Thread(new ThreadStart(CountForces));
           myThread1.Start();

            

            //  MessageBox.Show("");

        }


        public void Draw()
        {
            for (int i = 0; i < Objects.Count(); i++)
            {
                Objects[i].DrawCurcle();
            }
        }
        public void CountForces()
        {
            while (true) { 
                for (int i = 0; i < Objects.Count(); i++)
                {
                    for (int j = 0; j < Objects.Count(); j++)
                    {
                        if ((i != j) && (!Objects[i].IsStatic))
                        {
                            if (Objects[i].Position * Objects[j].Position != 0)
                                Objects[i].Velocity = Objects[i].Velocity + G * Objects[j].Mass / Math.Pow((Objects[i].Position * Objects[j].Position), 1.5) * (Objects[j].Position - Objects[i].Position);
                        }
                    }
                }


                for (int i = 0; i < Objects.Count(); i++)
                {
                    Objects[i].move();
                }

                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    Draw();
                }
                );
                Thread.Sleep(1);
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            if(!Convert.ToBoolean(IsStatic.IsChecked))
                ++NonStatic;
            Objects.Add(new Object(Convert.ToDouble(NewMass.Text),
                                  Convert.ToDouble(NewPosX.Text) + this.Width/2,
                                  Convert.ToDouble(NewPosY.Text) + this.Height/2,
                                  Convert.ToDouble(NewVelX.Text),
                                  Convert.ToDouble(NewVelY.Text),
                                  Convert.ToDouble(NewRad.Text),
                                  Convert.ToBoolean(IsStatic.IsChecked),
                                  NonStatic % 3 == 1,
                                  NonStatic % 3 == 2
                                  )) ;

            grid.Children.Add(Objects[Objects.Count - 1].ellipse);
            grid.Children.Add(Objects[Objects.Count - 1].track);



            IsStatic.IsChecked = false;

        }

        private void IsStatic_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          //  dt = 100 - (int)Slider1.Value*10;
        }
    }
}