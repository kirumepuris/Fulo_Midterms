using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float maxRange = 15f;
    public int colorIndex;

    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(startPosition, transform.position) > maxRange)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Enemy"))
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy.colorIndex == colorIndex)
        {

            Destroy(other.gameObject);
        }


        Destroy(gameObject);
    }
    }
}
