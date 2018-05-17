using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveManualRB))]
public class EMoveManualRB : Editor {

    private bool _hasParticleEffect;
    private bool _hasSoundEffect;

    private SerializedProperty _moveEffect;
    private SerializedProperty _runSound;
    private SerializedProperty _walkSound;

    private MoveManualRB _myTarget;
    private SerializedObject _soTarget;

    private void OnEnable()
    {
        _myTarget = (MoveManualRB)target;
        _soTarget = new SerializedObject(target);

        _moveEffect = _soTarget.FindProperty("_moveEffect");
        _runSound = _soTarget.FindProperty("_runSound");
        _walkSound = _soTarget.FindProperty("_walkSound");
    }

    public override void OnInspectorGUI()
    {
        _soTarget.Update();
        base.OnInspectorGUI();

        MoveManualRB moveScript = (MoveManualRB)target;

        GUILayout.Label("");
        GUILayout.Label("Effects", EditorStyles.boldLabel);



        // Particle Effects
        if (GUILayout.Toggle(_hasParticleEffect, "Has a Particle Effect?"))
        {
            _hasParticleEffect = true;
        }
        else
        {
            _hasParticleEffect = false;
        }

        if(_hasParticleEffect)
        {
            EditorGUILayout.PropertyField(_moveEffect);
        }



        // Sound Effects
        if (GUILayout.Toggle(_hasSoundEffect, "Has a Sound Effect?"))
        {
            _hasSoundEffect = true;

            if (_myTarget.gameObject.GetComponent<AudioSource>() == null)
                _myTarget.gameObject.AddComponent<AudioSource>();
        }
        else
        {
            _hasSoundEffect = false;
        }

        _soTarget.ApplyModifiedProperties();

        if (_hasSoundEffect)
        {
            EditorGUILayout.PropertyField(_runSound);
            EditorGUILayout.PropertyField(_walkSound);
        }
    }
}
