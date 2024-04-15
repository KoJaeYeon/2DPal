using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Pal
{
    private int _id;
    private string _palName;
    private int _lv;
    private int _exp;
    private float _hungry;
    private float _health;
    private float _maxHealth;
    private string _description;
    private Sprite _portrait;

    public Pal(int id, string palName, int lv, int exp, int hungry, int health, string description = "")
    {
        _id = id;
        _palName = palName;
        _lv = lv;
        _exp = exp;
        _hungry = hungry;
        _health = health;
        _maxHealth = health;
        _description = description;
    }

    public Pal(Pal pal)
    {
        _id= pal._id;
        _palName = pal._palName;
        _lv = pal._lv;
        _exp = pal._exp;
        _hungry= pal._hungry;
        _health = pal._health;
        _maxHealth = pal._maxHealth;
        _description = pal._description;
        _portrait = pal._portrait;
    }

    public Pal()
    {
    }

    #region Property
    public int id { get => _id; set => _id = value; }
    public string palName { get => _palName; set => _palName = value; }
    public int lv { get => _lv; set => _lv = value; }
    public int exp { get => _exp; set => _exp = value; }
    public float hungry { get => _hungry; set => _hungry = value; }
    public float health { get => _health; set => _health = value; }
    public float maxHealth { get => _maxHealth; set => _maxHealth = value; }
    public Sprite portrait { get => _portrait; set => _portrait = value; }
    #endregion
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Pal other = obj as Pal;
        if (other._id == _id && other._palName == _palName && other._lv == _lv && other._exp == _exp && other._hungry == _hungry && other._health == _health) return true;
        else return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return "id : " + _id + " Name : " + _palName + " Lv : " + _lv + " Exp : " + _exp + " Hungry : " + _hungry + " Health : " + _health;
    }
}
