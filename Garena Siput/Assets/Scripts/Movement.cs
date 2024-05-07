using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public string playerInputPrefix;
    public float moveSpeed = 3;
    public bool universalCanMove;
    public bool inverted = false;

    public Rigidbody2D rb;

    public Animator animatorWalk;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;
    public Vector2 input;

    private float MoveX;
    private float MoveY;

    InputAction playerInputAction;
    private void Awake()
    {
        playerInputAction = new(); 
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Start()
    {
        Debug.Log(playerInputAction.activeControl);
    }

    private void Update()
    {

        Animate();
        if(input.x < 0f && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }

    } // close update

    private void FixedUpdate()
    {
        rb.velocity = input * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        if (universalCanMove)
        {
            MoveX = value.Get<Vector2>().x;
            MoveY = value.Get<Vector2>().y;
            input = new Vector2(MoveX, MoveY);
            if (inverted)
            {
                input *= -1;
            }
        }



        if((MoveX == 0 && MoveY == 0) && (input.x !=0 || input.y != 0)){
            lastMoveDirection = input;
        }


        input.Normalize();

    }

    public void OnMovePlayer2(InputValue value)
    {
        Debug.Log(value.Get<Vector2>());
        if (universalCanMove)
        {
            MoveX = value.Get<Vector2>().x;
            MoveY = value.Get<Vector2>().y;
            input = new Vector2(MoveX, MoveY);
            if (inverted)
            {
                input *= -1;
            }
        }



        if ((MoveX == 0 && MoveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }


        input.Normalize();

    }

    void Animate()
    {
        animatorWalk.SetFloat("MoveX", input.x);
        animatorWalk.SetFloat("MoveY", input.y);
        animatorWalk.SetFloat("MoveMagnitude", input.sqrMagnitude);
        animatorWalk.SetFloat("LastMoveX", lastMoveDirection.x);
        animatorWalk.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;
        //Scale.x *= -1;
        transform.localScale = Scale;
        facingLeft = !facingLeft;
    }


}

