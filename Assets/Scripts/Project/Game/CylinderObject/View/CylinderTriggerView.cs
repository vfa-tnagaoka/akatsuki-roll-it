using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTriggerView : MonoBehaviour
{
    [SerializeField] CylinderView cylinder;
    private void OnTriggerEnter(Collider other)
    {
        this.cylinder.OnCylinderTriggerEnter(other);
    }
}
