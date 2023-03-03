using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class objectLookingAnimationRigging : MonoBehaviour
{
    private Animator animator;
    public Transform lookatObj;

    public float weight;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(animator != null)
        {
            animator.SetLookAtPosition(lookatObj.position);
            animator.SetLookAtWeight(weight);
        }
    }
}
