using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCaracter : MonoBehaviour
{
    // Referencias a los objetos GameObject que representan a los personajes masculino y femenino
    public GameObject MenCharacter;
    public GameObject WomenCharacter;

    // Variables booleanas que indican si se ha seleccionado el personaje masculino o femenino
    public bool Men;
    public bool Women;

    // Método llamado en cada frame
    void Update()
    {
        // Obtiene el valor almacenado en PlayerPrefs que indica si se ha seleccionado el personaje masculino o femenino
        Men = PlayerPrefs.GetInt("MenSelect") == 1;
        Women = PlayerPrefs.GetInt("WomenSelect") == 1;

        // Verifica si se ha seleccionado el personaje masculino
        if (Men == true)
        {
            // Activa el objeto del personaje masculino y destruye el objeto del personaje femenino
            MenCharacter.SetActive(true);
            Destroy(WomenCharacter);
        }

        // Verifica si se ha seleccionado el personaje femenino
        if (Women == true)
        {
            // Activa el objeto del personaje femenino y destruye el objeto del personaje masculino
            WomenCharacter.SetActive(true);
            Destroy(MenCharacter);
        }
    }
}
