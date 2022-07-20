using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 300f;
    //The weapon offset needs to be the same as the weaponParent's transform.position.y * -1
    public Vector2 weaponOffset = new Vector2(0, 0.08f);

 
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Vector2 movementValue, pointerInput, mousePos;
    private WeaponParent weaponParent;
    private Weapon weapon;
    private Camera cam;
    private CameraController camController;
    public Animator animator;
    
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main; 
        weaponParent = GetComponentInChildren<WeaponParent>();
        weapon = weaponParent.GetComponentInChildren<Weapon>();
        camController = cam.GetComponent<CameraController>();

    }

    void Update() {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 aimDirection = mousePos - rb.position + weaponOffset;
        weaponParent.SetAngle(aimDirection);
        camController.SetOffset(mousePos/10);
    }

    void FixedUpdate()
    {  
        ChangeWeaponAndCharacterDirection();
        rb.velocity = movementValue*Time.fixedDeltaTime*speed;

        animator.SetFloat("Speed", rb.velocity.magnitude);

        if(Mouse.current.leftButton.wasPressedThisFrame) {
            weapon.Fire();
            Debug.Log("Fired");
        }
    }

    //Gets movement input from the player 
    void OnMove(InputValue movementInput) {
        movementValue = movementInput.Get<Vector2>();
    }

    // Activated when the player presses the attack button
    void OnFire(InputValue pointerInput) {
        weapon.Fire();
        Debug.Log("Fired");
    }

    void ChangeWeaponAndCharacterDirection() {
         if( movementValue.x > 0) {
            transform.localScale = Vector3.one;
            weaponParent.transform.localScale = Vector3.one;
        }else if ( movementValue.x < 0) {
            transform.localScale = new Vector3(-1,1,1);
            weaponParent.transform.localScale = new Vector3(-1,1,1);
        }

        if(mousePos.x < rb.position.x) {
            weaponParent.transform.localScale = new Vector3(weaponParent.transform.localScale.x,-1,1);
        } else if (mousePos.x > rb.position.x) {
            weaponParent.transform.localScale = new Vector3(weaponParent.transform.localScale.x,1,1);
        }
    }
}
