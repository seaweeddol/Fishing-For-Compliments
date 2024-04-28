using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    // TODO add ability to raise and lower fishing line
    List<GameObject> fishOnLine = new();

    void OnTriggerEnter2D(Collider2D fish)
    {
        fishOnLine.Add(fish.gameObject);
    }

    void OnTriggerExit2D(Collider2D fish)
    {
        fishOnLine.Remove(fish.gameObject);
    }

    public FishMovement GetFish()
    {
        if (fishOnLine.Count != 0)
        {
            FishMovement mostDifficultFish = fishOnLine[0].GetComponent<FishMovement>();
            
            // Get most difficult fish from fishOnLine
            foreach (GameObject fish in fishOnLine)
            {
                FishMovement currentFish = fish.GetComponent<FishMovement>();
                if (currentFish.GetFishDifficulty() > mostDifficultFish.GetFishDifficulty())
                {
                    mostDifficultFish = currentFish;
                }
            }
            
            return mostDifficultFish;
        }
        else
        {
            return null;
        }
    }
}
