using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScript : MonoBehaviour
{
    public void CambiarEscena(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }
}
