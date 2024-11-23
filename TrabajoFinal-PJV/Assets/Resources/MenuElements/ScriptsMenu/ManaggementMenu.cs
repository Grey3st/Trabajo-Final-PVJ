using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ManaggementMenu : MonoBehaviour
{
    //funcion para cambiar de escenas
    public void CambiarEscena(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }

    //lista que se va a ver en la interfaz de unity
    [Header("opciones")]
    public Slider volumen;
    public Toggle mute;
    public Button volver;
    public AudioMixer mixer;
    private float lastvolume;
    [Header("Paneles")]
    public GameObject panelJuego;
    public GameObject panelOpciones;


    private void Awake()
    {
        volumen.onValueChanged.AddListener(CambiarVolumenMaster);
        panelOpciones.SetActive(false);
    }
    //funcion para abrir paneles
    public void AbrirPaneles(GameObject panel) 
    {
        panelJuego.SetActive(false);
        panelOpciones.SetActive(false);
        panel.SetActive(true);
    }
    public void CambiarVolumenMaster(float volumen)
    {
        mixer.SetFloat("volumenMaster",volumen);
    }
    public void setVolumen()
    {
        if (mute.isOn)
        {
            mixer.GetFloat("volumenMaster", out lastvolume);
            mixer.SetFloat("volumenMaster", -80);
        }
        else
        {
            mixer.SetFloat("volumenMaster", lastvolume);
        }
    }
}
