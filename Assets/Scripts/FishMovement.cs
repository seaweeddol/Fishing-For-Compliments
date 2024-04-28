using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int fishDifficulty = 1;
    Rigidbody2D myRigidbody;

    float currentMoveSpeed;
    bool fishIsHooked;

    void Start()
    {
        currentMoveSpeed = moveSpeed;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            FlipSprite();
        }
    }

    public void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        moveSpeed = -moveSpeed;
    }

    public void StopFishMovement()
    {
        currentMoveSpeed = moveSpeed;
        moveSpeed = 0f;
    }

    public void FishHooked()
    {
        moveSpeed = 0f;

    }

    // IEnumerator WiggleFishWhileHooked(SpriteRenderer renderer)
    // {
    //     float counter = 0;

    //     //Get current color
    //     Quaternion fishRotation = transform.rotation;

    //     while (fishIsHooked)
    //     {
    //         counter += Time.deltaTime;

    //         //Fade from 0 to 1
    //         float alpha = Mathf.Lerp(0, 1, counter / Time.deltaTime);

    //         //Change alpha only
    //         renderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);

    //         //Wait for a frame
    //         yield return null;
    //     }
    // }

    // IEnumerator WiggleFish()
    // {

    // }

    public void ResetFishMovement()
    {
        moveSpeed = currentMoveSpeed;

        // TODO: reset rotation
        // reset camera
    }

    public int GetFishDifficulty()
    {
        return fishDifficulty;
    }

    public void FishIsCaught()
    {
        Destroy(gameObject);
    }

    public void FishGotAway()
    {
        ResetFishMovement();
        // add a duplicate fish
    }
}
