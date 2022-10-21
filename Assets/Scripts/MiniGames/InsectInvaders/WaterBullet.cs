using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 500f;

    [SerializeField] 
    private float lifeTime = 5f;
    
    internal void DestroySelf()
    {
        gameObject.SetActive(false);
        DestroyImmediate(gameObject, true);
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
        Debug.Log("collisionenter");
        if (other.gameObject.GetComponent<Insect>())
        {
            InsectInvaders.Instance.InsectKilledAtLocation(other.gameObject.GetComponent<Insect>().transform);
            DestroySelf();
        }
    }
}
