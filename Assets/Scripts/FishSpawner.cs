using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] GameObject parentGO;
    [SerializeField] GameObject spawningArea;
    [SerializeField] GameObject initialSpawningArea;
    [SerializeField] TextMeshProUGUI fishRemainingUI;
    [SerializeField] TextMeshProUGUI fishCaughtUI;

    [Header("Fish Lists")]
    [SerializeField] List<GameObject> spawnableFish;
    [SerializeField] List<GameObject> upcomingFish;

    [Header("Gameplay Modifiers")]
    [SerializeField] int numFishToSpawnAtStart = 10;
    [SerializeField] int fishSpawnTimer = 10;
    [SerializeField] int nextDifficultyThreshold = 5;

    Renderer spawnAreaRenderer;
    PlayerController playerController;
    LevelChanger levelChanger;
    GameSession gameSession;
    int fishDifficultyTracker = 0;
    public int fishRemaining = 0;
    int currentDifficultyThreshold = 0;
    int currentDifficultyLevel = 1;

    int fishCaught = 0;
    bool allFishCaught;

    void Awake()
    {
        // Spawn initial fish randomly across the entire water area
        spawnAreaRenderer = initialSpawningArea.GetComponent<Renderer>();

        playerController = FindObjectOfType<PlayerController>();
        levelChanger = FindObjectOfType<LevelChanger>();
        gameSession = FindObjectOfType<GameSession>();
        fishRemainingUI.text = "Fish Remaining: " + fishRemaining.ToString();
        fishCaughtUI.text = "Fish Complimented: " + fishCaught;

        for (int i = 0; i < numFishToSpawnAtStart; i++)
        {
            SpawnFish();
        }

        // After initial fish spawn, spawn new fish in smaller spawningArea to make it more feasible to empty the pond
        spawnAreaRenderer = spawningArea.GetComponent<Renderer>();
        InvokeRepeating(nameof(SpawnFish), fishSpawnTimer, fishSpawnTimer);
    }

    /*
        Generate a list of fish difficulties to select from. 
        Difficulties are added to the list x times, where x is the difficulty.

        Example: 
            If currentDifficultyLevel = 4, the list will be:
            [1, 2, 2, 3, 3, 3, 4, 4, 4, 4]
    */
    List<int> FishDifficultyList()
    {
        List<int> fishDifficulties = new();

        for (int i = 1; i <= currentDifficultyLevel; i++)
        {
            for (int difficulty = 1; difficulty <= i; difficulty++)
            {
                fishDifficulties.Add(i);
            }
        }

        return fishDifficulties;
    }

    void SpawnFish()
    {
        if (allFishCaught) { return; }

        GameObject fishToSpawn = spawnableFish[0];

        if (currentDifficultyLevel > 1)
        {
            List<int> fishDifficulties;
            fishDifficulties = FishDifficultyList();

            // select a random fish from the spawnableFish list
            int selectedDifficulty = fishDifficulties[Random.Range(0, fishDifficulties.Count)];

            List<GameObject> fishOfSelectedDifficulty = new();
            foreach (GameObject fish in spawnableFish)
            {
                if (fish.GetComponent<FishMovement>().GetFishDifficulty() == selectedDifficulty)
                {
                    fishOfSelectedDifficulty.Add(fish);
                }
            }

            fishToSpawn = fishOfSelectedDifficulty[Random.Range(0, fishOfSelectedDifficulty.Count)];
        }

        // instantiate new fish at random location within spawning area
        GameObject newFish = Instantiate(
            fishToSpawn,
            GetRandomSpawnPoint(),
            Quaternion.identity,
            parentGO.transform
        );

        StartCoroutine(FadeIn(newFish.GetComponent<SpriteRenderer>(), 1f));

        // randomize what direction the fish is facing
        if (Random.value > 0.5)
        {
            newFish.GetComponent<FishMovement>().FlipSprite();
        }

        fishRemaining++;
        fishRemainingUI.text = "Fish Remaining: " + fishRemaining.ToString();
    }

    IEnumerator FadeIn(SpriteRenderer renderer, float duration)
    {
        float counter = 0;

        //Get current color
        Color spriteColor = renderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            //Fade from 0 to 1
            float alpha = Mathf.Lerp(0, 1, counter / duration);

            //Change alpha only
            renderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

            //Wait for a frame
            yield return null;
        }
    }

    Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(
            Random.Range(spawnAreaRenderer.bounds.min.x, spawnAreaRenderer.bounds.max.x),
            Random.Range(spawnAreaRenderer.bounds.min.y, spawnAreaRenderer.bounds.max.y),
            0
        );
    }

    public void UpdateFishCaught(bool fishWasCaught)
    {
        if (allFishCaught) { return; }

        if (fishWasCaught)
        {
            playerController.GetCurrentFishHooked().FishIsCaught();

            fishDifficultyTracker++;
            fishRemaining--;
            fishCaught++;

            if (fishRemaining == 0)
            {
                // TODO probably will be some bugs associated with this
                Debug.Log(fishCaught);
                gameSession.SetFishCaught(fishCaught);
                levelChanger.ShowGameOverScreen();
            }

            fishRemainingUI.text = "Fish Remaining: " + fishRemaining;
            fishCaughtUI.text = "Fish Complimented: " + fishCaught;

            // Increase difficulty if player has reached the nextDifficultyThreshold
            if (fishDifficultyTracker % nextDifficultyThreshold == 0 && upcomingFish.Count > 0)
            {
                IncreaseDifficulty();
            }
        }
        else
        {
            playerController.GetCurrentFishHooked().FishGotAway();

            if (fishDifficultyTracker > 0)
            {
                fishDifficultyTracker--;

                if (fishDifficultyTracker == currentDifficultyThreshold - 1 && spawnableFish.Count > 1)
                {
                    DecreaseDifficulty();
                }
            }
        }

        playerController.SetFishHooked(false);
    }

    void DecreaseDifficulty()
    {
        List<GameObject> fishToRemove = new();

        foreach (GameObject nextFish in spawnableFish)
        {
            if (nextFish.GetComponent<FishMovement>().GetFishDifficulty() == currentDifficultyLevel)
            {
                fishToRemove.Add(nextFish);
                upcomingFish.Insert(0, nextFish);
            }
        }

        for (int i = 0; i < fishToRemove.Count; i++)
        {
            spawnableFish.RemoveAt(spawnableFish.Count - 1);
        }

        currentDifficultyLevel--;
        fishSpawnTimer--;
        currentDifficultyThreshold -= nextDifficultyThreshold;
    }

    /* 
        If player falls below the currentDifficultyThreshold, 
        make the game easier by removing most difficult fish from spawnableFish
    */
    void IncreaseDifficulty()
    {
        currentDifficultyLevel++;
        fishSpawnTimer++;
        currentDifficultyThreshold = fishDifficultyTracker;

        List<GameObject> fishToRemove = new();

        // add fish of next difficulty to spawnableFish
        foreach (GameObject nextFish in upcomingFish)
        {
            if (nextFish.GetComponent<FishMovement>().GetFishDifficulty() == currentDifficultyLevel)
            {
                fishToRemove.Add(nextFish);
                spawnableFish.Add(nextFish);
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < fishToRemove.Count; i++)
        {
            upcomingFish.RemoveAt(0);
        }
    }

    public bool AllFishCaught()
    {
        return allFishCaught;
    }
}
