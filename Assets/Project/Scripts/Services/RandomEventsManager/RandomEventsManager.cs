using System;
using System.Collections;
using UnityEngine;

namespace FebJam
{
    public class RandomEventsManager
    {
        private float _timeBetweenEventsMin;
        private float _timeBetweenEventsMax;
        private MonoBehaviour _gameplayObject;
        private Coroutine _randRoutine;

        public event Action RandomEvent;

        public RandomEventsManager(MonoBehaviour gameplayObject, float timeBetweenEventsMin, float timeBetweenEventsMax)
        {
            _timeBetweenEventsMin = timeBetweenEventsMin;
            _timeBetweenEventsMax = timeBetweenEventsMax;
            _gameplayObject = gameplayObject;
            Start();
        }

        public void Pause()
        {
            if (_randRoutine != null)
            {
                _gameplayObject.StopCoroutine(_randRoutine);
            }
        }

        public void Start()
        {
            _randRoutine = _gameplayObject.StartCoroutine(NextEvent());
        }

        private IEnumerator NextEvent()
        {
            while (true)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(_timeBetweenEventsMin, _timeBetweenEventsMax));
                Debug.Log("Random event");
                RandomEvent?.Invoke();
            }
        }
    }
}