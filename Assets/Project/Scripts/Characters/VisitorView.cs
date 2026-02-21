using System.Collections.Generic;
using UnityEngine;

namespace FebJam
{
    public class Visitor : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _face, _hair, _sign;
        [SerializeField]
        private Transform _body;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private float _timeIn, _timeOut, _timeEnter;

        public string Name { get; private set; }
        public Weight Weight { get; private set; }
        public Length Length { get; private set; }
        public CharacterSign Sign { get; private set; }

        // private void 

        public void Init(string name, string speach, Sprite face, Sprite hair,
            Weight weight, Length length, CharacterSign sign, Sprite signSprite)
        {
            _face.sprite = face;
            _hair.sprite = hair;
            _sign.sprite = signSprite;
            Name = name;
            Weight = weight;
            Length = length;
            Sign = sign;
            Speach = speach;

            Vector3 scale = _body.localScale;
            scale.x = weight switch
            {
                Weight.Easy => 0.5f,
                Weight.Middle => 0.9f,
                Weight.Heavy => 1.2f,
                _ => 1f
            };
            _body.localScale = scale;
        }

        public void TellDecision(bool pass)
        {
            _animator.SetTrigger(pass ? "Enter" : "Out");
        }

        public float TimeIn => _timeIn;
        public float TimeOut => _timeOut;
        public float TimeEnter => _timeEnter;
        public string Speach {  get; private set; }
    }
}