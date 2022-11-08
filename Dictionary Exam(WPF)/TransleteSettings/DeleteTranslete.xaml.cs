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
    /// Логика взаимодействия для DeleteTranslete.xaml
    /// </summary>
    public partial class DeleteTranslete : Window
    {
        public string[] temp;
        public List<Words> words = null;
        public DeleteTranslete(List<Words> words)
        {
            this.words = words;
            InitializeComponent();
        }

        private void DeleteTranslet_Click(object sender, RoutedEventArgs e)
        {
            if (TransleteCombo.Text.Contains("Press"))
                MessageBox.Show("Выберите слово.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                words[WordCombo.SelectedIndex].Translations.Remove(TransleteCombo.SelectedIndex, temp[TransleteCombo.SelectedIndex].Count());
                words[WordCombo.SelectedIndex].DeleteTranslete(TransleteCombo.SelectedIndex, temp[TransleteCombo.SelectedIndex].Count(),TransleteCombo.SelectedItem.ToString());
                MessageBox.Show("Отлично.", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BackTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                if (words[WordCombo.SelectedIndex].Translations.Contains(","))
                {

                    string text = words[WordCombo.SelectedIndex].Translations;
                    temp = text.Split(' ');
                    for(int i = 0; i < temp.Length; i++)
                    {
                        TransleteCombo.Items.Add(temp[i]);
                    }          
                }
                else
                {
                    MessageBox.Show("Вы не можете удалить последний перевод слова.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
           
            }   
        }
    }
}
