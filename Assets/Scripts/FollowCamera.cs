using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : NetworkCameraBehavior
    {
        [SerializeField] float DistanceFromTarget;
        [SerializeField] Transform target;

        void LateUpdate()
        {
            //transform.position = target.position + new Vector3(0, DistanceFromTarget, 0);
            if (networkObject.IsOwner)
            {
                transform.position = GameObject.FindGameObjectWithTag("Player").transform.position +
                new Vector3(0, DistanceFromTarget, 0);
                //transform.position = target.position + new Vector3(0, DistanceFromTarget, 0);
            }
            transform.parent = null;
        }
	}
}

