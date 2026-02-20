using UnityEngine;

namespace FebJam
{
    // Тестовый скрипт на диалоговых NPC - позже переделать или удалить и создать новый
    public class DialogNPC : MonoBehaviour
    {
        // Диалоговая фраза, с которой начинается общение
        public DialogueLine StartDialogLine;

        public void Interact()
        {
            StartDialog();
        }

        private void StartDialog()
        {
            if (StartDialogLine)
            {
                // Переходим в режим диалога, только если есть, что сказать
                ServiceLocator.GetService<DialogueManager>().StartDialogue(StartDialogLine);
            }
        }
    }
}
