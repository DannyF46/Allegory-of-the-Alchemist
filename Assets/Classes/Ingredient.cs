using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Rigidbody))]
public class Ingredient : MonoBehaviour
{
    public string ingredientname { get; protected set; }
    public Rigidbody rb { get; protected set; } //if fluid, rb is the bottle. if solid, rb is the ingredient model
    public bool active = false;
    public Sprite uiIcon { get; protected set; }
    public bool isFluid { get; protected set; }

    public bool markedForSubmission { get; protected set; }
    protected void SetRigidbody()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Extrapolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }
    virtual public void Awake()
    {
        
    }
    public void MarkForSubmission()
    {
        markedForSubmission = true;
    }

    public void RemoveFromSubmission()
    {
        markedForSubmission = false;
    }
    /*public void RotateObject(Rigidbody rb)
    {
        //Drag mouse to rotate in screen plane
        Vector2 drag = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 deltaRot = new(drag.y, -drag.x, 0);
        Quaternion qdeltaRot = Quaternion.Euler(deltaRot);
        // rb.MoveRotation(qdeltaRot * rb.rotation); // THIS IS GOOD -- docs: If Rigidbody interpolation is enabled on the Rigidbody,
        // calling Rigidbody.MoveRotation will resulting in a smooth transition between the two
        // rotations in any intermediate frames rendered. This should be used if you want to
        // continuously rotate a rigidbody in each FixedUpdate.


        //use Q and E to rotate along the axis through the screen

        Vector2 roll = new(Input.GetKey(KeyCode.Q) ? 1 : 0, Input.GetKey(KeyCode.E) ? 1 : 0); //use bool?1:0 to turn a bool to an int
        Vector3 deltaRotRoll = new(0, 0, roll.y - roll.x);// if you press both buttons, it does nothing.
        Quaternion qdeltaRotRoll = Quaternion.Euler(Time.fixedDeltaTime * rollSpeed * deltaRotRoll);

        //apply both parts of the rotation -- mouse and Q/E
        rb.MoveRotation(qdeltaRotRoll * qdeltaRot * rb.rotation); //order of mult matters -- quatA * quatB -> apply B then A 
                                                                  //concept: fluid density/volume determines rotation speed
    }*/
}
