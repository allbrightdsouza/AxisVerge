using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.tag = transform.parent.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(this.tag.Equals("LeftBullet") && collision.gameObject.tag.Equals("HPLeft")) {
            return;
        } else if(this.tag.Equals("RightBullet") && collision.gameObject.tag.Equals("HPRight"))
        {
            return;
        }
        else if(!(collision.gameObject.tag.Equals(this.gameObject.tag)))
        {
            Destroy(this.gameObject);
        }
    }
}
