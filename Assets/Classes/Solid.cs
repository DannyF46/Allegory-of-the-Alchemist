using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class Solid : Ingredient
{
    //public string solidname;
    public int currentpreset;
    override public void Awake()
    {
        SetRigidbody();
        isFluid = false;

    }
    public void ApplyPreset(int prst, Sprite uiicon) //could be an abstract function in Ingredient but thats not my priority right now
    {
        SetProperties(SolidTypes.SolidsDictionary[prst], uiicon);
        currentpreset = prst;
    }
    public void SetProperties(SolidTypes.Solids prst, Sprite uiicon)
    {
        ingredientname = prst._name;
        uiIcon = uiicon;
        Debug.Log(ingredientname);
    }

}
