using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CtrlPlayer : MonoBehaviour {

    [SerializeField] private VirtualJoystick _joystick;
    private IMove _movingMech;

    private void Start()
    {
        _movingMech = GetComponent<IMove>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (_joystick._inputVector != Vector3.zero)
            _movingMech.Move(new Vector3(_joystick.Horizontal(), 0, _joystick.Vertical()));
        else
        {
            Quaternion camRot = Camera.main.transform.rotation;
            _joystick.Rotate(camRot);
        }
    }
}
