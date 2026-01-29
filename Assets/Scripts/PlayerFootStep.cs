using UnityEngine;

public class PlayerFootStep : MonoBehaviour
{
    //[SerializeField] private Vector2 footStepEffectOffset;
    [SerializeField] private ParticleSystem footStepEffect;
    [SerializeField] private string targetTag = "Ground";

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(targetTag))
        {
            footStepEffect.Play();
        }
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
