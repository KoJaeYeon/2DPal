using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    Vector2 direction;

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }
    private void OnMove(InputValue inputValue)
    {
        direction  = inputValue.Get<Vector2>();
    }
}
