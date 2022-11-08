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
    /// Логика взаимодействия для EditTranslete.xaml
    /// </summary>
    public partial class EditTranslete : Window
    {

        public string[] temp;
        public List<Words> words = null;
        public EditTranslete(List<Words> words)
        {
            this.words = words;
            InitializeComponent();
        }





        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in words)
            {
                WordCombo.Items.Add(item.Word);

            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            TransleteCombo.Items.Clear();
            if (WordCombo.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            { 
                    string text = words[WordCombo.SelectedIndex].Translations;
                    temp = text.Split(' ');
                    for (int i = 0; i < temp.Length; i++)
                    {
                        TransleteCombo.Items.Add(temp[i]);
                    }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (WordCombo.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                string text = words[WordCombo.SelectedIndex].Translations;
                temp = text.Split(' ');
                for (int i = 0; i < temp.Length; i++)
                {
                    TransleteCombo.Items.Add(temp[i]);
                }
            }
        }

        private void EditTranslete_Click(object sender, RoutedEventArgs e)
        {
            if (TransleteCombo.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                string temp = TransleteCombo.SelectedItem.ToString();
                words[WordCombo.SelectedIndex].EditTranslete(editTransleteTextBox.Text,temp);
                MessageBox.Show("Отлично.", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

     
    }
}
