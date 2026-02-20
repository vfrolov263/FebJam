using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FebJam
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _value;

        public void Init(Sprite sprite, string name, float value)
        {
            _image.sprite = sprite;
            _name.text = name;
            _value.text = value.ToString();
        }
    }
}