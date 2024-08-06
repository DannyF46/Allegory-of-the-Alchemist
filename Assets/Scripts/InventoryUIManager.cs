using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public RectTransform inventoryBar;
    public RectTransform inventoryIcons;
    public RectTransform submissionParticles;

    public ParticleSystem ParticlesPrefab;

    private InventoryNavigation InvNav;
    public RectTransform[] uiIcons;
    private InventoryGeneration InvGen;
    public UnityEngine.UI.Image[] uiImages;
    public List<ParticleSystem> submissionPartSys;

    public float defaultIconSize = 1f;
    public float selectedIconSize = 1.4f;

    private int activeIconIndex = 0; // cache for InvNav.activeIngredientIndex

    private void Start()
    {
        InvGen = GameObject.FindObjectOfType<InventoryGeneration>();
        InvNav = GameObject.FindObjectOfType<InventoryNavigation>();

        uiIcons = new RectTransform[InvNav.ingredients.Length];
        uiImages = new UnityEngine.UI.Image[InvNav.ingredients.Length];

        submissionPartSys = new List<ParticleSystem>(InvNav.ingredientsForSubmission.Capacity);

        inventoryBar.sizeDelta = new(230* InvNav.ingredients.Length, 200);

        for (int i = 0; i < uiImages.Length; i++)
        {
            //create all the icons needed for the current inventory size
            uiIcons[i] = new GameObject("icon", typeof(RectTransform)).GetComponent<RectTransform>();
            uiIcons[i].SetParent(inventoryIcons);
            uiIcons[i].anchorMin = new(0, 0.5f);
            uiIcons[i].anchorMax = new(0, 0.5f);
            uiIcons[i].pivot = new(0.5f, 0.5f);
            uiIcons[i].sizeDelta = new(100, 100);
            uiIcons[i].localPosition = new(150 + i * 225, 0, 0);
            uiIcons[i].localScale = defaultIconSize * Vector2.one;

            uiImages[i] = uiIcons[i].AddComponent<UnityEngine.UI.Image>();
            uiImages[i].sprite = InvNav.ingredients[i].uiIcon;

            //Fluid icons - apply appropriate color
            if (InvNav.ingredients[i].isFluid)
            {
                var liq = InvNav.ingredients[i] as Liquid;
                uiImages[i].color = liq.color;
            }

        }

        uiIcons[activeIconIndex].localScale = selectedIconSize*Vector3.one; //enlarge the icon for the currently active inventory slot 

    }

    public void ChangeActiveIcon()
    {
        uiIcons[activeIconIndex].localScale = defaultIconSize * Vector3.one; //shrink active icon back to default size

        activeIconIndex = InvNav.activeIngredientIndex; //update active index

        uiIcons[activeIconIndex].localScale = selectedIconSize * Vector3.one; //enlarge new active icon to show its selected
    }
    
    public void AddSubmissionParticles()
    {

        var icon = uiIcons[InvNav.activeIngredientIndex];
        var NewParticles = Instantiate(ParticlesPrefab, submissionParticles);
        NewParticles.transform.position = icon.transform.position;

        var npshape = NewParticles.shape;
        npshape.sprite = InvNav.activeIngredient.uiIcon;
        submissionPartSys.Add(NewParticles);
        
        
    }

    public void RemoveSubmissionParticles()
    {

        int index = InvNav.ingredientsForSubmission.IndexOf(InvNav.activeIngredient);
        Destroy(submissionPartSys[index].gameObject);
        submissionPartSys.Remove(submissionPartSys[index]);
        
    }

    public void ToggleSubmissionParticles()
    {
        if (InvNav.activeIngredient.markedForSubmission)
        {
            RemoveSubmissionParticles();
        }
        else if (!InvNav.activeIngredient.markedForSubmission)
        {
            if (InvNav.ingredientsForSubmission.Count >= InvNav.ingredientsForSubmission.Capacity)
            {
                Debug.Log("Remove an ingredient before adding another!");
            }
            else
            {
                AddSubmissionParticles();
            }
        }
    }
}
