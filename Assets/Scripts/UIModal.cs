using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MobileApp.UI
{
    public class UIModal : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Button acceptButton;
        [SerializeField] Button denyButton;

        // Events
        public static event Action<string> modalAccept;
        public static event Action<string> modalDeny;

        void LaunchAccept()
        {
            modalAccept?.Invoke("You Chose Yes");
            gameObject.SetActive(false);
        }

        private void LaunchDeny()
        {
            modalDeny?.Invoke("You Chose No");
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            acceptButton.onClick.AddListener(LaunchAccept);
            denyButton.onClick.AddListener(LaunchDeny);
        }

        private void OnDisable()
        {
            acceptButton.onClick.RemoveAllListeners();
            denyButton.onClick.RemoveAllListeners();
        }

    }
}
