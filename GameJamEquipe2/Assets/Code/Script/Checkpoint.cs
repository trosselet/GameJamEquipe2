using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetBool("isNearPlayer", true);
        if (other.TryGetComponent(out PlayerEndLevel endLevel))
        {
            endLevel.LastCheckpoint = gameObject;
        }
    }
}
