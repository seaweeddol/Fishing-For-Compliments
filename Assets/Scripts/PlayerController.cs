using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // TODO add sound effects
    WordManager wordManager;
    FishingLine fishingLine;
    FishSpawner fishSpawner;

    FishMovement currentFishHooked = null;
    bool fishHooked;

    void Awake()
    {
        wordManager = FindObjectOfType<WordManager>();
        fishingLine = FindObjectOfType<FishingLine>();
        fishSpawner = FindObjectOfType<FishSpawner>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (fishSpawner != null && fishSpawner.AllFishCaught()) { return; }

            currentFishHooked = fishingLine.GetFish();

            if ((currentFishHooked != null) && !fishHooked && !wordManager.IsWordCanvasActive())
            {
                /* 
                    TODO: 
                    show "fish hooked" UI
                    zoom in on hooked fish
                    wiggle fish back and forth
                */
                fishHooked = true;
                currentFishHooked.StopFishMovement();
                int difficulty = currentFishHooked.GetFishDifficulty();
                wordManager.ShowWord(difficulty);
            }
            else
            {
                // TODO: show "no fish hooked UI"
            }
        }
    }

    public void SetFishHooked(bool isFishHooked)
    {
        fishHooked = isFishHooked;

        if (!isFishHooked)
        {
            currentFishHooked = null;
        }
    }

    public FishMovement GetCurrentFishHooked()
    {
        return currentFishHooked;
    }
}
