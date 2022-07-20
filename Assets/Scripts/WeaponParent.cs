using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponParent : MonoBehaviour
{

    
    public void SetAngle(Vector2 pos) {
        transform.right = pos.normalized;
    }
}
