using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    [SerializeField] private float _restraintFactor;
    private Image _jsContainerImg;
    private Image _jsImg;
    [HideInInspector] public Vector3 _inputVector;

    private void Start()
    {
        _jsContainerImg = GetComponent<Image>();
        _jsImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData pEventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_jsContainerImg.rectTransform
            ,pEventData.position, pEventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _jsContainerImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _jsContainerImg.rectTransform.sizeDelta.x);

            _inputVector = new Vector3((pos.x * 2) + 1, 0, (pos.y * 2) - 1);

            if (_inputVector.magnitude > 1.0f)
                _inputVector = _inputVector.normalized;

            _jsImg.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (_jsContainerImg.rectTransform.sizeDelta.x / _restraintFactor),
                _inputVector.z * _jsContainerImg.rectTransform.sizeDelta.y / _restraintFactor);
        }
    }

    public void OnPointerDown(PointerEventData pEventData)
    {
        OnDrag(pEventData);
    }

    public void OnPointerUp(PointerEventData pEventData)
    {
        _inputVector = Vector3.zero;
        _jsImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (_inputVector.x != 0)
            return _inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (_inputVector.z != 0)
            return _inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }

    //public void Rotate(Quaternion rot)
    //{
    //    float angle = rot.eulerAngles.y;
    //    gameObject.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    //}
}
