using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace ParadoxGameStudio
{

    public class GameplayUIController : MonoBehaviour
    {
        [SerializeField] private HealthUIManager healthUIManager;
        [SerializeField] private ManaUIManager manaUIManager;
        [SerializeField] private InputControllerBase inputController;

        [SerializeField] private Button ultimateButton;


        private bool isHoldingUltimateButton = false;
        private float ultimateHoldingTime;
        [SerializeField] private float timeToCastSecondUltimate = 1f;

        [SerializeField] private UnityEvent onBaseUltimateCast;
        [SerializeField] private UnityEvent onSecondUltimateCast;
        [SerializeField] private UnityEvent onJumpButtonClicked;
        private void Update()
        {
            if (isHoldingUltimateButton)
            {
                ultimateHoldingTime += Time.deltaTime;
            }
        }

        public void OnUltimateButtonDown()
        {
            print("Ultimate button down");
            isHoldingUltimateButton = true;
        }

        public void OnUltimateButtonUp()
        {
            isHoldingUltimateButton = false;

            if (ultimateHoldingTime > timeToCastSecondUltimate)
            {
                print("Second ultimate cast");
                onBaseUltimateCast?.Invoke();
            }
            else
            {
                print("Base ultimate cast");
                onSecondUltimateCast?.Invoke();
            }
        }

        public void OnJumpButtonClicked()
        {
            print("Jump button clicked");
            onJumpButtonClicked?.Invoke();
        }

        public void OnAttackButtonClicked()
        {
            print("Jump button clicked");
            onJumpButtonClicked?.Invoke();
        }
    }
}
