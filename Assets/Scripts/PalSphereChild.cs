using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereChild : MonoBehaviour
{
    PalSphere palSphere;

    private void Awake()
    {
        palSphere = transform.parent.GetComponent<PalSphere>();
    }
    public void Check(int count)
    {
        palSphere.Check(count);
    }
}
