using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Product : Item
{
    private float _lavor;
    private int[,] _ingredients;
    public Product(string itemName, int _id, int count, float weight, float lavor , string description = "", int[,] ingredients = null) : base(itemName, _id, count, weight, description)
    {
        this._lavor = lavor;
        this._ingredients = ingredients;
    }

    public Product(Product product) : base(product)
    {
        this._lavor = product._lavor;
        this._ingredients = product._ingredients;
    }

    public float lavor
    {
        get => _lavor;
        set => _lavor = value;
    }
    public int[,] ingredients
    {
        get => _ingredients;
        set => _ingredients = value;
    }

}
