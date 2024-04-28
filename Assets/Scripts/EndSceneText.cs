using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        text.text = "You achieved your life's goal!\n" + gameSession.GetFishCaught() + " fish complimented";
    }
}
