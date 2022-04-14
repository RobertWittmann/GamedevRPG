using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetFade(float amount)
        {
            canvasGroup.alpha = Mathf.Clamp(amount, 0, 1);
        }

        public IEnumerator FadeOut()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeOutTime;
                yield return null;
            }
        }
        public IEnumerator FadeIn()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeInTime;
                yield return null;
            }
        }
    }
}
