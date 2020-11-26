using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDL.Core{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        float X = default;
        [SerializeField]
        float Y = default;
        [SerializeField]
        float Z = default;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(X, Y, Z);
        }
    }
}
