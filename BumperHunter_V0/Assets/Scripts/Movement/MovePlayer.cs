using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MoveManualRB {

    [SerializeField] private float _turnSpeed;

    public override void Move(Vector3 dir)
    {
        base.Move(dir);

        transform.rotation = Quaternion.LookRotation(dir, transform.up);
    }
}