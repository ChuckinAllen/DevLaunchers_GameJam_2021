using UnityEngine;
using RPG.Combat;
using RPG.Attrbutes;
using RPG.Movement;
using RPG.Core;
using GameDevTV.Utils;

namespace RPG.Control
{
    [DisallowMultipleComponent]
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float aggroCooldownTime = 5f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float shoutDistance = 5f;
        [SerializeField] bool canShout = true;

        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        LazyValue<Vector3> gardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        float timeSinceAggrovated = Mathf.Infinity;
        float timesinceShouted = Mathf.Infinity;
        int currentWaypointIndex = 0;


        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            gardPosition = new LazyValue<Vector3>(GetGuardPosition);
        }


        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
            gardPosition.ForceInit();
        }


        private void Update()
        {
            if (health.IsDead()) return;

            if (IsAggrevated() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspitionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            timeSinceAggrovated += Time.deltaTime;
            timesinceShouted += Time.deltaTime;
        }

        public void AggrevateAllies()
        {
            timeSinceAggrovated = 0;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = gardPosition.value;

            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distaceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distaceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void SuspitionBehaviour()
        {
            GetComponent<ActionScheduler>().CancleCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);

            if(canShout)
            {
                AggrevateNerbyEnemies();
                canShout = false;
            }
        }

        private void AggrevateNerbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position,shoutDistance,Vector3.up,0);
            foreach (RaycastHit hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if(ai == null || ai == this) continue;
                ai.AggrevateAllies();
            }
        }

        private bool IsAggrevated()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance || timeSinceAggrovated < aggroCooldownTime;
        }

        // Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }//Bug ai does not go back to garding position if you move to diffrant scean
}    //To fix this make the ai keep track of the gard position when game started and save that as well
