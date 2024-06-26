using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    int typeIndex;

    WordDisplay display;

    public Word(string _word, WordDisplay _display)
    {
        word = _word;
        typeIndex = 0;

        display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void LetterTypedCorrectly()
    {
        display.ChangeLetterColor(typeIndex, true);
        typeIndex++;
    }

    public void LetterTypedIncorrectly()
    {
        display.WordTypedIncorrectly(typeIndex);
    }

    public bool WordTyped()
    {
        bool wordTyped = typeIndex >= word.Length;
        return wordTyped;
    }

    public void DisplayCorrectWord()
    {
        display.WordTypedCorrectly();
    }
}
