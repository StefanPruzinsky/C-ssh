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
using DbORM;
using System.Windows.Threading;

namespace C_sshRemote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer DispatcherTimer { get; private set; }
        public Database Database { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void commandBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show(commandBox.Text);
                commandBox.Text = "pi@raspberrypi:~ $ ";
                commandBox.CaretIndex = commandBox.Text.Length;
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            Database = new Database(String.Format(@"Server=tcp:csshmain.database.windows.net,1433;Database=csshmain;User ID={0}@csshmain;Password={1};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", adminLogin.Text, password.Password));
        }
    }
}
