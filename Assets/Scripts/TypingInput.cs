using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingInput : MonoBehaviour
{
    WordManager wordManager;

    void Awake()
    {
        wordManager = FindObjectOfType<WordManager>();
    }

    void Update()
    {
        //TODO make input non-case sensitive
        
        if (wordManager.IsWordActive())
        {
            // Input.inputString: grabs all characters written in this frame
            foreach (char keyInput in Input.inputString)
            {
                // only accept letter input, ignore other keys
                if (char.IsLetter(keyInput))
                {
                    wordManager.TypeLetter(keyInput);
                }
            }
        }
    }
}
