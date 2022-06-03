using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamera : EnemyBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxMoveAngle;

    public override void SeekMovement()
    {
        base.SeekMovement();

        float force = moveSpeed * Time.deltaTime;

        if (Mathf.Abs(force + transform.rotation.z) > maxMoveAngle)
            force = Mathf.Clamp(maxMoveAngle - Mathf.Abs(transform.rotation.z), 0, maxMoveAngle) * Time.deltaTime;
        Debug.Log(force);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + force);
    }

}
