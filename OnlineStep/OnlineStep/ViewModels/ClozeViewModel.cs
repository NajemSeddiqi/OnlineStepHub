using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using OnlineStep.Models;
using OnlineStep.Navigation.Interfaces;
using OnlineStep.Services;
using Xamarin.Forms;
using Page = Xamarin.Forms.Page;

namespace OnlineStep.ViewModels
{
    public class ClozeViewModel : BaseViewModel 
    {
        private readonly INavigator _navigator;
        private readonly Cloze _cloze;
        private readonly string[] _sentences;
        private string _missingWord;

        public ClozeViewModel(INavigator navigator)
        {
            Debug.WriteLine("ClozeViewModel Constructor:");
            _cloze = (Cloze) PageNavigator.GetCurrentPage;
            _navigator = navigator;
            _sentences = SplitSentence(_cloze.content.sentence, _cloze.content.missingWords);
            _missingWord = _cloze.content.missingWords[0];
            Title = _cloze.title;
            Image = _cloze.image;
            EntryPlaceholder = CreatePlaceholder(_missingWord);
            GuessedWord = "";

            ShowCorrection = false;
            ShowCorrectMeButton = true;

            ClozeGuiHelper = InitClozeLabelAndEntry(_cloze.content.missingWords[0], _cloze.content.sentence);
        }
        //Empty constructor used for testing

        public ClozeViewModel(){}

        public string[] SplitSentence(string sentence, List<string> missingWords)
        {
            string missingWord = missingWords[0];
            return sentence.Split(new string[] { missingWord }, StringSplitOptions.None);
        }

        public string CreatePlaceholder(string missingWord)
        {

            string placeholder = "";
            for (int i = 0; i < missingWord.Length; i++)
            {
                placeholder += "_";
            }
            return placeholder;
        }

        public ICommand CheckCorrectAnswer => new Command(() =>
        {
            
            Debug.WriteLine(GuessedWord);
            if (GuessedWord.Equals(_missingWord, StringComparison.InvariantCultureIgnoreCase))
            {
                //Logic for right answer
            
                CorectOrWrongBool = true;
                CorrectOrWrongMessage = "Rätt svar!";
                PageNavigator.Xp += 10;
            }
            else
            {
                //Logic for wrong answer
                CorectOrWrongBool = false;
                CorrectOrWrongMessage = "Fel svar";

            }
            PageNavigator.PageResults.Add(CorectOrWrongBool);
            ShowCorrection = true;
            ShowCorrectMeButton = false;
        });

        public ICommand GoToNextPage => new Command(() =>
        {
            PageNavigator.PushNextPage(_navigator);
        });


        public List<ClozeRow> InitClozeLabelAndEntry(string missingWord, string sentence) 
        {
            List<ClozeRow> displayCloze = new List<ClozeRow>();

         
            int rowSize = 25;
            string rowText = "";
            int EmptyEntrySize = -6;
            int EntryMultiplyer = 12;
            string[] words = sentence.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                rowText = rowText + words[i] + " ";
                if (rowText.Length >= rowSize || i+1 == words.Length)
                {
                    if (rowText.Contains(missingWord) == true)
                    {
                    
                        string[] parts = rowText.Split(new string[] { missingWord }, StringSplitOptions.None);

                        if (1 == parts.Length)
                        {
                            parts[1] = "";
                        }

                        if (0 == parts.Length)
                        {
                            parts[0] = "";
                        }

                        int entry = missingWord.Length * EntryMultiplyer;

                        ClozeRow rowWithEntry = new ClozeRow()
                        {
                            SentenceFirstPart = parts[0],
                            SentenceSecondPart = parts[1],
                            EntryLenght = entry
                        };
                        displayCloze.Add(rowWithEntry);
                        rowText = "";

                    }
                    else
                    {
                        ClozeRow rowWithoutEntry = new ClozeRow()
                        {
                            SentenceFirstPart = rowText,
                            EntryLenght = EmptyEntrySize,
                            SentenceSecondPart = ""
                        };
                        displayCloze.Add(rowWithoutEntry);
                        rowText = "";

                    }

                }
              
            }

            return displayCloze;
        }
        public List<ClozeRow> ClozeGuiHelper { get; set; }
        public string CorrectOrWrongMessage { set; get; }
        public bool CorectOrWrongBool { set; get; }
        public bool ShowCorrection { set; get; }
        public bool ShowCorrectMeButton { set; get; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string SentencesPartOne => _sentences[0];
        public string SentencesPartTwo => _sentences[1];
        public string EntryPlaceholder { get; set; }
        public string GuessedWord { set; get; }
        public double Progress => PageNavigator.GetProgress();
    }

    public class ClozeRow
    {
        public string SentenceFirstPart { get; set; }
        public string SentenceSecondPart { get; set; }
        public int EntryLenght { get; set; }
    }

}

