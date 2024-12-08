using System.Collections;
using UnityEngine;

public class AutoColors : MonoBehaviour
{
    [SerializeField] private float duration = 5.0f; // Temps avant de changer la couleur
    private float elapsedTime = 0.0f; // Temps �coul�

    [SerializeField] private DimensionShifter dimensionShifter;

    void Start()
    {
        // V�rifier si la r�f�rence est assign�e
        if (dimensionShifter == null)
        {
            // Si non, chercher automatiquement un composant DimensionShifter dans la sc�ne
            dimensionShifter = FindObjectOfType<DimensionShifter>();

            if (dimensionShifter == null)
            {
                Debug.LogError("DimensionShifter n'est pas assign� et ne peut pas �tre trouv� dans la sc�ne !");
            }
        }
    }

    void Update()
    {
        // Compter le temps �coul�
        elapsedTime += Time.deltaTime;

        // Si le temps �coul� d�passe la dur�e, changer la couleur
        if (elapsedTime >= duration)
        {
            elapsedTime = 0.0f; // R�initialiser le compteur

            // Changer la couleur via DimensionShifter si disponible
            if (dimensionShifter != null)
            {
                dimensionShifter.ChangeColor();
            }
            else
            {
                Debug.LogWarning("Impossible de changer la couleur : DimensionShifter est introuvable.");
            }
        }
    }
}
