using System.Collections;
using UnityEngine;

public class AutoColors : MonoBehaviour
{
    [SerializeField] private float duration = 5.0f; // Temps avant de changer la couleur
    private float elapsedTime = 0.0f; // Temps écoulé

    [SerializeField] private DimensionShifter dimensionShifter;

    void Start()
    {
        // Vérifier si la référence est assignée
        if (dimensionShifter == null)
        {
            // Si non, chercher automatiquement un composant DimensionShifter dans la scène
            dimensionShifter = FindObjectOfType<DimensionShifter>();

            if (dimensionShifter == null)
            {
                Debug.LogError("DimensionShifter n'est pas assigné et ne peut pas être trouvé dans la scène !");
            }
        }
    }

    void Update()
    {
        // Compter le temps écoulé
        elapsedTime += Time.deltaTime;

        // Si le temps écoulé dépasse la durée, changer la couleur
        if (elapsedTime >= duration)
        {
            elapsedTime = 0.0f; // Réinitialiser le compteur

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
