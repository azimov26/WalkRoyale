using UnityEngine;

using System.Collections;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/UI/Tutorial/TutorialProvider")]
    public partial class TutorialProvider : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public GameObject tutorialActivator;
        [SerializeField()] public CanvasGroup canvasGroup;

        [Header("Animation Properties")]
        [SerializeField()] public AnimationCurve fadingCurve;
        [SerializeField()] public float fadingFrequency;
        [SerializeField()] public float fadingAlphaMinimum;
        [SerializeField()] public float fadingDuration;

        protected virtual IEnumerator Start()
        {
            var elapsed = 0.0f;
            var sourceAlpha = canvasGroup.alpha;
            var targetAlpha = fadingAlphaMinimum;

            while (elapsed <= fadingDuration)
            {
                elapsed += Time.deltaTime;

                var t = fadingCurve.Evaluate(Mathf.Repeat((elapsed / fadingDuration) * fadingFrequency, 1.0f));
                var newAlpha = Mathf.LerpUnclamped(sourceAlpha, targetAlpha, t);
                canvasGroup.alpha = newAlpha;

                yield return new WaitForEndOfFrame();
            }

            tutorialActivator.SetActive(false);
        }
    }
}