using TMPro;

using UnityEngine;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/GameProgress/GameProgress")]
    public partial class GameProgress : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Game game;
        [SerializeField()] public TextMeshProUGUI gameResourcesLabel;

        [Header("Initial Properties")]
        [SerializeField()] public int dollars;

        public virtual void SubtractDollars(int amount)
        {
            dollars -= amount;
            gameResourcesLabel.text = dollars.ToString() + "$";

            if (dollars < 0)
                game.Defeat();
        }

        public virtual void AddDollars(int amount)
        {
            dollars += amount;
            gameResourcesLabel.text = dollars.ToString() + "$";
        }

        protected virtual void Start()
        {
            gameResourcesLabel.text = dollars.ToString() + "$";
        }
    }
}