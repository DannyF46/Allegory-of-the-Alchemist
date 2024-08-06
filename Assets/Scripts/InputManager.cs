using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

using UnityEngine.UIElements;
using System.Runtime.CompilerServices;

public class InputManager : MonoBehaviour
{
    public Camera cam;
    private Vector2 MousePos;
    private InventoryNavigation InvNav;
    private SubmissionCheck SubCheck;
    private InventoryUIManager InvUI;
    private HealthManager HealthManager;

    public float maxRotSpeed = 10f;
    public float xyrotspeed = 1f;
    public float zrotspeed = 1f;
    
    private bool enableRotation = false;
    private void Start()
    {
        cam = Camera.main;
        InvNav = FindObjectOfType<InventoryNavigation>();
        SubCheck = FindObjectOfType<SubmissionCheck>();
        InvUI = FindObjectOfType<InventoryUIManager>();
        HealthManager = FindObjectOfType<HealthManager>();
    }

    public void EnableRotation (InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            enableRotation = true;
        }
        else if(context.canceled)
        {
            enableRotation = false;
        }
    }
    public void RotateObjectXY(InputAction.CallbackContext context)
    {
        if(enableRotation)
        {
            Rigidbody ingredientRB = InvNav.activeIngredient.rb;
            Vector2 drag = 0.01f * context.ReadValue<Vector2>();
            //Vector2 drag = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Quaternion deltaRotXY = Quaternion.Euler(drag.y, -drag.x, 0);

            ingredientRB.MoveRotation(deltaRotXY * ingredientRB.rotation); // THIS IS GOOD -- docs: If Rigidbody interpolation is enabled on the Rigidbody,
                                                                           // calling Rigidbody.MoveRotation will resulting in a smooth transition between the two
                                                                           // rotations in any intermediate frames rendered. This should be used if you want to
                                                                           // continuously rotate a rigidbody in each FixedUpdate.
        }

    }
    public void RotateObjectZ(InputAction.CallbackContext context)
    {
        if (enableRotation)
        { 
            /*Rigidbody ingredientRB = ingredientSelector.activeIngredient.rb;
            // use Q and E to rotate along the axis through the screen
            float rollSpeed = 100f;

            float roll = context.ReadValue<float>();
            //if(context.action.IsPressed())
            {
                Vector3 deltaRotRoll = new(0, 0, roll);
                Quaternion deltaRotZ = Quaternion.Euler(Time.fixedDeltaTime * rollSpeed * deltaRotRoll);

                ingredientRB.MoveRotation(deltaRotZ * ingredientRB.rotation); //order of mult matters -- quatA * quatB -> apply B then A 
                                                                              //concept: fluid density/volume determines rotation speed
            }*/
        }

    }
    private void Update()
    {
        if(enableRotation)
        {
            RotateObject();
        }
    }
    public void RotateObject()
    {
        Rigidbody ingredientRB = InvNav.activeIngredient.rb;

        //Drag mouse to rotate in screen plane
        Vector2 drag = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        drag *= xyrotspeed;
        drag = Vector2.ClampMagnitude(drag, maxRotSpeed);
        Quaternion xydeltaRot = Quaternion.Euler(drag.y, -drag.x, 0);

        //use Q and E to rotate along the axis through the screen
        
        Vector2 roll = new(Input.GetKey(KeyCode.Q) ? 1 : 0, Input.GetKey(KeyCode.E) ? 1 : 0); //use bool?1:0 to turn a bool to an int
        Vector3 deltaRotRoll = new(0, 0, roll.y - roll.x);// if you press both buttons, it does nothing.
        Quaternion zdeltaRot = Quaternion.Euler(Time.fixedDeltaTime * zrotspeed * deltaRotRoll);

        //apply both parts of the rotation -- mouse and Q/E
        ingredientRB.MoveRotation(zdeltaRot * xydeltaRot * ingredientRB.rotation); //order of mult matters -- quatA * quatB -> apply B then A 
                                                                  //concept: fluid density/volume determines rotation speed

    }
    public void SwitchIngredients(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InvNav.ChangeSelectedIngredient((int)context.ReadValue<float>());
            InvUI.ChangeActiveIcon();
        }
    }

    public void SwitchSubmission(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InvNav.ChangeSubmissionSelection((int)ctx.ReadValue<float>());
        }
    }

    public void AddSubmisison(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            InvUI.ToggleSubmissionParticles();
            InvNav.ToggleSubmission();
            //InvNav.AddIngredientToSubmission();
            
        }
    }

    public void Submit(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            SubCheck.CheckSubmission();

            if(!SubCheck.Correct)
            {
                HealthManager.UpdateHealth();
            }
            
        }
        
    }
}
