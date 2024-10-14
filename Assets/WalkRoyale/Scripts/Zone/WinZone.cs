using UnityEngine;

namespace WalkRoyale
{
    [ExecuteInEditMode()]
    [AddComponentMenu("WalkRoyale/WinZone/WinZone")]
    public partial class WinZone : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public Game game;

        public virtual void PerformWin()
        {
            game.Win();
        }
    }
}