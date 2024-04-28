using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool buttonHighlighted;

    IEnumerator RotateBackAndForth()
    {
        StartCoroutine(Utilities.UpdateScaleOverTime(transform, 1.2f));

        float rotation = 3f;

        while (buttonHighlighted)
        {
            rotation = -rotation;
            yield return StartCoroutine(Utilities.UpdateRotationOverTime(transform, Quaternion.Euler(0, 0, rotation)));
        }

        yield return StartCoroutine(Utilities.UpdateRotationOverTime(transform, Quaternion.Euler(0, 0, 0)));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHighlighted = true;
        StartCoroutine(RotateBackAndForth());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonHighlighted = false;
        StartCoroutine(Utilities.UpdateScaleOverTime(transform, 1.0f));
    }
}
