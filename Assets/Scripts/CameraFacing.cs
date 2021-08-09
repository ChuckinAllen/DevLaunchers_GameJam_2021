using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
public class CameraFacing : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
            //transform.LookAt(Camera.main.transform.position, new Vector3(0, transform.position.y, transform.position.z));
        }
    }
}

