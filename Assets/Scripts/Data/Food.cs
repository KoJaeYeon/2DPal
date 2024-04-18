using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Product
{
    public Food(string itemName, int _id, int count, float weight, float lavor, string description = "", int[,] ingredients = null) : base(itemName, _id, count, weight, lavor, description, ingredients)
    {

    }

    public Food(Food food) : base(food)
    {


    }
}
