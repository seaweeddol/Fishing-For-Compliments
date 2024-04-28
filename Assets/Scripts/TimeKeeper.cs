using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    float time = 0f;

    void Update()
    {
        time += Time.deltaTime;
    }

    public float GetTime()
    {
        return time;
    }
}
