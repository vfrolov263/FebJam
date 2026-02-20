using System;
using UnityEngine;

namespace FebJam
{
    /// <summary>
    /// Потенциально поменяется, пока это нужно для перехвата статуса диалога из DialogueSystem.
    /// </summary>
    public class DialogueModeSwitch : MonoBehaviour
    {
        public Action DialogueСompleted;       

        private void OnEnable()
        {
            //if (ServiceLocator.TryGetService(out GameplayManager gameplayManager))
            //{
            //    gameplayManager.ChangeState(GameplayState.Dialogue);
            //}
        }

        private void OnDisable()
        {
            //if (ServiceLocator.TryGetService(out GameplayManager gameplayManager))
            //{
            //    gameplayManager.ChangeState(GameplayState.Play);
            //}
        }
    }
}