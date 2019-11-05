using Extensions.UnityEngine;
using System;
using UnityEngine;

public class AutoRotateZ : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        this.transform.SetRotationEulerZ(Time.timeSinceLevelLoad * this.speed);
    }

}