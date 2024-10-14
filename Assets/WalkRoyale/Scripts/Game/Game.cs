using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

namespace WalkRoyale
{
    [AddComponentMenu("WalkRoyale/Game/Game")]
    public partial class Game : MonoBehaviour
    {
        [Header("References")]
        [SerializeField()] public GameObject winActivator;
        [SerializeField()] public TextMeshProUGUI winLabel;
        [SerializeField()] public GameObject defeatActivator;
        [SerializeField()] public TextMeshProUGUI defeatLabel;
        [SerializeField()] public PlayerController playerController;
        [SerializeField()] public FloatingMessageProvider floatingMessageProvider;
        [SerializeField()] public AudioSource audioSourceVictory;

        [Header("Scenes")]
        [SerializeField()] public string restartSceneName;

        [NonSerialized()] public bool isGameOver;

        protected virtual IEnumerator SetDefeatTime()
        {
            yield return new WaitForSeconds(2.5f);

            for (int i = 3, n = 0; i != n; i--)
            {
                defeatLabel.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }

            Restart();
        }

        public virtual void Defeat()
        {
            playerController.SetPauseState(true);
            floatingMessageProvider.ResetTime();
            defeatActivator.SetActive(true);
            isGameOver = true;
            StartCoroutine(SetDefeatTime());
        }

        protected virtual IEnumerator SetWinTime()
        {
            yield return new WaitForSeconds(2.5f);

            for (int i = 3, n = 0; i != n; i--)
            {
                winLabel.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }

            Restart();
        }

        public virtual void Restart()
        {
            SceneManager.LoadScene(restartSceneName);
        }

        public virtual void Win()
        {
            playerController.SetPauseState(true);
            floatingMessageProvider.ResetTime();
            winActivator.SetActive(true);
            isGameOver = true;
            audioSourceVictory.Play();
            StartCoroutine(SetWinTime());
        }
    }
}