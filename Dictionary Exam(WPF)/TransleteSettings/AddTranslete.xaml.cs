using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Dictionary_Exam_WPF_
{
    /// <summary>
    /// Логика взаимодействия для AddTranslete.xaml
    /// </summary>
    public partial class AddTranslete : Window
    {
        public List<Words> words = new List<Words>();
        public AddTranslete(List<Words> words)
        {
            this.words = words;
            
            
            InitializeComponent();
        }
        private new void Loaded(object sender, RoutedEventArgs e)
        {

            foreach(var item in words)
            {
                comboBox.Items.Add(item.Word);
            }
            
        }

        private void AddTranslate_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                if(TwoTranslate.Text.Length > 0)
                {
                    words[comboBox.SelectedIndex].AddTranslete(TwoTranslate.Text);
                    MessageBox.Show("Отлично!", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                    MessageBox.Show("Введите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void BackTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
