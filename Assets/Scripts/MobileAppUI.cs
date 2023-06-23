using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MobileApp.UI
{
    public class MobileAppUI : MonoBehaviour
    {
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject chooseOption;
        [SerializeField] GameObject openCamera;
        [SerializeField] GameObject modal;

        // Main Events

        private void OpenOptionsWindow()
        {
            chooseOption.SetActive(true);
        }

        private void OpenCameraWindow()
        {
            openCamera.SetActive(true);
        }

        public void OpenModal()
        {
            modal.SetActive(true);
        }

        private void OnEnable()
        {
            UIMenu.openOptsWindow += OpenOptionsWindow;
            UIMenu.openCamWindow += OpenCameraWindow;
            UIChooseOption.launchModal += OpenModal;
        }
    }
}
