using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAvatar : MonoBehaviour
{
    // Variables booleanas que indican si se ha seleccionado el avatar masculino o femenino
    public bool Men;
    public bool Women;

    // Método llamado al despertar el script
    private void Awake()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el avatar masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;
    }

    // Método llamado en cada frame
    private void Update()
    {
        // Verifica si no se ha seleccionado ningún avatar y establece el avatar masculino como predeterminado
        if (Men == false && Women == false)
        {
            Men = true;
        }

    }

    // Método público para seleccionar el avatar masculino
    public void MenAvatar()
    {
        Men = true;
        Women = false;
        Save();
    }

    // Método público para seleccionar el avatar femenino
    public void WomenAvatar()
    {
        Men = false;
        Women = true;
        Save();
    }

    // Método público para guardar la selección de avatar en PlayerPrefs
    public void Save()
    {
        PlayerPrefs.SetInt("MenSelect", Men ? 1 : 0);
        PlayerPrefs.SetInt("WomenSelect", Women ? 1 : 0);
    }
}