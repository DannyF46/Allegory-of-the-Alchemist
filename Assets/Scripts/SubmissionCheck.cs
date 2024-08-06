using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SubmissionCheck : MonoBehaviour
{
    private RecipeManager RecMan;
    private InventoryNavigation InvNav;
    public int score;
    public bool Correct;
    public Animator winloseAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        RecMan = GetComponent<RecipeManager>();
        InvNav = GetComponent<InventoryNavigation>();
    }

    public void CheckSubmission()
    {
        var recipe = RecMan.Recipe.ToList();
        var submission = InvNav.ingredientsForSubmission;

        score = 0;
        for (int i = 0; i < submission.Count; i++)
        {
            if (submission.Contains(recipe[i]))//if the submisison list contains a recipe ingredient, increment the score
            {
                score++;
            }
        }

        if (score == recipe.Count) //if the score is perfect (all submitted ingredients are in the recipe), then you win
        {
            Debug.Log("success!");
            winloseAnimation.SetTrigger("Win");
            Correct = true;
            
        }
        else
        {
            Debug.Log("try again!");
            winloseAnimation.SetTrigger("Lose");
            Correct = false;
        }
    }
}
