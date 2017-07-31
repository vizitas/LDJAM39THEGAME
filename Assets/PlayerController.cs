using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Walking, Programming, Coffee, Energy, Sun, Oven }
    [SerializeField]
    float playerWalkingSpeed = 1f;
    Rigidbody2D body;
    public static PlayerState State;
    private Animator animator;
    private bool reallyWalking;
    bool isFacingRight = true;
    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        State = PlayerState.Walking;
    }
    void Update()
    {
        reallyWalking = false;
        if (State == PlayerState.Walking)
        {
            if (Input.GetKey(KeyCode.A))
            {
                body.velocity = new Vector2(-playerWalkingSpeed, 0);
                Flip(false);
                reallyWalking = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                body.velocity = new Vector2(playerWalkingSpeed, 0);
                Flip(true);
                reallyWalking = true;
            }
            else
            {
                body.velocity = new Vector2(0, 0);
            }
        }
        else
        {
            body.velocity = new Vector2(0, 0);
        }
        AnimationLogic();
    }
    private void AnimationLogic()
    {

        animator.SetBool("moving", State == PlayerState.Walking && reallyWalking);
        animator.SetBool("working", State != PlayerState.Walking);
    }
    private void Flip(bool toRight)
    {
        if ((isFacingRight && toRight) || (!isFacingRight && !toRight))
            return;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = toRight;
    }
}
