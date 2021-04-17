using System.Collections;
using UnityEngine;
using TMPro;
    public class MainMenu : MonoBehaviour
    {
    [SerializeField] TextMeshProUGUI versionText;
    // Use this for initialization
    void Start()
        {
        if (versionText == null)
        {
            throw new MainMenuException("version text not seted");
        }

        versionText.text = Application.version;
        }


        public void Quit ()
        {
            Application.Quit();
        }


        public void SelectMap ()
        {
        Loading.LoadScene("map1");
        }

            public void ContinueSession ()
        {

        }

        public void OpenSettings ()
        {

        }


    }