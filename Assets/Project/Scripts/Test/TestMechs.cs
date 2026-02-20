using UnityEngine;

namespace FebJam
{
    public class TestMechs : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _audioClip;
        private void Awake()
        {
            ServiceLocator.GetService<ResourcesSet>().AddResource("gold", 40f);
            ServiceLocator.GetService<ResourcesSet>().AddResource("gold", -100f);
            ServiceLocator.GetService<ResourcesSet>().AddResource("mana", 50f);
            ServiceLocator.GetService<MusicManager>().PlayMusic(_audioClip);
        }
    }
}