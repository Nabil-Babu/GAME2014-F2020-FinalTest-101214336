using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum States{
    IDLE,
    SHRINK,
    GROW
}

public class FloatingPlatform : MonoBehaviour
{
    [SerializeField] private Animator m_Animator; //Animator to control Platform state
    [SerializeField] private bool isShrinking = false; // Flag for checking if already shrinking
    [SerializeField] private bool smallState = false; // Reached smallest size
    [SerializeField] private bool bigState = true; // Already reached Max Size 
    [SerializeField] private bool isGrowing = false; // Flag for checking if growing
    [SerializeField] States currentState = States.IDLE; // Current State of the Platform
    void Start()
    {
        m_Animator.SetInteger("PlatformState", (int)currentState);
    }

    void Update()
    {
        // Switch Case State Machine 
        switch(currentState)
        {
            case States.IDLE:
                // Do Nothing
                break;
            case States.SHRINK:
                if(!smallState && isShrinking)
                {
                    StartCoroutine(ShrinkPlatform());
                }
                break;
            case States.GROW:
                if(!bigState && isGrowing)
                {
                    StartCoroutine(GrowPlatform());
                }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentState = States.SHRINK;
            isShrinking = true; 
            isGrowing = false;  
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            currentState = States.GROW;
            isShrinking = false;
            isGrowing = true;   
        }
    }

    IEnumerator ShrinkPlatform()
    {
        m_Animator.SetInteger("PlatformState", (int)currentState);
        yield return new WaitForSeconds(2.0f);
        smallState = true;
        bigState = false; 
        isShrinking = false;   
    }

    IEnumerator GrowPlatform()
    {
        m_Animator.SetInteger("PlatformState", (int)currentState);
        yield return new WaitForSeconds(2.0f);
        smallState = false;
        bigState = true;
        isGrowing = false;   
    }
}
