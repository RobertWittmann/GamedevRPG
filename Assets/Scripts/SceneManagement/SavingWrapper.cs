using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.SetFade(1);
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}