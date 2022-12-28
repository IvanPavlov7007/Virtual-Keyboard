using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteIndicator : MonoBehaviour
{
    Transform piece;
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    public void ShowIndicator(Transform piece)
    {
        this.piece = piece;
        gameObject.SetActive(true);
    }

    public void HideIndicator()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        transform.position = CommonTools.yPlaneVector(piece.position, transform.position.y);
        transform.LookAt(cam);
    }
}
