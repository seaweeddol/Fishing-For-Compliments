using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    [SerializeField] GameObject wordPrefab;
    [SerializeField] GameObject wordCanvas;
    [SerializeField] GameObject initialFishUI;
    [SerializeField] GameObject intitialFishhookedText;
    Word activeWord;
    FishSpawner fishSpawner;
    TimeKeeper timeKeeper;

    bool isFirstFishHooked = true;

    void Awake()
    {
        fishSpawner = FindObjectOfType<FishSpawner>();
        timeKeeper = FindObjectOfType<TimeKeeper>();
    }

    public void ShowWord(int difficulty)
    {
        if (isFirstFishHooked)
        {
            isFirstFishHooked = false;
            StartCoroutine(DisplayInitialFishHookedUI(difficulty));
        }
        else
        {
            Word word = new(WordRandomizer.GetRandomWord(difficulty), DisplayWord());
            activeWord = word;
        }
    }

    public void TypeLetter(char letter)
    {
        if (!IsWordActive()) { return; }

        if (activeWord.GetNextLetter() == letter)
        {
            activeWord.LetterTypedCorrectly();

            if (activeWord.WordTyped())
            {
                activeWord.DisplayCorrectWord();
                activeWord = null;
                fishSpawner.UpdateFishCaught(true);
            }
        }
        else
        {
            activeWord.LetterTypedIncorrectly();
            activeWord = null;
            fishSpawner.UpdateFishCaught(false);
        }
    }

    public bool IsWordActive()
    {
        return activeWord != null;
    }

    WordDisplay DisplayWord()
    {
        wordCanvas.SetActive(true);
        GameObject wordObject = Instantiate(wordPrefab, wordCanvas.transform);
        WordDisplay wordDisplay = wordObject.GetComponent<WordDisplay>();

        return wordDisplay;
    }

    IEnumerator DisplayInitialFishHookedUI(int difficulty)
    {
        initialFishUI.SetActive(true);

        float currentTime = timeKeeper.GetTime();
        float timeToRotate = 3f;
        float rotation = 2f;

        while (timeKeeper.GetTime() <= currentTime + timeToRotate)
        {
            rotation = -rotation;
            yield return StartCoroutine(Utilities.UpdateRotationOverTime(intitialFishhookedText.transform, Quaternion.Euler(0, 0, rotation)));
        }

        initialFishUI.SetActive(false);

        Word word = new(WordRandomizer.GetRandomWord(difficulty), DisplayWord());
        activeWord = word;
    }

    public bool IsWordCanvasActive()
    {
        return wordCanvas.activeSelf;
    }
}
