﻿using System;
using System.Linq;
using System.Windows.Forms;
using SilverLinguo.Enums;
using SilverLinguo.Extensions;
using SilverLinguo.Repositories.Models;

namespace SilverLinguo.Services.Form
{
    public class VerbalFormService
    {
        public static void SetWordTextBoxVisibilityForSelectedLanguage(SelectedLanguage selectedLanguage, TextBox firstLanguageWordTextBox, TextBox secondLanguageWordTextBox)
        {
            switch (selectedLanguage)
            {
                case SelectedLanguage.Lithuanian:
                    firstLanguageWordTextBox.Visible = false;
                    secondLanguageWordTextBox.Visible = true;
                    break;
                case SelectedLanguage.English:
                    firstLanguageWordTextBox.Visible = true;
                    secondLanguageWordTextBox.Visible = false;
                    break;
                default:
                    firstLanguageWordTextBox.Visible = false;
                    secondLanguageWordTextBox.Visible = true;
                    break;
            }
        }

        public static void HanldeVerbalFormLoadedEvent(Button nextWordButton, WordPair[] wordsArray, out int currentWordPairId, 
            Label progressLabel, TextBox firstLanguageWordTextBox, TextBox secondLanguageWordTextBox)
        {
            new Random().Shuffle(wordsArray);

            nextWordButton.Select();

            currentWordPairId = wordsArray[0].Id;

            CommonFormService.SetProgressLabelText(progressLabel, wordsArray);

            WordPair firstWord = wordsArray.First();
            firstLanguageWordTextBox.Text = firstWord.FirstLanguageWord;
            secondLanguageWordTextBox.Text = firstWord.SecondLanguageWord;
        }

        public static void HandleNextWordButtonClickedEvent(
            Button iDontKnowTheWordButton, TextBox firstLanguageWordTextBox, 
            TextBox secondLanguageWordTextBox, SelectedLanguage selectedLanguage)
        {
            iDontKnowTheWordButton.Visible = true;

            switch (selectedLanguage)
            {
                case SelectedLanguage.Lithuanian:
                    firstLanguageWordTextBox.Visible = false;
                    break;
                case SelectedLanguage.English:
                    secondLanguageWordTextBox.Visible = false;
                    break;
                case SelectedLanguage.Mixed:
                    bool currentEnLabelVisibleState = secondLanguageWordTextBox.Visible;
                    bool currentLtLabelVisibleState = firstLanguageWordTextBox.Visible;
                    secondLanguageWordTextBox.Visible = !currentEnLabelVisibleState;
                    firstLanguageWordTextBox.Visible = !currentLtLabelVisibleState;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void HandleVisibilityOnIDontKnowButtonClickedEvent(
            Button iDontKnowTheWordButton, Button nextWordButton, TextBox firstLanguageWordTextBox,
            TextBox secondLanguageWordTextBox)
        {
            iDontKnowTheWordButton.Visible = false;
            nextWordButton.Focus();

            firstLanguageWordTextBox.Visible = true;
            secondLanguageWordTextBox.Visible = true;
        }
    }
}