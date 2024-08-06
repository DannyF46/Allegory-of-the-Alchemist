using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Generate a random recipe based on IngredientGeneration
public class RecipeManager : MonoBehaviour
{
    public int RecipeLength = 4; //how many ingredients the recipe should be made of
    private InventoryGeneration InvGen; 

    public Ingredient[] Recipe; //the ingredients needed for the recipe
    private List<int> recipeingredientindices;

    void Awake()
    {
        InvGen = GetComponent<InventoryGeneration>();
        Recipe = new Ingredient[RecipeLength];

        recipeingredientindices = new List<int>();//used to ensure we dont try to take the same ingredient twice

        for (int i = 0; i < Recipe.Length; i++)
        {
            int uniqueRandomRecipeIndex = GenerateUniqueIndex();
            Recipe[i] = InvGen.Inventory[uniqueRandomRecipeIndex].GetComponent<Ingredient>();
            Debug.Log(Recipe[i].ingredientname);
        }
    }
    public int GenerateUniqueIndex()
    {
        int randomRecipeIngredientIndex = Mathf.FloorToInt(Random.value * InvGen.Inventory.Length);

        if (!recipeingredientindices.Contains(randomRecipeIngredientIndex))
        {
            recipeingredientindices.Add(randomRecipeIngredientIndex);
            return randomRecipeIngredientIndex;
        }
        else
        {
            return GenerateUniqueIndex();
        }
    }
}
