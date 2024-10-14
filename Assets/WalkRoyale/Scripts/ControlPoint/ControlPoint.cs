using UnityEngine;

using System.Collections;

namespace WalkRoyale
{
    [ExecuteInEditMode()]
    [AddComponentMenu("WalkRoyale/ControlPoint/ControlPoint")]
    public partial class ControlPoint : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Transform flagL;
        [SerializeField()] public Transform flagR;

        [Header("Animation Properties")]
        [SerializeField()] public Vector3 openAxis;
        [SerializeField()] public float openAngle;
        [SerializeField()] public float openDuration;

        protected virtual IEnumerator OpenGateFlag(Transform target)
        {
            var elapsed = 0.0f;

            var sourceAngle = target.eulerAngles;
            var targetAngle = openAxis * openAngle;

            while (elapsed <= openDuration)
            {
                elapsed += Time.deltaTime;

                var t = Mathf.Clamp01(elapsed / openDuration);
                var newAngle = Vector3.LerpUnclamped(sourceAngle, targetAngle, t);
                target.eulerAngles = newAngle;

                yield return new WaitForEndOfFrame();
            }
        }

        public virtual void OpenGate()
        {
            StartCoroutine(OpenGateFlag(flagL));
            StartCoroutine(OpenGateFlag(flagR));
        }
    }
}