﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace TyperX.ViewModels
{
    public class MainWindowViewModel : AvaloniaObject
    {
        
        private static readonly string[] words = new string[]
        {
            "cat", "dog", "bat", "rat", "hat", "mat", "pen", "cup", "box", "fox",
            "jam", "ham", "ram", "yam", "zip", "dip", "lip", "sip", "top", "pop",
            "bug", "rug", "hug", "mug", "fun", "run", "sun", "bun", "pin", "win",
            "car", "bar", "far", "tar", "red", "bed", "fed", "led", "sad", "mad",
            "toy", "boy", "joy", "soy", "cow", "bow", "row", "mow", "tap", "nap",
            "egg", "leg", "peg", "beg", "big", "dig", "fig", "pig", "fit", "hit",
            "ice", "ace", "pie", "tie", "zoo", "boo", "goo", "moo", "zap", "cap",
            "nut", "cut", "gut", "hut", "bet", "get", "let", "met", "set", "wet",
            "dot", "hot", "lot", "pot", "add", "bad", "dad", "had", "pad", "sad",
            "house","brave","google","firefox","librefox","mouse","keyboard","lose","win",
            "number","binary","hexa","visual","code","visualstudio"
        };

        // AvaloniaProperty-k a bindinghoz
        public static readonly StyledProperty<string> TimeLeftProperty =
            AvaloniaProperty.Register<MainWindowViewModel, string>(nameof(TimeLeft), "Rendelkezésre álló idő: 30 sec");

        public static readonly StyledProperty<string> CurrentWordProperty =
            AvaloniaProperty.Register<MainWindowViewModel, string>(nameof(CurrentWord), "Press Start!");

        public static readonly StyledProperty<string> UserInputProperty =
            AvaloniaProperty.Register<MainWindowViewModel, string>(nameof(UserInput), "");

        public static readonly StyledProperty<string> FeedbackProperty =
            AvaloniaProperty.Register<MainWindowViewModel, string>(nameof(Feedback), "");

        public static readonly StyledProperty<string> ResultProperty =
            AvaloniaProperty.Register<MainWindowViewModel, string>(nameof(Result), "");


        public string TimeLeft
        {
            get => GetValue(TimeLeftProperty);
            set => SetValue(TimeLeftProperty, value);
        }

        public string CurrentWord
        {
            get => GetValue(CurrentWordProperty);
            set => SetValue(CurrentWordProperty, value);
        }

        public string UserInput
        {
            get => GetValue(UserInputProperty);
            set => SetValue(UserInputProperty, value);
        }

        public string Feedback
        {
            get => GetValue(FeedbackProperty);
            set => SetValue(FeedbackProperty, value);
        }

        public string Result
        {
            get => GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        // Mezők
        private Random rand = new Random();
        private List<string> testWords = new List<string>();
        private int currentIndex = 0;
        private int correctWords = 0;
        private int totalWords = 0;
        private Stopwatch stopwatch = new Stopwatch();
        private DispatcherTimer timer;

        // Parancs
        public ICommand StartCommand { get; }

        public MainWindowViewModel()
        {
            StartCommand = new DelegateCommand(StartTest);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;
        }

        private void StartTest()
        {
            testWords.Clear();
            correctWords = 0;
            totalWords = 0;
            currentIndex = 0;
            Result = "";
            Feedback = "";

            //random kivalaszthato szavak for ciklusa 20ig 
            for (int i = 0; i < 20; i++)
            {
                testWords.Add(words[rand.Next(words.Length)]);
            }

            CurrentWord = testWords[currentIndex];
            stopwatch.Restart();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (stopwatch.Elapsed.TotalSeconds >= 30 || currentIndex >= testWords.Count)
            {
                timer.Stop();
                EndTest();
            }
            else
            {
                TimeLeft = $"Hátralévő idő: {(30 - stopwatch.Elapsed.TotalSeconds):0} sec";
            }
        }

        public void CheckInput()
        {
            if (string.IsNullOrEmpty(UserInput) || stopwatch.Elapsed.TotalSeconds >= 30 || currentIndex >= testWords.Count)
            {
                if (stopwatch.Elapsed.TotalSeconds >= 30 || currentIndex >= testWords.Count)
                {
                    timer.Stop();
                    EndTest();
                }
                return;
            }

            totalWords++;
            if (UserInput == testWords[currentIndex])
            {
                correctWords++;
                Feedback = "Jó! ✅";
            }
            else
            {
                Feedback = "Hiba! ❌";
            }

            currentIndex++;
            if (currentIndex < testWords.Count)
            {
                CurrentWord = testWords[currentIndex];
                UserInput = "";
            }
            else
            {
                timer.Stop();
                EndTest();
            }
        }

        private void EndTest()
        {
            stopwatch.Stop();
            timer.Stop();
            double wpm = correctWords / (stopwatch.Elapsed.TotalMinutes);
            double accuracy = (double)correctWords / totalWords * 100;
            Result = $"WPM: {wpm:0.0} | Pontosság: {accuracy:0}% | Helyes: {correctWords}/{totalWords}";
            CurrentWord = "Vége!";
            Feedback = "";
        }
    }

    
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => _execute();
    }
}