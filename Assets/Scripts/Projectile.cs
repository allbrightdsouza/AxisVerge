using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject particle;
    public enum State
    {
        fired,
        aiming,
        initializing
    }

    [SerializeField]
    float firedSpeed = 2.0f;
    [SerializeField]
    Transform target;

    [SerializeField]
    State currentState;

    Vector3 firedDirection;

    [SerializeField]
    List<GameObject> arrowParts;
    [SerializeField]
    float aimTime;
    float aimtTimeStart;

    bool playedMusic = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform> ();
        currentState = State.aiming;
        aimTime = Random.Range(1.5f, 3f);
        aimtTimeStart = aimTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.aiming)
        {
            LookAtTarget();
            aimTime -= Time.deltaTime;
            float percent = aimTime / aimtTimeStart;

            ChangeColor(percent);
            if(percent < 0.2f && !playedMusic)
            {
                playedMusic = true;
                ProjectileSpawner.Instance.PlayLaser();
            }
            if (aimTime <= 0f )
            {
                currentState = State.fired;
                firedDirection = target.position - transform.position;
                firedDirection.Normalize();
            }
        }

        if(currentState == State.fired)
        {
            transform.position += firedDirection * firedSpeed * Time.deltaTime;
        }
    }

    void ChangeColor(float percent)
    {
        for(int i = 0; i < arrowParts.Count; i++)
        {
            Material mat = arrowParts[i].GetComponent<MeshRenderer>().material;
            mat.color = Color.Lerp(Color.green, Color.red, 1 - percent);
        }
    }
    void LookAtTarget()
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            GameObject obj = Instantiate(particle, this.transform.position, Quaternion.identity);
            Destroy(obj, 3f);
            Destroy(this.gameObject);
        }
    }

}
