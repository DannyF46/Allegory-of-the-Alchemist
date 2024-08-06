using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class LiquidLightingManager : MonoBehaviour
{
    [SerializeField] private int numLights = 10; //number of lights to be created and managed -- Renderer asset settings need
                                                 //Rendering Path set to Forward+ (for 256 lights) or Deffered (for infinte?)

    private GameObject[] particles; //liquid particles inside bottles
    private Light[] lights; //light components
    private GameObject[] lightObjects; //light gameobjects -- needed to set rotation

    //Light parameters
    public float intensity = 1E8f; 
    public float range = 1000;
    public LightShadows shadows = LightShadows.None;
    public float spotangle = 2.5f;

    public InventoryNavigation InvNav;
    //public Ingredient currentIngredient; //cache for currently selected ingredient
    public Liquid currentLiquid; //if currentIngreident is a liquid, caches currently selected liquid

    void Start()
    {
        InvNav = GameObject.FindObjectOfType<InventoryNavigation>();
        //currentIngredient = InvNav.activeIngredient; //cache starting ingredient
        currentLiquid = InvNav.activeIngredient.isFluid ? InvNav.activeIngredient as Liquid : null; //if starting ingredient is liquid, cache it as Liquid
        
        //create empty game object to attach light component to
        GameObject empty = new GameObject();

        //initialize light and particle lists
        lightObjects = new GameObject[numLights];
        lights = new Light[numLights];

        //instantiate the empties, then add the light comps and apply settings
        for (int i = 0; i < numLights; i++)
        {
            lightObjects[i] = GameObject.Instantiate(empty, this.transform);
            lights[i] = lightObjects[i].AddComponent<Light>();
            lights[i].enabled = false;

            lights[i].type = LightType.Spot;
            lights[i].innerSpotAngle = 0;
            lights[i].spotAngle = spotangle;
            lights[i].intensity = intensity;
            lights[i].range = range;
            lights[i].shadows = shadows;
        }

        //if currentLiquid is not null, then the current object is a liquid and must be illuminated to create fluid effect. 
        if (currentLiquid)
        {
            particles = GameObject.FindGameObjectsWithTag("water");

            //enable and set light colors according to the starting liquid
            foreach (Light light in lights)
            {      
                light.enabled = true;
                light.color = currentLiquid.color;
            }
        }
        
        
    }
    
    void Update()
    {
        //check InventoryNavigation (not cache) if current ingredient is a liquid
        if (InvNav.activeIngredient.isFluid)
        {
            //change lights if the selected ingredient changes and is different than the cached liquid
            if (currentLiquid != InvNav.activeIngredient)
            {
                ChangeLitIngredient(InvNav.activeIngredient as Liquid);
            }

            //Every frame, aim each light at a random fluid particle -- the idea is to sort of show the fluid shadow "on average" to communicate properties
            //also makes a neat, liquid-like shimmering effect as a bonus.
            foreach (GameObject light in lightObjects)
            {
                int randIndex = Mathf.FloorToInt((particles.Length) * Random.value);
                Transform particleToLight = particles[randIndex].transform;
                light.transform.LookAt(particleToLight);
            }

            //update light parameters on the spot (for noodling via inspector)
            foreach (Light light in lights)
            {
                light.intensity = intensity;
                light.spotAngle = spotangle;
                light.color = currentLiquid.color;
                light.range = range;
                light.shadows = shadows;
            }
        }
        else //if InvNav's activeIngredient is not a liquid, then null liquid cache and disable lights
        {
            currentLiquid = null;
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
        }

        
    }

    public void ChangeLitIngredient(Liquid liquid)
    {
        particles = GameObject.FindGameObjectsWithTag("water");//gets the fluid particles for the new liquid ingredient
        
        //enable lights and set color of new liquid
        foreach (Light light in lights)
        {
            light.enabled = true;
            light.color = liquid.color;
        }
        currentLiquid = liquid; //cache the liquid
    }

}
