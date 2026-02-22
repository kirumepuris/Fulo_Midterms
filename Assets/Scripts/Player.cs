using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    public float range = 20f;
    public Transform[] spawnPoints;   // Assign same 3 spawn points here

    private Transform currentTarget;
    public Transform firePoint;
    public float firerate = 1f;
    public GameObject[] playerPrefabs;
    public GameObject[] bulletPrefabs;

    private int currentIndex = 0;
    private GameObject currentVisual;   


    void Update()
    {
        DetectEnemies();

        if (currentTarget != null)
        {
            RotateTowardTarget();
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
        CycleCharacter();
        }
   
    }
    void Start()
    {
        StartCoroutine(ShootRoutine());
        currentVisual = Instantiate(playerPrefabs[currentIndex], transform);
        currentVisual.transform.localPosition = Vector3.zero;
        currentVisual.transform.localRotation = Quaternion.identity;

    }   

    IEnumerator ShootRoutine(){
    while (true)
    {
    GameObject bullet = Instantiate(
    bulletPrefabs[currentIndex],
    firePoint.position,
    firePoint.rotation
    );

    bullet.GetComponent<Bullet>().colorIndex = currentIndex;
    
    yield return new WaitForSeconds(firerate);
    }
    }
    
    void DetectEnemies()
    {
        RaycastHit hit;
        currentTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform spawnPoint in spawnPoints)
        {
            Vector3 direction = (spawnPoint.position - transform.position).normalized;

            Debug.DrawRay(transform.position, direction * range);

            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (hit.distance < closestDistance)
                    {
                        closestDistance = hit.distance;
                        currentTarget = hit.transform;
                    }
                }
            }
        }
    }

    void RotateTowardTarget()
    {
        Vector3 targetPosition = new Vector3(
            currentTarget.position.x,
            transform.position.y,
            currentTarget.position.z
        );

        transform.LookAt(targetPosition);
    }
    void CycleCharacter()
    {
    currentIndex = (currentIndex + 1) % playerPrefabs.Length;

    Destroy(currentVisual);
    currentVisual = Instantiate(playerPrefabs[currentIndex], transform);
    currentVisual.transform.localPosition = Vector3.zero;
    currentVisual.transform.localRotation = Quaternion.identity;
    }
    void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Enemy"))
    {
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
    }
}