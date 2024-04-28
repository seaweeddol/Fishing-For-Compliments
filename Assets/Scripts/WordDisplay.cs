using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WordDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float timeBeforeClearingWord = 0.3f;
    [SerializeField] Color32 correctColor;
    [SerializeField] Color32 incorrectColor;

    string word;
    string correctWordHex;
    string incorrectWordHex;

    void Awake()
    {
        correctWordHex = correctColor.r.ToString("X2") + correctColor.g.ToString("X2") + correctColor.b.ToString("X2");
        incorrectWordHex = incorrectColor.r.ToString("X2") + incorrectColor.g.ToString("X2") + incorrectColor.b.ToString("X2");
    }

    public void SetWord(string _word)
    {
        word = _word;
        text.text = word;
    }

    public void ChangeLetterColor(int currentLetterIndex, bool isWordCorrect)
    {
        string frontText = word[0..(currentLetterIndex + 1)];
        string backText = word[(currentLetterIndex + 1)..word.Count()];
        string color;

        if (isWordCorrect)
        {
            color = correctWordHex;
        }
        else
        {
            color = incorrectWordHex;
        }

        text.text = "<color=#" + color + ">" + frontText + "</color>" + backText;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    public void WordTypedCorrectly()
    {
        text.color = correctColor;
        text.text += char.Parse("!");
        StartCoroutine(WordTypedAnimation(1.2f));
    }

    public void WordTypedIncorrectly(int letterIndex)
    {
        ChangeLetterColor(letterIndex, false);
        StartCoroutine(WordTypedAnimation(0.8f));
    }

    IEnumerator WordTypedAnimation(float targetScaleSize)
    {
        yield return StartCoroutine(Utilities.UpdateScaleOverTime(transform, targetScaleSize));

        yield return new WaitForSecondsRealtime(timeBeforeClearingWord);

        RemoveWord();
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
