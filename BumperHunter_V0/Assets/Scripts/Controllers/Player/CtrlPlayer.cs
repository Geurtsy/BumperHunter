using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CtrlPlayer : MonoBehaviour {

    public float Rage
    {
        get { return Rage; }
        set
        {
            if(value + Rage > 100.0f)
            {
                RageOut();
            }
        }
    }

    [HideInInspector] public float _stamina = 100.0f;
    [SerializeField] private VirtualJoystick _joystick;
    private IMove _movingMech;

    [SerializeField] private AudioSource _myAudioSource;
    [SerializeField] private AudioClip[] _rageSounds;
    [SerializeField] private AudioClip[] _bumperCollectSounds;

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

    private void OnTriggerEnter(Collider other)
    {
        var go = other.gameObject;

        if(go.tag == "Bumper")
        {
            Bumper bumper = go.GetComponent<Bumper>();
            Rage -= bumper._length;
            _myAudioSource.PlayOneShot(_bumperCollectSounds[Random.Range(0, _bumperCollectSounds.Length)]);
        }
    }

    private void RageOut()
    {
        _myAudioSource.PlayOneShot(_rageSounds[Random.Range(0, _bumperCollectSounds.Length)]);
    }
}
