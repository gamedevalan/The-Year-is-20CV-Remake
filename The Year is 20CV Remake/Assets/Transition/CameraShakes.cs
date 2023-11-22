using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakes : MonoBehaviour
{
    public static CameraShakes instance;

    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Calculate a fake delta time, so we can Shake while game is paused.
        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;
    }

    public static void Shake()
    {
        instance._originalPos = instance.gameObject.transform.localPosition;
        instance.StopAllCoroutines();
        SoundEffectsBattle.hurtNormalStatic.Play();
        instance.StartCoroutine(instance.cShake(0.1f, 0.1f));
    }

    public static void CritShake()
    {
        instance._originalPos = instance.gameObject.transform.localPosition;
        instance.StopAllCoroutines();
        SoundEffectsBattle.hurtCriticalStatic.Play();
        instance.StartCoroutine(instance.cShake(0.3f, 0.25f));
    }

    public IEnumerator cShake(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= _fakeDelta;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}
