using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace FebJam
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField]
        private float _startupDelay = 2f;
        [SerializeField]
        private float _timeBetweenVisitors = 2f;
        [SerializeField]
        private Visitor _visitorPrefab;

        [SerializeField]
        private TMP_Text _levelText, _expText;

        [SerializeField]
        private GameObject[] _fails = new GameObject[3];

        [Header("Achievements")]
        [SerializeField]
        AchievementData _detectBadGuy, _wrongDetectBadGuy, _loseGame, _get2Lvl, _get3Lvl, _winGame;

        private ImagesData _imagesData;
        private Visitor _visitor;
        private CharacterDialogue _dialogue;
        private Papers _papers;
        private AchievementsDatabase _achievementsDB;

        private Array _weights, _lengths, _signs;


        private int _level = 1;
        private int _exp = 0;
        private const int LEVEL_UP_EXP = 3;
        private const int MAX_LEVEL = 4;
        private int _lifes = 3;

        private Vector3 _shortPosition = new(-4.8f, -1.8f, 0f),
            _middlePosition = new (-4.8f, 0f, 0f),
            _longPosition = new(-4.8f, 2f, 0f);

        public void Init(CharacterDialogue dialogue, Papers papers, ImagesData imagesData, AchievementsDatabase achievementsDB)
        {
            _dialogue = dialogue;
            _dialogue.Hide();
            _dialogue.DecisionMade += OnDecisionMade;
            _imagesData = imagesData;
            _papers = papers;
            _papers.Hide();
            _weights = Enum.GetValues(typeof(Weight));
            _lengths = Enum.GetValues(typeof(Length));
            _signs = Enum.GetValues(typeof(CharacterSign));
            _achievementsDB = achievementsDB;
        }

        public void Start()
        {
            StartCoroutine(VisitRoutine());
        }

        private void OnDestroy()
        {
            if (_dialogue != null )
                _dialogue.DecisionMade -= OnDecisionMade;
        }

        private IEnumerator VisitRoutine()
        {
            yield return new WaitForSeconds(_startupDelay);

            while (true)
            {
                CreateVisitor();
                yield return new WaitForSeconds(_visitor.TimeIn);
                ShowDialogue();
                ShowPapers();
                yield return new WaitUntil(() => _dialogue.HasDecision);
                _dialogue.Hide();
                _papers.Hide();
                yield return new WaitForSeconds(_timeBetweenVisitors);
            }
        }

        private void CreateVisitor()
        {
            if (_visitor != null)
                Destroy(_visitor.gameObject);

            _visitor = Instantiate(_visitorPrefab);
            string name = GameStrings.Names[UnityEngine.Random.Range(0, GameStrings.Names.Length)];
            string speach = GameStrings.Speaches[UnityEngine.Random.Range(0, GameStrings.Speaches.Length)];
            Sprite face = _imagesData.Faces[UnityEngine.Random.Range(0, _imagesData.Faces.Count)];
            Sprite hair = _imagesData.Hairs[UnityEngine.Random.Range(0, _imagesData.Hairs.Count)];
            Weight weight = (Weight)_weights.GetValue(UnityEngine.Random.Range(0, _weights.Length));
            Length length = (Length)_lengths.GetValue(UnityEngine.Random.Range(0, _lengths.Length));
            CharacterSign sign = (CharacterSign)_signs.GetValue(UnityEngine.Random.Range(0, _signs.Length));
            Sprite signSprite = sign switch
            {
                CharacterSign.Mole => _imagesData.Mole,
                CharacterSign.PoorEye => _imagesData.PoorEye,
                CharacterSign.Mustache => _imagesData.Mustache,
                _ => null
            };

            _visitor.Init(name, speach, face, hair, weight, length, sign, signSprite);

            _visitor.gameObject.transform.position = length switch
            {
                Length.Short => _shortPosition,
                Length.Middle => _middlePosition,
                Length.Tall => _longPosition,
                _ => Vector3.zero
            };

            Debug.Log($"Make visitor: {length}, {weight}, {sign}");
        }

        private void ShowPapers()
        {
            UnityEngine.Random.Range(0, 100);

            Length length = _visitor.Length;
            Weight weight = _visitor.Weight;
            CharacterSign sign = _visitor.Sign;

            if (UnityEngine.Random.Range(0, 100) > 60)
            {
                int paramToLie = UnityEngine.Random.Range(0, Math.Min(_level + 1, 3));

                switch (paramToLie)
                {
                    case 0:
                        length = GetWrongLenth(length);
                        break;
                    case 1:
                        weight = GetWrongWeight(weight);
                        break;
                    default:
                        sign = GetWrongSign(sign);
                        break;
                }                 
            }

            if (_level == 1)
            {
                _papers.Init(_visitor.Name, length);
                _papers.Show();
            }
            else if (_level == 2)
            {
                _papers.Init(_visitor.Name, length, weight);
                _papers.Show();
            }
            else
            {
                _papers.Init(_visitor.Name, length, weight, sign);
                _papers.Show();
            }
        }

        private Length GetWrongLenth(Length correct)
        {
            Length length;

            do
            {
                length = (Length)_lengths.GetValue(UnityEngine.Random.Range(0, _lengths.Length));
            } while (length == correct);

            return length;
        }

        private Weight GetWrongWeight(Weight correct)
        {
            Weight weight;

            do
            {
                weight = (Weight)_weights.GetValue(UnityEngine.Random.Range(0, _weights.Length));
            } while (weight == correct);

            return weight;
        }

        private CharacterSign GetWrongSign(CharacterSign correct)
        {
            CharacterSign sign;

            do
            {
                sign = (CharacterSign)_signs.GetValue(UnityEngine.Random.Range(0, _signs.Length));
            } while (sign == correct);

            return sign;
        }

        private void ShowDialogue()
        {
            _dialogue.Init(_visitor.Speach);
            _dialogue.Show();
        }

        private void OnDecisionMade(bool pass)
        {
            _visitor.TellDecision(pass);

            if (IsCorrectDecision(pass))
            {
                Debug.Log($"Correct!");
                _exp++;

                if (!pass)
                {
                    Achieve(ref _detectBadGuy);
                }

                if (_exp >= LEVEL_UP_EXP)
                {
                    _exp = 0;
                    _level++;

                    if (_level == 2)
                    {
                        Achieve(ref _get2Lvl);
                    }
                    else if (_level == 3)
                    {
                        Achieve(ref _get3Lvl);
                    }
                }

                if (_level >= MAX_LEVEL)
                {
                    Win();
                }
            }
            else
            {
                Debug.Log($"Not correct!");
                _lifes--;

                if (!pass)
                {
                    Achieve(ref _wrongDetectBadGuy);
                }

                int failId = Math.Clamp(3 - _lifes - 1, 0, 2);
                _fails[failId].SetActive(true);

                if (_lifes <= 0)
                    GameOver();
            }

            _expText.text = $"Опыт: {_exp}";
            _levelText.text = $"Ур.: {_level}";
        }

        private bool IsCorrectDecision(bool pass)
        {
            bool lvl1Pass = _visitor.Length == _papers.Length;
            bool lvl2Pass = _visitor.Weight == _papers.Weight;
            bool lvl3Pass = _visitor.Sign == _papers.Sign;

            bool papersCorrect =  _level switch
            {
                1 => lvl1Pass,
                2 => lvl1Pass && lvl2Pass,
                _ => lvl1Pass && lvl2Pass && lvl3Pass
            };

            return papersCorrect == pass;
        }

        private void GameOver()
        {
            Debug.Log("GameOver");
            Achieve(ref _loseGame);
            StartCoroutine(EndGame("Fail"));
        }

        private void Win()
        {
            Debug.Log("Win");
            Achieve(ref _winGame);
            StartCoroutine(EndGame("Win"));
        }

        private void Achieve(ref AchievementData achievement)
        {
            if (achievement != null)
            {
                _achievementsDB.Add(achievement);
                achievement = null;
            }
        }

        private IEnumerator EndGame(string nameScene)
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(nameScene);
        }
    }
}

// рост
// рост вес
// визуал лица