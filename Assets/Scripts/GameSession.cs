using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int fishCaught = 0;

    void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetFishCaught(int _fishCaught)
    {
        fishCaught = _fishCaught;
    }

    public int GetFishCaught()
    {
        return fishCaught;
    }
}
