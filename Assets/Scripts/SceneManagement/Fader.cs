using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1f;

        private CanvasGroup canvasGroup;
        private Coroutine currentActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetFade(float amount)
        {
            canvasGroup.alpha = Mathf.Clamp(amount, 0, 1);
        }

        public Coroutine FadeOut()
        {
            return Fade(1);
        }

        public Coroutine FadeIn()
        {
            return Fade(0);
        }
        public Coroutine Fade(float target)
        {
            if (currentActiveFade != null) StopCoroutine(currentActiveFade);
            currentActiveFade = StartCoroutine(FadeRoutine(target));
            return currentActiveFade;
        }

        private IEnumerator FadeRoutine(float target)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / fadeTime);
                yield return null;
            }
        }
    }
}
