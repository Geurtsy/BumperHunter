using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CtrlPlayer : MonoBehaviour {

    private float _rage;
    private float _stamina;

    public float Rage
    {
        get { return _rage; }
        set
        {
            if(value + _rage > 100.0f)
            {
                RageOut();
                return;
            }

            _rage = value;
        }
    }

    public float Stamina
    {
        get { return _stamina; }
        set
        {
            if(value + _stamina > 100.0f)
            {
                _stamina = 100.0f;
                return;
            }
            else if(_stamina - value < 0.0f)
            {
                _stamina = 0.0f;
                // Play exhausted audio.
                // Start run cool down.
                return;
            }

            _stamina = value;
        }
    }

    [Header("Dependent Variables")]
    [SerializeField] private float _rageRate;
    [SerializeField] private float _staminaIncRate;

    [Header("Controls")]
    [SerializeField] private VirtualJoystick _joystick;
    private IMove _movingMech;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _rageSounds;
    [SerializeField] private AudioClip[] _bumperCollectSounds;
    [SerializeField] private AudioClip _exhaustedSound;
    private AudioSource _myAudioSource;

    private void Start()
    {
        _movingMech = GetComponent<IMove>();

        ResetValues();
    }

    private void Update()
    {
        IncreaseDependables();
    }

    private void FixedUpdate()
    {
        if (_joystick._inputVector != Vector3.zero)
            _movingMech.Move(new Vector3(_joystick.Horizontal(), 0, _joystick.Vertical()));
        else
        {
            Quaternion camRot = Camera.main.transform.rotation;
            //_joystick.Rotate(camRot);
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
            Destroy(go);
        }
    }

    // Controls what happens when rage meter reaches max.
    private void RageOut()
    {
        _myAudioSource.PlayOneShot(_rageSounds[Random.Range(0, _bumperCollectSounds.Length)]);
    }

    private void IncreaseDependables()
    {
        Rage += Time.deltaTime * _rageRate;
        Stamina += Time.deltaTime * _staminaIncRate;
    }

    // Reset values.
    private void ResetValues()
    {
        Stamina = 100.0f;
        Rage = 0.0f;
    }
}
