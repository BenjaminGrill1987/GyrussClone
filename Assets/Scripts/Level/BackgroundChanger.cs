using System.Collections.Generic;
using UnityEngine;
using GyroSpace.Utility;

namespace GyroSpace.Level
{

    public class BackgroundChanger : Singleton<BackgroundChanger>
    {
        [SerializeField] private List<Sprite> _backgrounds;

        private SpriteRenderer _image;

        void Start()
        {
            _image = GetComponent<SpriteRenderer>();
        }

        public static void ChangeBackGround()
        {
            _Instance._image.sprite = _Instance._backgrounds[Random.Range(0, _Instance._backgrounds.Count)];
        }
    }
}