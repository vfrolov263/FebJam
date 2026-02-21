
using System;
using TMPro;
using UnityEngine;

namespace FebJam
{
	public class CharacterDialogue: MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _speach;

		public event Action<bool> DecisionMade;

		public bool HasDecision { get; private set; }

		public void Init(string speach)
		{
            _speach.text = speach;
        }

		public void MakeDecision(bool pass)
		{
			HasDecision = true;
            DecisionMade?.Invoke(pass);
        }

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			HasDecision = false;
            gameObject.SetActive(false);
		}
	}
}