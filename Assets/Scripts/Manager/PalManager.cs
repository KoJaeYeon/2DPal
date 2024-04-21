using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalManager : Singleton<PalManager>
{
    public List<Building> buildings = new List<Building>();
    public List<Building> producing = new List<Building>();
    public List<Building> cooking = new List<Building>();
    public List<Building> sleeping = new List<Building>();

    public List<StrawPalBed> palBedBuilding;
    private void Awake()
    {
        
    }

    public void ClearList()
    {
        buildings.Clear();
        producing.Clear();
        cooking.Clear();
        sleeping.Clear();
    }
}
