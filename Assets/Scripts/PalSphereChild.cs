using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalSphereChild : MonoBehaviour
{
    public void Check(int count)
    {
        BattleManager.Instance.Check(count);
    }
}
