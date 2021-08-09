using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action) //subitution of mono mmaks it say that this is the same thing as anything that is a mono
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
        }

        public void CancleCurrentAction()
        {
            StartAction(null);
        }
    }
}
