using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTriggerView : MonoBehaviour
{
    [SerializeField] CylinderView cylinder;
    private void OnTriggerEnter(Collider other)
    {
        this.cylinder.OnTriggerEnter(other);
    }
}
