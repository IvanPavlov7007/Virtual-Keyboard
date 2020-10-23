using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LightingKey))]
public class PhysicalKeyResponse : MonoBehaviour
{
    [SerializeField]
    Vector3 pressedDirection;
    [SerializeField]
    float pressAnimationSpeed = 20f, releaseAnimationSpeed = 60f,
        inclineFrameY = 5f, inclineFrameZMultipluier = 3f;

    [SerializeField]
    bool controlledByAxis = false;

    [SerializeField]
    AudioClip pressSound, releaseSound;

    Vector3 pressedPosition;

    bool pressed;
    Coroutine pressAnimationCoroutine;
    Quaternion endRotation;

    //Box-edges in its local space
    const float maxX = 0.5f;
    const float maxZ = 0.5f;

    LightingKey lightingKey;
    protected void Awake()
    {
        pressedPosition = transform.localPosition + pressedDirection;
    }

    protected void Start()
    {
        lightingKey = GetComponent<LightingKey>();
    }

    //Some work in progress
    //public void controlByAxis(float axisValue)
    //{
    //    if (!pressed && axisValue > 0)
    //    {
    //        controlledByAxis = true;

    //        Vector3 pressPoint = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
    //        Press(pressPoint);
    //    }
    //    else if (controlledByAxis && pressed && axisValue == 0)
    //    {
    //        pressed = false;
    //        controlledByAxis = false;
    //    }
    //}

    public void Press(Vector3 point)
    {
        //base.Press(point);
        //aud.PlayOneShot(pressSound);
        calculateEndRotation(point);
        if (pressAnimationCoroutine == null)
            pressAnimationCoroutine = StartCoroutine(PressAnimation());
        else
        {
            pressed = true;
        }
    }

    public void Press()
    {
        lightingKey.StimulateCenter();
        Press(transform.up);
    }

    public void Release()
    {
        if (pressed)
        {
            pressed = false;
        }
    }

    //Some work in progress

    //protected override void handleMouseUp(RaycastHit[] hits)
    //{
    //    base.handleMouseUp(hits);
    //    if (pressed)
    //    {
    //        aud.PlayOneShot(releaseSound);
    //        pressed = false;
    //    }
    //}

    //protected override void handleNotAlteredRaycast(RaycastHit[] hits)
    //{
    //    base.handleNotAlteredRaycast(hits);
    //    if (controlledByAxis)
    //        return;
    //    if (pressed)
    //    {
    //        RaycastHit hit = System.Array.Find(hits, obj => obj.transform == transform);
    //        if (hit.transform != null)
    //        {
    //            calculateEndRotation(transform.InverseTransformPoint(hit.point));
    //        }
    //        else
    //            pressed = false;
    //    }
    //}

    private void calculateEndRotation(Vector3 pressPoint)
    {
        pressPoint.y += inclineFrameY;
        pressPoint.z *= inclineFrameZMultipluier;
        endRotation = transform.rotation * Quaternion.FromToRotation(transform.up * inclineFrameY, pressPoint);
    }

    IEnumerator PressAnimation()
    {
        float animationState = 0;
        Quaternion defaultRotation = transform.localRotation;
        Vector3 defaultPosition = transform.localPosition;
        pressed = true;
        do
        {
            animationState += (pressed ? Time.deltaTime * pressAnimationSpeed : -Time.deltaTime * releaseAnimationSpeed);
            animationState = Mathf.Min(2f, animationState);
            transform.localRotation = Quaternion.Slerp(defaultRotation, endRotation, animationState);
            transform.localPosition = Vector3.Lerp(defaultPosition, pressedPosition, Mathf.Clamp(animationState, 1f, 2f) - 1f);
            yield return new WaitForEndOfFrame();
        } while (animationState > 0);
        pressAnimationCoroutine = null;
        yield return 0;
    }
}
