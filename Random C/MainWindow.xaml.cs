using SimpleTCP;
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

namespace Random_C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SimpleTcpClient client = new SimpleTcpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client.DataReceived += (sender, e) =>
            {
                var msg = Encoding.UTF8.GetString(e.Data);

                if (msg.Contains("-1"))
                {
                    try
                    {
                        MessageBox.Show("Disconnected");
                        client.Disconnect();
                    }
                    catch { }
                }
                else
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        TXT.Text += msg + "\n\n";
                    }));
                  
                }
               
            };
            try
            {
                client.Connect("127.0.0.1", 5000);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
          
        }

        private void Button_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                client.Write("1");
            }
            catch { }
            
        }

        private void Button_GotMouseCapture_1(object sender, MouseEventArgs e)
        {
            try
            {
                client.Write("disconnect");
            }
            catch { }
            
        }

        private void Connect_GotMouseCapture(object sender, MouseEventArgs e)
        {
            try
            {
                client.Connect("127.0.0.1", 5000);
            }
            catch { }
        }
    }
}
