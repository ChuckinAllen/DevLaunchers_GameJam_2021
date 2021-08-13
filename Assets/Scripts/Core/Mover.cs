using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.AI;
using RPG.Attrbutes;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : PlayerCubeBehavior, IAction
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxNavPathLength = 40f;

        NavMeshAgent navMeshAgent;

        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void LateUpdate()
        {
            // If this is not owned by the current network client then it needs to
            // assign it to the position and rotation specified
            if (!networkObject.IsOwner)
            {
                // Assign the position of this cube to the position sent on the network
                transform.position = networkObject.position;

                // Assign the rotation of this cube to the rotation sent on the network
                transform.rotation = networkObject.rotation;

                // Stop the function here and don't run any more code in this function
                return;
            }
            navMeshAgent.enabled = !health.IsDead();
            UpdateAmimator();

            // Since we are the owner, tell the network the updated position
            networkObject.position = transform.position;

            // Since we are the owner, tell the network the updated rotation
            networkObject.rotation = transform.rotation;
            //networkObject.SendRpc("HelloWorldworld", Receivers.AllBuffered);
        }


        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;
            return true;
        }

        //this is like an interface so that can be can be called from other places
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            //Debug.Log("Destnation is null or something");
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAmimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 LocalVelocity = transform.InverseTransformDirection(velocity);
            float speed = LocalVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return total;
        }
    }
}
