using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static IEnumerator UpdateScaleOverTime(Transform transform, float targetScaleSize, float duration = 0.1f)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScaleVector = new(targetScaleSize, targetScaleSize, targetScaleSize);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScaleVector, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScaleVector;
    }

    public static IEnumerator UpdateRotationOverTime(Transform transform, Quaternion targetRotation, float duration = 0.5f)
    {
        Quaternion initialRotation = transform.localRotation;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
    }

    // public static IEnumerator RotateBackAndForth(
    //     Transform transform, 
    //     Quaternion targetRotation, 
    //     float duration = 1f, 
    //     float durationPerRotation = 0.5f
    // )
    // {
    //     float elapsedTime = 0f;

    //     while (elapsedTime < duration)
    //     {
    //         transform.gameObject
    //         StartCoroutine(UpdateRotationOverTime(transform, targetRotation));
    //         elapsedTime += Time.deltaTime;
    //         yield return new;
    //     }
    // }

    // IEnumerator WiggleFishHookedText()
    // {
    //     float timeToWiggle = 1f;

    //     while (Time.time <= timeToWiggle)
    //     {
    //         time = Mathf.PingPong(1 * Time.time, 0.5f);
    //         intitialFishhookedText.transform.localRotation =
    //             Quaternion.Lerp(
    //                 intitialFishhookedText.transform.localRotation,
    //                 Quaternion.Euler(0, 0, -10), time
    //             );
    //         intitialFishhookedText.transform.localRotation =
    //             Quaternion.Lerp(
    //                 intitialFishhookedText.transform.localRotation,
    //                 Quaternion.Euler(0, 0, 10), time
    //             );

    //         yield return null;
    //     }
    // }



}
