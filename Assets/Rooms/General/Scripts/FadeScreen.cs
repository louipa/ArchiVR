using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tengio {
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Canvas))]
    public class FadeScreen : MonoBehaviour {

        [SerializeField]
        private float duration = 0.2f;

        [SerializeField] public Camera xrCamera;

        private Image image;
        private Canvas canvas;
        private Coroutine fadeOutCoroutine;
        private Coroutine fadeInCoroutine;

        [Header("World Space Settings")]
        [SerializeField] private float distanceInFront = 0.5f; // meters in front of camera

        private void Awake() {
            image = GetComponent<Image>();
            canvas = GetComponent<Canvas>();
            SetupCanvas();
        }

        private void SetupCanvas() {
            if (xrCamera == null) {
                Debug.LogWarning("XR Camera not assigned on FadeScreen.");
                return;
            }

            // Make canvas render in front of camera
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = xrCamera;
            } else if (canvas.renderMode == RenderMode.ScreenSpaceCamera) {
                canvas.worldCamera = xrCamera;
            }

            // Parent to XR camera to always follow
            canvas.transform.SetParent(xrCamera.transform, false);
            canvas.transform.localPosition = new Vector3(0f, 0f, distanceInFront);
            canvas.transform.localRotation = Quaternion.identity;
            canvas.transform.localScale = Vector3.one;
        }

        public void FadeOut(Action callback = null) {
            CancelPendingFades();
            fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(callback));
        }

        public void FadeIn(Action callback = null) {
            CancelPendingFades();
            fadeInCoroutine = StartCoroutine(FadeInCoroutine(callback));
        }

        private IEnumerator FadeInCoroutine(Action callback) {
            Color color = image.color;
            color.a = 1f;
            float startTime = Time.unscaledTime;
            while (Time.unscaledTime - startTime <= duration) {
                color.a -= Time.unscaledDeltaTime / duration;
                color.a = Mathf.Clamp01(color.a);
                image.color = color;
                yield return null;
            }
            color.a = 0f;
            image.color = color;
            callback?.Invoke();
        }

        private IEnumerator FadeOutCoroutine(Action callback) {
            Color color = image.color;
            color.a = 0f;
            float startTime = Time.unscaledTime;
            while (Time.unscaledTime - startTime <= duration) {
                color.a += Time.unscaledDeltaTime / duration;
                color.a = Mathf.Clamp01(color.a);
                image.color = color;
                yield return null;
            }
            color.a = 1f;
            image.color = color;
            callback?.Invoke();
        }

        private void CancelPendingFades() {
            if (fadeOutCoroutine != null) StopCoroutine(fadeOutCoroutine);
            if (fadeInCoroutine != null) StopCoroutine(fadeInCoroutine);
        }
    }
}