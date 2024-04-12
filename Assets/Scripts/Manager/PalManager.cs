using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalManager : Singleton<PalManager>
{
    public List<Building> buildings = new List<Building>();
    public List<Building> producing = new List<Building>();
}
