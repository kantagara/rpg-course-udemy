using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _currentAction;
        public void StartAction(IAction action)
        {
            if(action== _currentAction) return;
            if(_currentAction != null) _currentAction.Cancel();
            _currentAction = action;
        }
    }
}