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
using System.Windows.Shapes;

namespace Dictionary_Exam_WPF_
{
    /// <summary>
    /// Логика взаимодействия для DeleteWord.xaml
    /// </summary>
    public partial class DeleteWord : Window
    {
        public List<Words> words = new List<Words>();
        public DeleteWord(List<Words> words)
        {
            this.words = words;


            InitializeComponent();
        }
        private new void Loaded(object sender, RoutedEventArgs e)
        {

            foreach (var item in words)
            {
                comboBox.Items.Add(item.Word);
            }

        }

        private void AddTranslate_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
                words.Remove(words[comboBox.SelectedIndex]);
        }


        private void BackTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
