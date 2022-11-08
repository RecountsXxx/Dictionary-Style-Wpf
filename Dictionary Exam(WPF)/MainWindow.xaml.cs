using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Dictionary_Exam_WPF_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MenuDictionary menuDictionary = new MenuDictionary();


        public ObservableCollection<Dictionary> dictionary = null;

        public string nameDictionaryText;

        public MainWindow()
        {
            InitializeComponent();

        }
        private void ButtonAddDictionary_Click(object sender, RoutedEventArgs e)
        {
             nameDictionaryText = OneDictionary.Text + "-" + TwoDictionary.Text; // это соедение двух комбо боксов

            if (nameDictionaryText.Contains("Press"))
                MessageBox.Show("Выберите нужный вам словарь", "Erorr", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                //Regex regex = new Regex("^[A-Za-zА-Яа-я]*[-]+[a-zа-я]+$");
                if (OneDictionary.Text != TwoDictionary.Text)
                {
                    if (listBoxDictionary.Items.Contains(nameDictionaryText))
                        MessageBox.Show("Такой словарь уже существует.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        listBoxDictionary.Items.Clear();
                        if(dictionary == null)
                        {
                            dictionary = new ObservableCollection<Dictionary>();
                        }
                        dictionary.Add(new Dictionary(nameDictionaryText));
                        foreach (var item in dictionary)
                        {
                            listBoxDictionary.Items.Add(item.nameDictionary);
                        }

                    }
                }
                else
                    MessageBox.Show("Зачем вам словарь на одном и том же языке?.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private new void Loaded(object sender, RoutedEventArgs e)
        {
            string temp;
            try
            {
                listBoxDictionary.Items.Clear();
                using (StreamReader fstream = new StreamReader("ListDictionary.txt", true))
                {
                    while (fstream.Peek() != -1)
                    {
                        temp = fstream.ReadLine();
                        if(dictionary == null)
                        {
                            dictionary = new ObservableCollection<Dictionary>();
                        }
                        dictionary.Add(new Dictionary(temp));
                        
                    }
                }
                if(dictionary != null)
                {
                    foreach (var item in dictionary)
                    {
                        listBoxDictionary.Items.Add(item.nameDictionary);
                    }
                }
            }
            catch
            {
                FileStream file = new FileStream("ListDictionary.txt", FileMode.Create);
            }
        }
        private void listBoxDictionary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hide();
            dictionary[listBoxDictionary.SelectedIndex].menu(nameDictionaryText);
            Show();
        }

        private void listBoxDictionary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                string path = listBoxDictionary.SelectedItem.ToString() + "-Dictionary.txt";
                if (File.Exists(path))
                File.Delete(path);
                int pos = listBoxDictionary.SelectedIndex;
                dictionary.RemoveAt(pos);          
                listBoxDictionary.Items.Clear();
                foreach (var item in dictionary)
                {
                    listBoxDictionary.Items.Add(item.nameDictionary);
                }
                if(dictionary.Count == 0)
                {
                    dictionary = null;
                }


            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(dictionary != null)
            {
                using (StreamWriter fstream = new StreamWriter("ListDictionary.txt", false))
                {
                    foreach (var item in dictionary)
                    {
                        fstream.WriteLine(item.nameDictionary);
                    }
                }
            }
            Environment.Exit(0);
        }

        private void EditDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (dictionary != null)
            {
                RenameDictionary rename = new RenameDictionary(dictionary);
                Hide();
                rename.ShowDialog();
                Show();
                listBoxDictionary.Items.Clear();
                dictionary = rename.dictionary;
                foreach(var item in dictionary)
                {
                    listBoxDictionary.Items.Add(item.nameDictionary);
                }

            }
            else
                MessageBox.Show("Словарей ещё нету, что вы пытаетесь изменить?", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show("'Del' - Удалить словарь\nДля замены имени словаря есть кнопка 'Edit dictionary name'\nДля загрузки списка из файла есть кнопка 'Manage' и выберите нужную вам операцию\nЧто бь изменить имя словаря, сверху есть кнопка 'Edit dictionary name' нажмите её и задайте новое значение словарю ","Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LoadList_Click(object sender, RoutedEventArgs e)
        {
             OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader fs = new StreamReader(openFileDialog.FileName))
                {
                    while (true)
                    {
                        string text = fs.ReadLine();

                        if (text == null) break;

                        dictionary.Add(new Dictionary(text));
                        listBoxDictionary.Items.Add(text);

                    }

                   
                }            
                MessageBox.Show("Отлично", "Super", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Произошла непревидинная ошибка", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);


        }
        private void LoadWords_Click(object sender, RoutedEventArgs e)
        {
           if(dictionary != null)
            {
                LoadWords ld = new LoadWords(dictionary);
                ld.ShowDialog();
            }
            else
            {
                MessageBox.Show("У вас ещё нету словарей.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ManageItemMenu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
