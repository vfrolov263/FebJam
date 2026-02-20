using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FebJam
{
    public class AchievementsDatabase: IEnumerable<AchievementData>
    {
        [SerializeField]
        private List<AchievementData> _data = new();
        public event Action<AchievementData> Achieved;

        public void Add(AchievementData achievement)
        {
            if (!_data.Contains(achievement))
            {
                _data.Add(achievement);
                Achieved?.Invoke(achievement);
            }
        }

        public IEnumerator<AchievementData> GetEnumerator()
        {
            foreach (var achievement in _data)
                yield return achievement;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
