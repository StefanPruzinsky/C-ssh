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
using System.Windows.Threading;

namespace C_sshRemote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer DispatcherTimer { get; private set; }
        public int NumberOfTicks { get; private set; }

        private ConnectionManager ConnectionManager;

        public MainWindow()
        {
            InitializeComponent();

            //Database = new Database(@"Server=tcp:csshmain.database.windows.net,1433;Database=csshmain;User ID=csshmain@csshmain;Password={your_password_here};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += DispatcherTimer_Tick;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            NumberOfTicks = 0;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            string response = "";
            NumberOfTicks++;

            if ((response = ConnectionManager.GetResponse()) != null)
            {
                DispatcherTimer.Stop();
                NumberOfTicks = 0;

                WriteToRichTextBox(response);

                commandBox.Text = String.Format("pi@raspberrypi:~/{0} $ ", String.Join("/", ConnectionManager.TerminalPath.ToArray()));
                commandBox.IsEnabled = true;
                commandBox.CaretIndex = commandBox.Text.Length;
            }
            else if (NumberOfTicks > 60)
            {
                MessageBox.Show("Časový limit pretiekol, server nedpovedá.");

                commandBox.Text = String.Format("pi@raspberrypi:~/{0} $ ", String.Join("/", ConnectionManager.TerminalPath.ToArray()));
                commandBox.IsEnabled = true;
                commandBox.CaretIndex = commandBox.Text.Length;

                WriteToRichTextBox("Časový limit pretiekol, server nedpovedá.");

                DispatcherTimer.Stop();
                NumberOfTicks = 0;
            }
        }

        private void commandBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (ConnectionManager != null)
                {
                    WriteToRichTextBox(String.Format("pi@raspberrypi:~{0} $ {1}", String.Join("/", ConnectionManager.TerminalPath.ToArray()), commandBox.Text.Substring(commandBox.Text.IndexOf('$') + 2)));
                    commandBox.IsEnabled = false;

                    ConnectionManager.SendRequest(commandBox.Text.Substring(commandBox.Text.IndexOf('$') + 2));
                    DispatcherTimer.Start();
                }
                else
                {
                    MessageBox.Show("Nie ste pripojený k serveru!");
                    commandBox.Text = "pi@raspberrypi:~ $ ";
                }
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginToServer();
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginToServer();
        }

        /*private void richTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EditingCommands.Backspace.Execute(null, richTextBox);
                flowDocumentInsideRTB.Blocks.Add(new Paragraph(new Run("pi@raspberrypi:~ $ ")));
                richTextBox.CaretPosition = richTextBox.CaretPosition.DocumentEnd;
                EditingCommands.Backspace.Execute(null, richTextBox);
            }
        }*/

        private void WriteToRichTextBox(string text)
        {
            Paragraph paragraph = new Paragraph(new Run(text));
            paragraph.Margin = new Thickness(0, 0, 0, 0);

            flowDocumentInsideRTB.Blocks.Add(paragraph);
            //paragraphInsideRichTextBox.Inlines.Add(new Run(text));

            richTextBox.ScrollToEnd();
        }

        private void LoginToServer()
        {
            try
            {
                ConnectionManager = new ConnectionManager(String.Format(@"Server=tcp:csshmain.database.windows.net,1433;Database=csshmain;User ID={0}@csshmain;Password={1};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", adminLogin.Text, password.Password));

                MessageBox.Show("Prihlásenie bolo úspešné. :)");
                commandBox.Text = "pi@raspberrypi:~ $ ";
                commandBox.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Problém s pripojením. :( Prosím zopakujte prihlásenie.");
            }
        }
    }
}
