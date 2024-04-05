using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public enum ViewDirection
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}
public class PlayerControll : MonoBehaviour
{
    Vector2 direction;
    public ViewDirection viewdirection;
    public GameObject target; // 콜라이더 오브젝트
    public Transform GetTarget;
    public bool running = false;
    public bool fire = false;

    public float speed = 0.01f;
    public float run = 1;

    Animator animator;
    private void Awake()
    {
        GetTarget = transform.GetChild(0);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * run);
    }
    private void OnFire(InputValue inputValue)
    {
        fire = inputValue.isPressed;
    }
    private void OnMove(InputValue inputValue)
    {
        direction  = inputValue.Get<Vector2>();
        if (direction.x == 0 && direction.y != 0)
        {
            animator.SetBool("Idle", false);
            if (direction.y < 0)
            {
                viewdirection = ViewDirection.Down;
                GetTarget.localPosition = new Vector2(0, -1.5f);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    Debug.Log("getget");
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                }

            }
            else if (direction.y > 0)
            {
                viewdirection = ViewDirection.Up;
                GetTarget.localPosition = new Vector2(0, 1.5f);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                }

            }
        }
        else if (direction.x != 0 && direction.y == 0)
        {
            animator.SetBool("Idle", false);
            if (direction.x < 0)
            {
                viewdirection = ViewDirection.Left;
                GetTarget.localPosition = new Vector2(-1, 0);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                }
            }
            else if (direction.x > 0)
            {
                viewdirection = ViewDirection.Right;
                GetTarget.localPosition = new Vector2(1, 0);
                if (animator.GetInteger("Direction") != (int)viewdirection)
                {
                    animator.SetInteger("Direction", (int)viewdirection);
                    animator.SetTrigger("Move");
                }
            }
        }
        else if (direction.x == 0 && direction.y == 0)
        {
            animator.SetBool("Idle",true);
        }
    }

    private void OnRun(InputValue inputValue)
    {
        running = inputValue.isPressed;
        run = running ? 2 : 1;
    }
}
