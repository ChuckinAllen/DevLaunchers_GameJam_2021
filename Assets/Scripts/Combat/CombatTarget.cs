using UnityEngine;
using RPG.Attrbutes;
using RPG.Control;

namespace RPG.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        bool IRaycastable.HandleRaycast(PlayerController callingController)
        {
            Debug.Log("enter");
            if (!enabled) return false;

            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                //var activePlayer = GameObject.FindGameObjectWithTag("Player");
                //Debug.Log(callingController.name + " " + activePlayer.name);
                /*if (callingController.gameObject == activePlayer)
                {
                    return false;
                }*/
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }
            return true;
        }
    }
}

