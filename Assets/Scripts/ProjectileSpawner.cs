using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip switchUp;
    [SerializeField]
    AudioClip laser;

    [SerializeField]
    Transform projectileParent;
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    int count = 1;

    public float spawnCountTimeInterval = 40f;
    [SerializeField]
    private float spawnCountTimeLeft;

    [SerializeField]
    Transform player;

    public float safeRadius = 3f;
    public float timeInterval;

    [SerializeField]
    private float timeLeft;

    // Start is called before the first frame update

    public static ProjectileSpawner Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeLeft = timeInterval;
        spawnCountTimeLeft = spawnCountTimeInterval;
    }
    // Start is called before the first frame update
    void Update()
    {
        spawnCountTimeLeft -= Time.deltaTime;
        if (spawnCountTimeLeft <= 0f)
        {
            spawnCountTimeLeft = spawnCountTimeInterval;
            count += 1;
        }

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            for(int i = 0; i < count; i++)
                Spawn();
        }

    }

    public void PlaySwitchUp()
    {
        audioSource.PlayOneShot(switchUp);
    }

    public void PlayLaser()
    {
        audioSource.PlayOneShot(laser);
    }
    void Spawn()
    {
        timeLeft = timeInterval;
        GameObject obj = Instantiate(projectile, projectileParent);

        float height = Camera.main.orthographicSize - 1.5f;
        float width = Camera.main.orthographicSize * Screen.width / Screen.height - 1.5f;


        obj.transform.position = new Vector3(
            Random.Range(-width, width),
            Random.Range(-height, height), 0f);

        Vector3 oldPos = obj.transform.position;
        Vector3 weightedDirection = (obj.transform.position - player.position);
        if (weightedDirection.magnitude < safeRadius)
        {
            Debug.Log("Within Radius");
            obj.transform.position += weightedDirection.normalized * safeRadius;
            Debug.DrawLine(oldPos, obj.transform.position, Color.green, Mathf.Infinity);
        }

        obj.transform.position = new Vector3(
            Mathf.Clamp(obj.transform.position.x, -width, width),
            Mathf.Clamp(obj.transform.position.y, -height, height), 0f);
    }
}
