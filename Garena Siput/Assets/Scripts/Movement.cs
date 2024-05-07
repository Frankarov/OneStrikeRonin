using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public string playerInputPrefix;
    public float moveSpeed = 3;
    public bool universalCanMove;
    public bool inverted = false;
    private Vector2 movement;
    private float horizontal;
    private float vertical;
    public Rigidbody2D rb;

    public Animator animatorWalk;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;
    private Vector2 input;

    private float MoveX;
    private float MoveY;

    private void Start()
    {
    }

    private void Update()
    {
        //if (universalCanMove == true)
        //{
        //    if (inverted)
        //    {
        //        horizontal = -Input.GetAxisRaw(playerInputPrefix + "horizontal");
        //        vertical = -Input.GetAxisRaw(playerInputPrefix + "vertical");
        //        movement = new Vector3(horizontal, vertical, 0f).normalized;
        //        //transform.Translate(new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime);
        //    }
        //    else if (!inverted)
        //    {
        //        horizontal = Input.GetAxisRaw(playerInputPrefix + "horizontal");
        //        vertical = Input.GetAxisRaw(playerInputPrefix + "vertical");
        //        movement = new Vector3(horizontal, vertical, 0f).normalized;
        //        //transform.Translate(new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime);
        //    }

        //}

        ProcessInputs();
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

    void ProcessInputs()
    {
        if (universalCanMove)
        {



            if (inverted)
            {
                MoveX = Input.GetAxisRaw(playerInputPrefix+"Horizontal");
                MoveY = Input.GetAxisRaw(playerInputPrefix + "Vertical");
                input.x = Input.GetAxisRaw(playerInputPrefix + "Horizontal");
                input.y = Input.GetAxisRaw(playerInputPrefix + "Vertical");
            }
            else if(!inverted)
            {
                MoveX = Input.GetAxisRaw(playerInputPrefix + "Horizontal");
                MoveY = Input.GetAxisRaw(playerInputPrefix + "Vertical");
                input.x = Input.GetAxisRaw(playerInputPrefix + "Horizontal");
                input.y = Input.GetAxisRaw(playerInputPrefix + "Vertical");
            }

        }



        if((MoveX == 0 && MoveY == 0) && (input.x !=0 || input.y != 0)){
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

