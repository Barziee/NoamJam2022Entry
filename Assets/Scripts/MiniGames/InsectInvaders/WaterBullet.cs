using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 200f;

    [SerializeField] 
    private float lifeTime = 5f;
    
    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void Awake()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.up);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Insect>())
        {
            InsectInvaders.Instance.InsectKilledAtLocation(other.gameObject.GetComponent<Insect>().transform);
            DestroySelf();
        }
            
    }
}
