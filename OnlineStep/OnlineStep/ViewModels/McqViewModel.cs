using OnlineStep.Models;
using OnlineStep.Navigation.Interfaces;
using OnlineStep.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace OnlineStep.ViewModels
{
    class McqViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly Mcq _mcq;
        private string _correctAnswer { get; set; }
         
        public McqViewModel(INavigator navigator)
        {
            Debug.WriteLine("McqViewModel Constructor: ");
            _mcq = (Mcq)PageNavigator.GetCurrentPage;
            _navigator = navigator;
            Title = _mcq.title;
            Question = _mcq.content.question;
            _correctAnswer = _mcq.content.correctAnswer;
            Debug.WriteLine("Before _answerHelper loop: answers.count = " + _mcq.content.answers.Count);

            CreateAnswerList();

            ShowCorrection = false;
            ShowCorrectMeButton = true;
            AnythingSelected = false;
        }
        private void CreateAnswerList()
        {
            for (int i = 0; i < _mcq.content.answers.Count; i++)
            {
                Debug.WriteLine("Inside _answerHelper loop: index = " + i);
                Answer answerHelper = new Answer
                {
                    Value = _mcq.content.answers[i],
                    Selected = false
                };
                _answers.Add(answerHelper);
            }
        }

        public string Title { get; set; }
        public string Question { get; set; }
        public string CorrectOrWrongMessage { get; set; }
        public bool CorectOrWrongBool { get; set; }
        public bool AnythingSelected { get; set; }
        public bool ShowCorrection { get; set; }
        public bool ShowCorrectMeButton { get; set; }
        public ICommand SelectAnswer => new Command<string>((commandAnswer) =>
        {
            if (ShowCorrectMeButton)
            {
                for (int i = 0; i < _answers.Count; i++)
                {
                    _answers[i].Selected = false;
                }
                foreach (var answer in _answers.Where(answer => answer.Value.Equals(commandAnswer)).Select(answer => answer)) { answer.Selected = true; }
                AnythingSelected = true;
            }
        });

        public ICommand CheckCorrectAnswer => new Command(() =>
        {
            if (AnythingSelected)
            {
                foreach (var answer in _answers.Where(answer => answer.Selected).Select(answer => answer))
                {
                    if (answer.Value.Equals(_correctAnswer, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //Logic for right answer
                        Debug.WriteLine("Rätt svar");
                        CorectOrWrongBool = true;
                        CorrectOrWrongMessage = "Rätt svar!";
                        PageNavigator.Xp += 10;
                    }
                    else
                    {
                        //Logic for wrong answer
                        Debug.WriteLine("Fel svar");
                        CorectOrWrongBool = false;
                        CorrectOrWrongMessage = "Fel svar";

                    }
                }
                PageNavigator.PageResults.Add(CorectOrWrongBool);
                ShowCorrection = true;
                ShowCorrectMeButton = false;
            }
            
        });
        
        
        //Answer keeps track on the selected answer
        private List<Answer> _answers = new List<Answer>();
        public List<Answer> Answers { get => _answers;}
        public class Answer : INotifyPropertyChanged
        {
            public string Value { get; set; }
            public bool Selected { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
            
        }
        public ICommand GoToNextPage => new Command(() =>
        {
            PageNavigator.PushNextPage(_navigator);
        });

        public double Progress => PageNavigator.GetProgress();


    }
}

