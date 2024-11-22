using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManaggementMenu : MonoBehaviour
{
    public void CambiarEscena(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }
}
