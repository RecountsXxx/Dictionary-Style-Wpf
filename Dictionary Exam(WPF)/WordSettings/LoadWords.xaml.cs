using Microsoft.Win32;
using System;
using System.Collections;
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
using MessageBox = System.Windows.MessageBox;

namespace Dictionary_Exam_WPF_
{
    /// <summary>
    /// Логика взаимодействия для LoadWords.xaml
    /// </summary>
    public partial class LoadWords : Window
    {
        ObservableCollection<Dictionary> dictionaries = new ObservableCollection<Dictionary>();
        public LoadWords(ObservableCollection<Dictionary> dictionaries)
        {
            this.dictionaries = dictionaries;
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox.Text.Contains("Press"))
                MessageBox.Show("Выберите словарь.", "Eror", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                string text = null;
                string path = comboBox.Text + "-Dictionary.txt";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
    
                if (openFileDialog.ShowDialog() == true)
                {
                    text = File.ReadAllText(openFileDialog.FileName);
                            using (StreamWriter fstream = new StreamWriter(path, true))
                            {
                                fstream.WriteLine(text);
                                MessageBox.Show("Отлично", "Super", MessageBoxButton.OK, MessageBoxImage.Information);
                                fstream.Close();
                            }            
                }
                else
                    MessageBox.Show("Произошла непревидинная ошибка", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackTo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private new void Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in dictionaries)
            {
                comboBox.Items.Add(item.nameDictionary);
            }
        }
    }
}
