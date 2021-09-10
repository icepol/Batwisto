using UnityEngine;

public class DevilGroup : MonoBehaviour
{
    private SoundEffects _soundEffects;

    private void Awake()
    {
        _soundEffects = GetComponent<SoundEffects>();
    }

    private void Start()
    {
        _soundEffects.PlayOnDevilSpawn();
        
        Destroy(gameObject, 10f);
    }
}
