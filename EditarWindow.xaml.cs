using Atividade1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Atividade1
{
    /// <summary>
    /// Lógica interna para EditarWindow.xaml
    /// </summary>
    public partial class EditarWindow : Window
    {
        public EditarWindow()
        {
            InitializeComponent();
        }

        public void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
