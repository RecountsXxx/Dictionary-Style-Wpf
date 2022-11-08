using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Speech.Synthesis;
using System.ComponentModel;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Threading;

namespace Dictionary_Exam_WPF_
{
    /// <summary>
    /// Логика взаимодействия для MenuDictionary.xaml
    /// </summary>
    ///  

    public partial class MenuDictionary : Window
    {
        public string nameDictionary;
        public List<Words> words { get; set; } = null;
        public int countWords = 0;
        public bool checkSave = false;


        public bool TestForNullOrEmpty(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }
        public MenuDictionary()
        {
            InitializeComponent();
        }
        public MenuDictionary(string nameDictionary)
        {
            this.nameDictionary = nameDictionary;
            InitializeComponent();

        }
        private void ButtonAddWord_Click(object sender, RoutedEventArgs e)
        {
            if (TestForNullOrEmpty(WordTextBox.Text) || TestForNullOrEmpty(TranscriptionTextBox.Text) || TestForNullOrEmpty(TranslationTextBox.Text))
                MessageBox.Show("Заполните все поля пожалуйста", "Erorr", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if(words == null)
                {
                    words = new List<Words>();
                    listWords.ItemsSource = words;
                }
                words.Add(new Words(WordTextBox.Text, TranscriptionTextBox.Text, TranslationTextBox.Text));
              
                WordTextBox.Clear();
                TranscriptionTextBox.Clear();
                TranslationTextBox.Clear();
                listWords.Items.Refresh();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {
                using (StreamWriter fstream = new StreamWriter(nameDictionary + "-Dictionary.txt"))
                {
                    if (words != null)
                    {
                        foreach (var item in words)
                        {
                            fstream.WriteLine(item.Word);
                            fstream.WriteLine(item.Transcription);
                            fstream.WriteLine(item.Translations);
                        }
                    }
                    checkSave = true;
                }
            }
            else
                MessageBox.Show("Зачем вам сохранять пустой файл?", "Erorr", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(nameDictionary + "-Dictionary.txt"))
            {

                string word, transcription, translation;
                using (StreamReader fstream = new StreamReader(nameDictionary + "-Dictionary.txt"))
                {
                    while (fstream.Peek() != -1)
                    {

                        word = fstream.ReadLine();
                        transcription = fstream.ReadLine();
                        translation = fstream.ReadLine();
              
                        if(words == null)
                        {
                            words = new List<Words>();
                            listWords.ItemsSource = words;
                        }
                        words.Add(new Words(word, transcription, translation));
                        countWords++;
                    }
                }
                listWords.Items.Refresh();
            }
        }
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SortDictionary_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {
 
                words.Sort((left, right) => left.Word.CompareTo(right.Word));
                listWords.Items.Refresh();
            }
            else
                MessageBox.Show("Нам нету что сортировать.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void EditWord_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {
                EditWords dt = new EditWords(words);
                dt.ShowDialog();
                words = dt.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();

            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void RemoveWord_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {
                DeleteWord dt = new DeleteWord(words);
                dt.ShowDialog();
                words = dt.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();


            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void EditTrans_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {

                EditTranscriptions dt = new EditTranscriptions(words);
                dt.ShowDialog();
                words = dt.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();

            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void EditTranslete_Click(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {

                EditTranslete dt = new EditTranslete(words);
                dt.ShowDialog();
                words = dt.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();
            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void DeleteTranslete_Click(object sender, RoutedEventArgs e)
        {

            if (words != null)
            {
                DeleteTranslete dt = new DeleteTranslete(words);
                dt.ShowDialog();
                words = dt.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();
            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void AddTranslate_Click(object sender, RoutedEventArgs e)
        {
            if(words != null)
            {
                AddTranslete addTranslete = new AddTranslete(words);
                addTranslete.ShowDialog();
                words = addTranslete.words;
                listWords.ItemsSource = words;
                listWords.Items.Refresh();
            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (words != null)
            {
                if (words.Count() != countWords && checkSave == false)
                {
                    MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            if (words != null)
                            {
                                using (StreamWriter fstream = new StreamWriter(nameDictionary + "-Dictionary.txt"))
                                {
                                    if (words != null)
                                    {
                                        foreach (var item in words)
                                        {
                                            fstream.WriteLine(item.Word);
                                            fstream.WriteLine(item.Transcription);
                                                fstream.WriteLine(item.Translations);
                                        }
                                    }
                                }
                            }
                            break;
                        case MessageBoxResult.No:

                            break;
                    }
                }
            }
        }
        private void ButtonFind_Click(object sender, RoutedEventArgs e)
        {
            if(words != null)
            {
                string findWord = FindTextBox.Text;
                int temping = 0;
                foreach (var item in words)
                {
                    if (item.Word.Contains(findWord))
                    {
                        MessageBox.Show($"Word: {item.Word}\nTranscription: {item.Transcription}\nTranslations: {item.Translations}\nIndex: {temping}", "Info", MessageBoxButton.OK, MessageBoxImage.Information); ;


                    }
                    temping++;
                }
                FindTextBox.Clear();
            }
            else
                MessageBox.Show("У вас ещё нету слов.", "Erorr", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для действий с словарём есть кнопка'Manage words' в ней все описано что и как, кнопки 'OK' в выборе нужны для подтверждения",  "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
    }
}
