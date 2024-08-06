using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;

public class IngredientSelector : MonoBehaviour
{
    [Header("Ingredients")]
    public Ingredient[] ingredients;
    private int minIndex = 0;
    private int maxIndex = 1;

    public int activeIndex;
    public Ingredient activeIngredient;
    public bool fluidActive;

    [Header("UI")]
    public RectTransform[] uiIcons;
    public float defaultIconSize = 1f;
    public float selectedIconSize = 1.4f;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = this.GetComponentsInChildren<Ingredient>();
        maxIndex = ingredients.Length - 1;

        activeIndex = 0;
        activeIngredient = ingredients[activeIndex];
        activeIngredient.active = true;
        uiIcons[activeIndex].localScale = selectedIconSize * Vector3.one;

        foreach (Ingredient ingredient in ingredients)
        {
            if (!ingredient.active)
            {
                ingredient.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeSelectedIngredient(int changedir)
    {
        //int changedir = (Input.GetKeyDown(KeyCode.D) ? 1 : 0) - (Input.GetKeyDown(KeyCode.A) ? 1 : 0);
        if (!changedir.Equals(0))
        {
            activeIngredient.active = false;
            uiIcons[activeIndex].localScale = defaultIconSize*Vector3.one;
            activeIngredient.gameObject.SetActive(false);


            activeIndex = Mathf.Clamp(activeIndex + changedir, minIndex, maxIndex);
            activeIngredient = ingredients[activeIndex];
            activeIngredient.gameObject.SetActive(true);
            activeIngredient.active = true;

            uiIcons[activeIndex].localScale = selectedIconSize * Vector3.one;
        }
    }
}
