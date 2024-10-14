using TMPro;

using UnityEngine;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/UI/FloatingMessage/FloatingMessageProvider")]
    public partial class FloatingMessageProvider : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public GameObject floatingMessageActivator;
        [SerializeField()] public TextMeshProUGUI floatingMessageLabel;

        [Header("Animation Properties")]
        [SerializeField()] public Color zeroAmountColor;
        [SerializeField()] public Color addColor;
        [SerializeField()] public Color subtractColor;
        [SerializeField()] public float intervalToReset;

        protected float lastAmountAtTime;
        protected int localAmountAtTime;

        public virtual void RemoveFloatingMessageAmount(int amount)
        {
            floatingMessageActivator.SetActive(true);

            localAmountAtTime -= amount;

            if (localAmountAtTime > 0)
            {
                floatingMessageLabel.color = addColor;
                floatingMessageLabel.text = "+" + localAmountAtTime.ToString();
            }
            else if (localAmountAtTime < 0)
            {
                floatingMessageLabel.color = subtractColor;
                floatingMessageLabel.text = localAmountAtTime.ToString();
            }
            else
            {
                floatingMessageLabel.color = zeroAmountColor;
                floatingMessageLabel.text = "0";
            }

            lastAmountAtTime = intervalToReset;
        }

        public virtual void AddFloatingMessageAmount(int amount)
        {
            floatingMessageActivator.SetActive(true);

            localAmountAtTime += amount;

            if (localAmountAtTime > 0)
            {
                floatingMessageLabel.color = addColor;
                floatingMessageLabel.text = "+" + localAmountAtTime.ToString();
            }
            else if (localAmountAtTime < 0)
            {
                floatingMessageLabel.color = subtractColor;
                floatingMessageLabel.text = localAmountAtTime.ToString();
            }
            else
            {
                floatingMessageLabel.color = zeroAmountColor;
                floatingMessageLabel.text = "0";
            }

            lastAmountAtTime = intervalToReset;
        }

        public virtual void ResetTime()
        {
            lastAmountAtTime = 0.0f;
        }

        protected virtual void AmountProcess(float deltaTime)
        {
            if (floatingMessageActivator.activeSelf)
            {
                lastAmountAtTime -= Time.deltaTime;

                if (lastAmountAtTime <= 0.0f)
                {
                    floatingMessageActivator.SetActive(false);
                    localAmountAtTime = 0;
                }
            }
        }

        protected virtual void Update()
        {
            AmountProcess(Time.deltaTime);
        }
    }
}