using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Temp_Pal : Slot_Pal
{
    private new void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector3 point = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = point;

    }
}
