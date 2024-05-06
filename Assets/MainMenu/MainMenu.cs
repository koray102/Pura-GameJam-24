using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Sahne indeksi 1 ise bu, genellikle oyunun ana sahnesidir.
    }

    // Update is called once per frame
    public void ExitGame()
    {
        // Uygulamadan çýkýþ yapýlýr
        Application.Quit();
    }
}
