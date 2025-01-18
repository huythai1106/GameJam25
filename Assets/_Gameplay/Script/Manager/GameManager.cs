using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Layout")]
        public LayerMask layerPlayer;
        public LayerMask layerMonster;
        public LayerMask layerGround;
        public LayerMask layerGroundOneWay;
        public LayerMask layerBullet;

        public KeyboardSetting keyboardSetting;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Screen.orientation = ScreenOrientation.LandscapeRight;

            Physics2D.IgnoreLayerCollision(6, 10, true);
            Physics2D.IgnoreLayerCollision(10, 10, true);
        }

        private void OnDestroy()
        {
            Instance = null;
            Physics2D.IgnoreLayerCollision(6, 10, false);
            Physics2D.IgnoreLayerCollision(10, 10, false);
        }
    }
}
