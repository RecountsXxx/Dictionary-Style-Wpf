using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;

namespace Dictionary_Exam_WPF_
{
    public class Words
    {
 
        public Words()
        {

        }
        public string Word { get; set; }
        public string Transcription { get; set; }
        public string Translations { get; set; }
        public Words(string word, string transcription, string translations)
        {
            this.Word = word;
            this.Transcription = transcription;
            Translations = translations;
;
        }
        public void RemoveWord()
        {

        }
        public void EditWord(string word)
        {
            Word = word;
        }
        public void EditTranscription(string trans)
        {
            Transcription = trans;
        }
        public void AddTranslete(string trans)
        {
            Translations += ", " + trans;
        }
        public void EditTranslete(string newValue,string oldValue)
        {
            Translations = Translations.Replace(oldValue, newValue);
        }
        public void DeleteTranslete(int index,int count,string str)
        {

            Translations = Translations.Replace(str, string.Empty);
            Translations = Translations.Trim();
            if(Translations.Contains("  "))
            {
                Translations = Translations.Replace("  ", " ");
            }
           if(Translations.Last() == ',')
            {
                Translations = Translations.Replace(",", " ");
            }

        }
    }

    public class Dictionary
    {
        public string nameDictionary { get; set; }

        public Dictionary(string nameDictionary)
        {
            this.nameDictionary = nameDictionary;
        }
        public Dictionary()
        {
          
        }
        public void menu(string nameDictionaryText)
        {
            MenuDictionary menuDictionary = new MenuDictionary(nameDictionary);
            menuDictionary.nameDictionaryLabel.Content += nameDictionary;
            menuDictionary.Show();        
        }
    }
}
