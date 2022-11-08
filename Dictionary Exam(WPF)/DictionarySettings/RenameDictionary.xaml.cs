using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для RenameDictionary.xaml
    /// </summary>
    public partial class RenameDictionary : Window
    {
        public ObservableCollection<Dictionary> dictionary = new ObservableCollection<Dictionary>();
        public RenameDictionary(ObservableCollection<Dictionary> dictionary)
        {
            this.dictionary = dictionary;
            InitializeComponent();
        }

        private void BackTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RenameButton_Click(object sender, RoutedEventArgs e)
        {
            bool temp = false;
            bool temptwo = false;
            if (comboBox.Text.Contains("Press"))
                MessageBox.Show("Выберите словарь.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);    
            else
            {
                foreach(var item in dictionary)
                {
                    if (item.nameDictionary.Contains(TwoDictionary.Text))
                    {
                     
                        temp = true;
                        temptwo = true;
                        
                    }
                }
                if (temptwo == true)
                {
                    MessageBox.Show("Зачем вам одинаковые словари?", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                if (temp != true)
                {
                    if (TwoDictionary.Text.Length >= 2)
                    {
                        int index = comboBox.SelectedIndex;
                        dictionary.RemoveAt(index);
                        dictionary.Insert(index, new Dictionary(TwoDictionary.Text));
                        if (File.Exists(comboBox.Text + "-Dictionary.txt"))
                            File.Move(comboBox.Text + "-Dictionary.txt", TwoDictionary.Text + "-Dictionary.txt");
                        MessageBox.Show("Все пройшло успешно.", "Nice", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();

                    }
                    else
                        MessageBox.Show("Разве такие словари существуют?", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                    Close();
      
            }

        }
        private new void  Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in dictionary)
            {
                comboBox.Items.Add(item.nameDictionary);
            }
        }

    }
}
