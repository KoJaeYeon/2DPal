using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCancel : MonoBehaviour
{
    public GetTarget getTarget;

    void Cancel()
    {
        getTarget.CancelBuilding();
    }
}
