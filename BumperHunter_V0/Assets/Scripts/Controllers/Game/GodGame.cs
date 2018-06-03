using UnityEngine;
using UnityEngine.UI;

public class GodGame : MonoBehaviour {

    // UI Elements.
    [SerializeField] private Slider _staminaSlider;
    [SerializeField] private Slider _rageSlider;

    private CtrlPlayer _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<CtrlPlayer>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    // Updates UI.
    private void UpdateUI()
    {
        _staminaSlider.value = _player.Stamina / 100;
        _rageSlider.value = _player.Rage / 100;
    }
}
