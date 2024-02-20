using System.Collections.Generic;
using UnityEngine;

public class RandomImageOrder : MonoBehaviour
{
    // Lista para almacenar las imágenes
    private List<Transform> images = new List<Transform>();

    void Start()
    {
        // Obtiene todas las imágenes que son hijas del objeto Canvas
        foreach (Transform child in transform)
        {
            images.Add(child);
        }

        // Mezcla la lista de imágenes
        Shuffle(images);

        // Asigna la nueva posición a cada imagen
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetSiblingIndex(i);
        }
    }

    // Función para mezclar una lista
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
