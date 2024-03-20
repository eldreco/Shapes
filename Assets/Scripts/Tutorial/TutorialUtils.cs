using UnityEngine;

namespace TutorialUtils
{

    public interface IStage
    {
        public string GetNeededMovement();
        public void ShowStageInstructions();
        public void BeginStage();
        public void ShowInstructions();
        public void HideInstructions();
    }
}