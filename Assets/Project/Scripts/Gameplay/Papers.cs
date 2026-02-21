using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace FebJam
{
    public enum Weight
    {
        Easy,
        Middle,
        Heavy
    }

    public enum Length
    {
        Short,
        Middle,
        Tall
    }

    public enum CharacterSign
    {
        PoorEye,
        Mole,
        Mustache
    }

    public class Papers : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name, _lenght, _weight, _sign;

        private const string LENGTH_ENTRY = "Рост: ",
        LENGTH_SHORT = "низкий",
        LENGTH_MIDDLE = "средний",
        LENGTH_TALL = "высокий";

        private const string WEIGHT_ENTRY = "Вес: ",
        WEIGHT_EASY = "легкий",
        WEIGHT_MIDDLE = "средний",
        WEIGHT_HEAVY = "тяжелый";

        private const string SIGN_ENTRY = "Особые приметы: ",
        SIGN_POOR_EYE = "плохое зрение",
        SIGN_MOLE = "родинка",
        SIGN_MUSTACHE = "усы";

        public Weight Weight { get; private set; }
        public Length Length { get; private set; }
        public CharacterSign Sign { get; private set; }


        public void Init(string name, Length length)
        {
            Clear();
            _name.text = name;
            SetLength(length);

        }

        public void Init(string name, Length length, Weight weight)
        {
            Init(name, length);
            SetWeight(weight);
        }

        public void Init(string name, Length length, Weight weight, CharacterSign sign)
        {
            Init(name, length, weight);
            SetSign(sign);
        }

        private void Clear()
        {
            _name.text = "";
            _lenght.text = "";
            _weight.text = "";
            _sign.text = "";
        }

        private void SetLength(Length length)
        {
            Length = length;

            switch (length)
            {
                case Length.Short:
                    _lenght.text = LENGTH_ENTRY + LENGTH_SHORT;
                    break;
                case Length.Middle:
                    _lenght.text = LENGTH_ENTRY + LENGTH_MIDDLE;
                    break;
                case Length.Tall:
                    _lenght.text = LENGTH_ENTRY + LENGTH_TALL;
                    break;
            }
        }

        private void SetWeight(Weight weight)
        {
            Weight = weight;

            switch (weight)
            {
                case Weight.Easy:
                    _weight.text = WEIGHT_ENTRY + WEIGHT_EASY;
                    break;
                case Weight.Middle:
                    _weight.text = WEIGHT_ENTRY + WEIGHT_MIDDLE;
                    break;
                case Weight.Heavy:
                    _weight.text = WEIGHT_ENTRY + WEIGHT_HEAVY;
                    break;
            }
        }

        private void SetSign(CharacterSign sign)
        {
            Sign = sign;

            switch (sign)
            {
                case CharacterSign.PoorEye:
                    _sign.text = SIGN_ENTRY + SIGN_POOR_EYE;
                    break;
                case CharacterSign.Mole:
                    _sign.text = SIGN_ENTRY + SIGN_MOLE;
                    break;
                case CharacterSign.Mustache:
                    _sign.text = SIGN_ENTRY + SIGN_MUSTACHE;
                    break;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

// рост
// рост вес
// рост вес визуал