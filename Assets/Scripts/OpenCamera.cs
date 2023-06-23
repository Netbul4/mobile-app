using UnityEngine;
using UnityEngine.UI;

namespace MobileApp
{
    public class OpenCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] RawImage background;
        [SerializeField] Image camLogo;
        [SerializeField] AspectRatioFitter fit;
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

        private void Back()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            backButton.onClick.AddListener(Back);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
        }

    }
}