using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerPoint : MonoBehaviour
{
    // M�thode appel�e lorsqu'un autre objet entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant a le tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Le joueur a atteint le point de victoire !");
            Destroy(gameObject);

            // Ajoutez ici le code que vous souhaitez ex�cuter
            // Exemple : charger une nouvelle sc�ne, afficher un message, etc.
        }
    }
}
