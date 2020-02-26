using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSpawner : MonoBehaviour
{
    [SerializeField]
    Transform pickUpParent;
    [SerializeField]
    GameObject pickUp;

    public int count;
    [SerializeField]
    int maxSpawnCount;
    public float timeInterval;

    [SerializeField]
    private float timeLeft;
    // Start is called before the first frame update

    public static SpeedUpSpawner Instance;
    private void Awake()
    {
        if(Instance == null)
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
        count = 0;
        timeLeft = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(count < maxSpawnCount)
            timeLeft -= Time.deltaTime;
        if(timeLeft <= 0f )
        {
            timeLeft = timeInterval;
            if(count < maxSpawnCount)
            {
                count += 1;
                GameObject obj = Instantiate(pickUp, pickUpParent);

                float height = Camera.main.orthographicSize - 0.5f;
                float width = Camera.main.orthographicSize * Screen.width / Screen.height - 0.5f;

                obj.transform.position = new Vector3(
                    Random.Range( -width, width),
                    Random.Range(-height, height), 0f);
            }
        }
    }
}
