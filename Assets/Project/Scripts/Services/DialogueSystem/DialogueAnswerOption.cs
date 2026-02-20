using UnityEngine;

namespace FebJam
{
    [CreateAssetMenu(fileName = "DialogueAnswerOption", menuName = "Scriptable Objects/DialogueAnswerOption")]
    public class DialogueAnswerOption : ScriptableObject
    {
        public string Text;
        public DialogueLine DialogueLine;
    }
}
