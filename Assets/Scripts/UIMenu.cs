using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace MobileApp.UI
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] Button chooseOptButton;
        [SerializeField] Button openCamButton;

        // Events
        public static event Action openOptsWindow;
        public static event Action openCamWindow;

        private void LaunchOptionsWindow()
        {
            openOptsWindow?.Invoke();
        }

        private void OpenCamButton()
        {
            openCamWindow?.Invoke();
        }

        private void OnEnable()
        {
            chooseOptButton.onClick.AddListener(LaunchOptionsWindow);
            openCamButton.onClick.AddListener(OpenCamButton);
        }

        private void OnDisable()
        {
            chooseOptButton.onClick.RemoveAllListeners();
            openCamButton.onClick.RemoveAllListeners();
        }
    }

}