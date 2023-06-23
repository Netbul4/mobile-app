using MobileApp.UI;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

namespace MobileApp
{
    public class OpenCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] MobileAppUI mobileApp;
        [SerializeField] RawImage background;
        [SerializeField] Image camLogo;
        [SerializeField] AspectRatioFitter fit;
        [SerializeField] Button takePhotoButton;
        [SerializeField] Button backButton;

        // Non-In-Inspector References
        private bool camAvaible;
        private WebCamTexture backCam;
        private Texture defaultBackground;

        private void Start()
        {
            defaultBackground = background.texture;
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.LogError("No Camera Detected");
                camAvaible = false;
                return;
            }

            for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                {
                    backCam = new WebCamTexture(devices[i].name,
                    Screen.width, Screen.height);
                }
            }

            if (backCam == null)
            {
                Debug.LogError("No Back Camera Detected!");
                background.texture = defaultBackground;
                return;
            }

            camLogo.gameObject.SetActive(false);
            backCam.Play();
            background.texture = backCam;
            background.color = Color.white;

            camAvaible = true;

        }

        private void Update()
        {
            if (!camAvaible)
                return;

            Orientate();
        }

        private void Orientate()
        {
            float ratio = (float)backCam.width / (float)backCam.height;
            fit.aspectRatio = ratio;

            float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -backCam.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);

        }

        public void TakePhoto()
        {
            StartCoroutine(Photo());
        }

        IEnumerator Photo()
        {

            yield return new WaitForEndOfFrame();

            Texture2D photo = new Texture2D(backCam.width, backCam.height);
            photo.SetPixels(backCam.GetPixels());
            photo.Apply();

            //Encode to a PNG
            byte[] bytes = photo.EncodeToPNG();

            //File.WriteAllBytes(Application.dataPath + "/photo.png", bytes);
            NativeGallery.SaveImageToGallery(photo, "Mobile App", "Photo");
        }

        private void Back()
        {
            mobileApp.ActivateMenu();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(Back);
            takePhotoButton.onClick.AddListener(TakePhoto);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
        }

    }
}