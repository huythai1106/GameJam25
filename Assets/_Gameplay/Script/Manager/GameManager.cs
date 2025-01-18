using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParadoxGameStudio
{
    public enum GameState
    {
        InGame,
        PlayBoss,
        Die,
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState gameState = GameState.InGame;
        public Player player;

        [Header("Layout")]
        public LayerMask layerPlayer;
        public LayerMask layerMonster;
        public LayerMask layerGround;
        public LayerMask layerGroundOneWay;
        public LayerMask layerBullet;

        public AudioClip bossBGM;

        public KeyboardSetting keyboardSetting;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Application.targetFrameRate = 60;

            Physics2D.IgnoreLayerCollision(6, 9, true);
            Physics2D.IgnoreLayerCollision(9, 9, true);
            Physics2D.IgnoreLayerCollision(8, 10, true);
            Physics2D.IgnoreLayerCollision(10, 10, true);
        }

        private void OnDestroy()
        {
            Instance = null;
            Physics2D.IgnoreLayerCollision(6, 9, false);
            Physics2D.IgnoreLayerCollision(9, 9, false);
            Physics2D.IgnoreLayerCollision(8, 10, false);
            Physics2D.IgnoreLayerCollision(10, 10, false);
        }

        public void ResetScene()
        {
            // Get the current active scene
            Scene currentScene = SceneManager.GetActiveScene();
            // Reload the current scene
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
