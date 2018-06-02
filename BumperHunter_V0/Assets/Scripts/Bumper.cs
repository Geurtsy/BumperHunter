using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(Collider))]
public class Bumper : MonoBehaviour
{

    public float _length;
    [SerializeField] private bool _preset;
    private Light _light;
    private Renderer _haloRenderer;

    [SerializeField] private Color _minBumperColour;
    [SerializeField] private Color _maxBumperColour;

    private void Start()
    {
        _light = GetComponent<Light>();
        _haloRenderer = transform.GetChild(0).GetComponent<Renderer>();

        if(!_preset)
        {
            _length = Random.Range(1.0f, 100.0f);
        }

        UpdateColours();
    }

    private void UpdateColours()
    {
        Color newColor = Color.Lerp(_minBumperColour, _maxBumperColour, _length / 100);
        _light.color = newColor;
        _haloRenderer.material.color = newColor + new Color(0, 0, 0, 0.3f);
    }
}
