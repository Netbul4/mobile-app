using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MobileApp.UI
{
    public class UIChooseOption : MonoBehaviour
    {
        [SerializeField] MobileAppUI mobileApp;
        [SerializeField] Button launchModalButton;
        [SerializeField] Button backButton;
        [SerializeField] TextMeshProUGUI messageText;

        public static event Action launchModal;

        private void OpenModal()
        {
            launchModal?.Invoke();
        }

        private void DisplayMessage(string message)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
        }

        private void Back()
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
            mobileApp.ActivateMenu();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            launchModalButton.onClick.AddListener(OpenModal);
            backButton.onClick.AddListener(Back);
            UIModal.modalAccept += DisplayMessage;
            UIModal.modalDeny += DisplayMessage;
        }

        private void OnDisable()
        {
            launchModalButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
            UIModal.modalAccept -= DisplayMessage;
            UIModal.modalDeny -= DisplayMessage;

        }
    }
}
