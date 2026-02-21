using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace FebJam
{
    [CreateAssetMenu(fileName = "ImagesData", menuName = "Scriptable Objects/ImagesData")]
    public class ImagesData : ScriptableObject
    {
        public List<Sprite> Faces;
        public Sprite Mustache, Mole, PoorEye;
        public List<Sprite> Hairs;
    }
}