using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerPoint : MonoBehaviour
{
    // Méthode appelée lorsqu'un autre objet entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant a le tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur a atteint le point de victoire !");
            Destroy(gameObject);

            // Ajoutez ici le code que vous souhaitez exécuter
            // Exemple : charger une nouvelle scène, afficher un message, etc.
        }
    }
}
