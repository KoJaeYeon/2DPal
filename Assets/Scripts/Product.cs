using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : Item
{
    private int[,] _ingredients;
    public Product(string itemName, int _id, int count, float weight, string description = "", int[,] ingredients = null) : base(itemName, _id, count, weight, description)
    {
        this._ingredients = ingredients;
    }

    public Product(Product product) : base(product)
    {
        this._ingredients = product._ingredients;
    }

    public int[,] ingredients
    {
        get => _ingredients;
        set => _ingredients = value;
    }

}
