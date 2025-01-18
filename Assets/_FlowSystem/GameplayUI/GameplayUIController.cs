using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace ParadoxGameStudio
{

    public class GameplayUIController : MonoBehaviour
    {
        private bool isHoldingUltimateButton = false;
        private bool isHoldingAttackButton = false;
        private float ultimateHoldingTime;
        private float attackHoldingTime;
        private bool isReachedThreshholdAttack;
        [SerializeField] private float timeToCastSecondUltimate = 0.5f;
        [SerializeField] private float timeToCastSecondAttack = 0.5f;

        [SerializeField] private UnityEvent onBaseUltimateCast;
        [SerializeField] private UnityEvent onSecondUltimateCast;
        [SerializeField] private UnityEvent onJumpButtonClicked;
        [SerializeField] private UnityEvent onBaseAttackCast;
        [SerializeField] private UnityEvent onSecondAttackCast;
        [SerializeField] private UnityEvent OnReachThreshholdAttack;


        private void Update()
        {
            if (isHoldingUltimateButton)
            {
                ultimateHoldingTime += Time.deltaTime;
            }
            if (isHoldingAttackButton)
            {
                attackHoldingTime += Time.deltaTime;
                if (attackHoldingTime - timeToCastSecondAttack >= 0 && isReachedThreshholdAttack == false)
                {
                    isReachedThreshholdAttack = true;
                    OnReachThreshholdAttack?.Invoke();
                }
            }
        }

        public void OnUltimateButtonDown()
        {
            print("Ultimate button down");
            isHoldingUltimateButton = true;
        }
        public void OnAttackButtonDown()
        {
            print("Attack button down");
            isHoldingAttackButton = true;
        }

        public void OnUltimateButtonUp()
        {
            isHoldingUltimateButton = false;

            if (ultimateHoldingTime > timeToCastSecondUltimate)
            {
                print("Second ultimate cast");
                onSecondUltimateCast?.Invoke();
            }
            else
            {
                onBaseUltimateCast?.Invoke();
                print("Base ultimate cast");
            }

            ultimateHoldingTime = 0;
        }
        public void OnAttackButtonUp()
        {


            if (attackHoldingTime > timeToCastSecondAttack)
            {
                print("Second attack cast");
                onSecondAttackCast?.Invoke();
            }
            else
            {
                print("Base attack cast");
                onBaseAttackCast?.Invoke();
            }
            attackHoldingTime = 0;
            isReachedThreshholdAttack = false;

        }

        public void OnJumpButtonClicked()
        {
            print("Jump button clicked");
            onJumpButtonClicked?.Invoke();
        }
    }
}
