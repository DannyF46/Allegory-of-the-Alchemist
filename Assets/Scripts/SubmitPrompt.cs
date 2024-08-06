using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubmitPrompt : MonoBehaviour
{
    public InventoryNavigation InvNav;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Text text;

    public bool show = false;

    public Animator Animate;
    private void Start()
    {
        InvNav = FindObjectOfType<InventoryNavigation>();
        image = GetComponent<UnityEngine.UI.Image>();
        text = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    private void Update()
    { 
        if(InvNav.ingredientsForSubmission.Count.Equals(InvNav.ingredientsForSubmission.Capacity))
        {
            Animate.SetBool("Show", true);
        }
        else 
        { 
            Animate.SetBool("Show", false) ;
        }
    }
}
