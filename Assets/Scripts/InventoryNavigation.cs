using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNavigation : MonoBehaviour
{
    private InventoryGeneration InvGen;
    private RecipeManager RecMan;

    [Header("Ingredients")]
    public Ingredient[] ingredients;

    public int activeSubmissionIndex;//selected submission slot (goes up to Recipe.Length - 1)
    public List<Ingredient> ingredientsForSubmission; //the ingredients that the player wants to submit for the recipe
    
    public int activeIngredientIndex { get; private set; }
    [SerializeField] public Ingredient activeIngredient { get; private set; }

    
    // Start is called before the first frame update
    void Awake()
    {
        InvGen = GetComponent<InventoryGeneration>();
        RecMan = GetComponent<RecipeManager>();

        ingredientsForSubmission = new List<Ingredient>(RecMan.RecipeLength); 
        ingredients = new Ingredient[InvGen.Inventory.Length];
        
        //Cache the inventory
        for(int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i] = InvGen.Inventory[i].GetComponent<Ingredient>();
        }

        activeIngredientIndex = 0; //current inventory slot selected
        activeSubmissionIndex = 0; //current submission slot selected

        activeIngredient = ingredients[activeIngredientIndex]; //Set ingredient in current inventory slot to active
        activeIngredient.active = true;

        foreach (Ingredient ingredient in ingredients) //deactivate the other ignredients
        {
            if (!ingredient.active)
            {
                ingredient.gameObject.SetActive(false);
            }
        }
    }



    public void ChangeSelectedIngredient(int changedir) //called from InputManager
    {
        if (!changedir.Equals(0))
        {
            //deactivate the ingredient in the current ingredient
            activeIngredient.active = false;
            activeIngredient.gameObject.SetActive(false);

            //get the next inventory slot and activate it's ingredient
            activeIngredientIndex = Mathf.Clamp(activeIngredientIndex + changedir, 0, ingredients.Length - 1);
            activeIngredient = ingredients[activeIngredientIndex];
            activeIngredient.gameObject.SetActive(true);
            activeIngredient.active = true;
        }
    }

    public void ChangeSubmissionSelection(int changedir)
    {
        if (!changedir.Equals(0))
        {
            //get the next submission slot index and mark it as the current active one
            activeSubmissionIndex = Mathf.Clamp(activeSubmissionIndex + changedir, 0, RecMan.RecipeLength-1);
            Debug.Log(activeSubmissionIndex);

        }
    }

    public void AddIngredientToSubmission()
    {
        ingredientsForSubmission.Add(activeIngredient); //adds current ingredient to the current submission slot
        activeIngredient.MarkForSubmission();
    }
    public void RemoveIngredientFromSubmission()
    {
        ingredientsForSubmission.Remove(activeIngredient);
        activeIngredient.RemoveFromSubmission();
    }
    public void ToggleSubmission()
    {
        if(activeIngredient.markedForSubmission)
        {
            RemoveIngredientFromSubmission();
        }
        else if(!activeIngredient.markedForSubmission)
        {
            if (ingredientsForSubmission.Count >= RecMan.RecipeLength)
            {
                Debug.Log("Remove an ingredient before adding another!");
            }
            else
            {
                AddIngredientToSubmission();
            }
        }
    }

}
