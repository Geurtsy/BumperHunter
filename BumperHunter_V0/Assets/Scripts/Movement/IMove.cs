using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove {

    float MasterSpeed { get; set; }

    void Move(Vector3 dir);
}
