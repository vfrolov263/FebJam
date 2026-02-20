using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FebJam
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _description;

        public void Init(Sprite sprite, string name, string description)
        {
            _image.sprite = sprite;
            _name.text = name;
            _description.text = description;
        }
    }
}
