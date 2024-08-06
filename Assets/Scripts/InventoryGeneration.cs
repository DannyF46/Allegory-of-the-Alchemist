using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryGeneration : MonoBehaviour //responsible for spawning in ingredients, and generating UI elements
{
    [Range(0, 1)]
    [SerializeField] private float percentSolids;
    [SerializeField] private GameObject[] bottles; //all bottled prefabs
    [SerializeField] public Sprite[] bottleicons; //all UI icons for bottles
    [SerializeField] private GameObject[] solids; //all solid prefabs
    [SerializeField] public Sprite[] solidicons;

    //index lists to keep track of which ingredients were spawned -- theres deff a better way but this was what I came up with first so
    private List<int> spawnedsolidindices;
    private List<int> spawnedliquidindices;

    [SerializeField] private Transform InventoryTransform; //parent object for generated objects
    [SerializeField] private int numToGenerate = 2; //number of ingredients to generate
    [SerializeField] public GameObject[] Inventory { get; private set; } //list of all the generated objects

    void Awake()
    {   
        Inventory = new GameObject[numToGenerate];
        InventoryTransform = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Transform>();

        spawnedliquidindices = new List<int>();
        spawnedsolidindices = new List<int>();

        int NumBottles = bottles.Length;
        //int NumSolids = solids.Length;
        
        for(int i = 0; i < Inventory.Length; i++)
        {
            int WeightedLiquidOrSolid = CustomRound(Random.value, 1-percentSolids);//0 or 1 -- 0 means generate a liquid, 1 means generate a solid. Weighted rounding of random number.
            
            if (WeightedLiquidOrSolid == 0)
            {
                int randomBottleIndex = Mathf.FloorToInt(Random.value * NumBottles); //pick a random bottle, its fine if multiple fluids have the same bottle shape (though
                                                                                            //bottle shape could deff be a puzzle element somehow)

                int uniqueRandomFluidIndex = GenerateUniqueLiquidPreset();//pick a random fluid to fill the bottle, ensuring no two bottles have the same fluid

                Inventory[i] = GameObject.Instantiate(bottles[randomBottleIndex], InventoryTransform); //spawn bottle
                Inventory[i].GetComponent<Liquid>().ApplyPreset(uniqueRandomFluidIndex, randomBottleIndex, bottleicons[randomBottleIndex]); //set fluid and bottle (bottle the liquid :] )


            }
            else if (WeightedLiquidOrSolid == 1)
            {
                int uniqueRandomSolidIndex = GenerateUniqueSolid();//pick a random solid without picking the same one twice

                Inventory[i] = GameObject.Instantiate(solids[uniqueRandomSolidIndex], InventoryTransform); //spawn the solid
                Inventory[i].GetComponent<Solid>().ApplyPreset(uniqueRandomSolidIndex, solidicons[uniqueRandomSolidIndex]); //set solid params (i.e. ingredient name)
            }


        }    
        
    }
    public int GenerateUniqueLiquidPreset()
    {

        int randomFluidIndex = Mathf.FloorToInt(Random.value * (LiquidTypes.PresetDictionary.Count - 1)); //pick a random fluid to fill the bottle (-1 compensates for the none preset)

        if (!spawnedliquidindices.Contains(randomFluidIndex))
        {
            spawnedliquidindices.Add(randomFluidIndex);
            return randomFluidIndex;
        }
        else 
        {
            return GenerateUniqueLiquidPreset();
        } 
    }

    public int GenerateUniqueSolid()
    {
        int NumSolids = solids.Length;
        int randomSolidIndex = Mathf.FloorToInt(Random.value * NumSolids); //pick a random fluid to fill the bottle

        if (!spawnedsolidindices.Contains(randomSolidIndex))
        {
            spawnedsolidindices.Add(randomSolidIndex);
            return randomSolidIndex;
        }
        else
        {
            return GenerateUniqueSolid();
        }
    }

    public int CustomRound(float x, float a)// if x > a, return 1. if x<1 return 0 (so its a Heaviside)
    {
        if(x >=  a)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
