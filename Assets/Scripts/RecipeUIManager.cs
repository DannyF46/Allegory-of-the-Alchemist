using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUIManager : MonoBehaviour
{
    public RecipeManager RecMan;
    public TMPro.TMP_Text RecipeText;
    // Start is called before the first frame update
    void Start()
    {
        RecMan = GameObject.FindObjectOfType<RecipeManager>();
        //RecipeText = GetComponentInChildren<TMPro.TMP_Text>();

        for (int i = 0; i < RecMan.RecipeLength; i++)
        {
            RecipeText.text += $"{RecMan.Recipe[i].ingredientname} \n".ToUpper();
        }
    
    }
}
