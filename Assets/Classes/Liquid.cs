using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Polybrush;

public class Liquid : Ingredient
{
    public enum PresetsEnum {custom, water, blood, honey };
    //public PresetsEnum preset = PresetsEnum.custom;
    //private PresetsEnum currentpreset;
    public int preset;
    private int currentpreset;

    [Header("Liquid Properties")]
    //public string fluidname;
    [Range(0,10)] public int viscosity;
    public Color color; //read by LiquidLightManager
    public int bottleindex;

    public Rigidbody[] particleRBs;

    override public void Awake()
    {
        isFluid = true; //set Ingredient.isFluid to true, since this is a liquid
        SetRigidbody();

        particleRBs = GetComponentsInChildren<Rigidbody>(); //Liquid prefabs contain fluid particles as children
    }
    public void ApplyPreset(int prst, int botind, Sprite uiicon)
    {
        SetProperties(LiquidTypes.PresetDictionary[prst],botind, uiicon);
        currentpreset = prst;
    }
    public void SetProperties(LiquidTypes.Preset prst, int botind, Sprite uiicon)
    {

        ingredientname = prst._name;
        viscosity = prst._viscosity;
        foreach (Rigidbody rb in particleRBs)
        {
            rb.drag = viscosity;
            rb.angularDrag = viscosity;
        }
        color = prst._color;
        bottleindex = botind;
        uiIcon = uiicon;
        Debug.Log(ingredientname);
    }
    /*public void ApplyPreset(PresetsEnum prst)
    {
        switch (prst)
        {
            case PresetsEnum.custom://let the inspector determine params (only drags are changed here -- LiquidLightManager handles light color)
                foreach (Rigidbody rb  in particleRBs) 
                {
                    rb.drag = viscosity;
                    rb.angularDrag = viscosity;
                }
                break;

            case PresetsEnum.water:
                SetProperties(LiquidTypes.water);
                break;

            case PresetsEnum.blood:
                SetProperties(LiquidTypes.blood);
                break;

            case PresetsEnum.honey:
                SetProperties(LiquidTypes.honey);
                break;

        }
        //currentpreset = prst;
    }*/
    
}
