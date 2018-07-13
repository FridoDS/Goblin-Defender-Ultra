using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] private float damage;
	// Use this for initialization
	void Start () {
        //DrawTraject();
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        //Debug.Log(transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<Enemy>().reduceHP(damage);
        }
        Destroy(gameObject);
    }

    public void Launch(Transform _bullseye, float _angle)
    {
        // source and target positions
        Vector3 pos = transform.position;
        Vector3 target = _bullseye.position;

        // distance between target and source
        float dist = Vector3.Distance(pos, target);

        // rotate the object to face the target
        transform.LookAt(target);

        // calculate initival velocity required to land the cube on target using the formula (V0 = SQRT(RG * sin2(@)))
        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        // create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);
    

        // transform it to global vector
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);


        // launch the cube by setting its initial velocity
        GetComponent<Rigidbody>().velocity = globalVelocity;


    }

    

    private void DrawTraject()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(20);

        for(int i = 0; i < 20; i++)
        {
            line.SetPosition(i, new Vector3((i+1)/2, 0f, i ));
        }
    }
}
