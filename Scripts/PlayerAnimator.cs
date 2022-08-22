using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnim;
    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
    }
    public void MovementAnim(float speed)
    {
        playerAnim.SetFloat(TagManager.PLAYERANIMPARAMETER, speed);
    }

}
