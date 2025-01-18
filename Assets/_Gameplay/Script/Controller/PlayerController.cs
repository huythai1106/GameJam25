using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParadoxGameStudio
{
    public class PlayerController : ControllerBase
    {
        private KeyboardSetting.KeyBoard _keyBoard;
        private Player playerCharacter;

        protected override void Start()
        {
            base.Start();
            playerCharacter = character as Player;
            _keyBoard = GameManager.Instance.keyboardSetting.playerKeyboard;
        }

        public void SetRotation(Vector2 v)
        {
            playerCharacter.movement.rotate = v;
        }

        protected override void HandleControll()
        {
            if (Input.GetKeyDown(_keyBoard.left))
            {
                playerCharacter.movement.rotate = Vector2.left;
            }
            else if (Input.GetKeyUp(_keyBoard.left))
            {
                playerCharacter.movement.rotate = Vector2.zero;
            }

            if (Input.GetKeyDown(_keyBoard.right))
            {
                playerCharacter.movement.rotate = Vector2.right;
            }
            else if (Input.GetKeyUp(_keyBoard.right))
            {
                playerCharacter.movement.rotate = Vector2.zero;
            }

            if (Input.GetKeyDown(_keyBoard.up))
            {
                playerCharacter.movement.HandleJump();
            }

            if (Input.GetKeyDown(_keyBoard.down))
            {
                playerCharacter.ButtonDown();
            }
        }
    }

}
