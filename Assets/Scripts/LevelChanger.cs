using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] GameObject interfaceCanvas;
    [SerializeField] GameObject introCanvas;
    [SerializeField] GameObject creditsCanvas;
    [SerializeField] Animator animator;
    int levelToLoad;
    bool buttonHighlighted;

    public void StartButtonPressed()
    {
        interfaceCanvas.SetActive(false);
        introCanvas.SetActive(true);

        StartCoroutine(WaitForKeyPress(KeyCode.Space));
    }

    public void MainMenuButtonPressed()
    {
        FadeToLevel(0);
    }

    public void ShowGameOverScreen()
    {
        // TODO add an escape button menu and ability to quit the game
        FadeToLevel(2);
    }

    IEnumerator WaitForKeyPress(KeyCode keyCode)
    {
        bool keyPressed = false;

        while (!keyPressed)
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyPressed = true;
            }
            yield return null;
        }

        FadeToLevel(1);
    }

    public void CreditsButtonPressed()
    {
        creditsCanvas.SetActive(true);
        interfaceCanvas.SetActive(false);

        StartCoroutine(WaitForKeyPressCredits(KeyCode.Space));
    }

    IEnumerator WaitForKeyPressCredits(KeyCode keyCode)
    {
        bool keyPressed = false;

        while (!keyPressed)
        {
            if (Input.GetKeyDown(keyCode))
            {
                keyPressed = true;
            }
            yield return null;
        }

        creditsCanvas.SetActive(false);
        interfaceCanvas.SetActive(true);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("FadeIn");
    }
}
