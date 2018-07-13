using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

    public GameObject ball;
    public float firepower;
    public float _angle;
    private float radiAngle;
    public Transform _bullseye;
    public Camera camera;
    [SerializeField] private float projectilePerSecond;


    private float defaultFOV;
    private float timestamp;


    // Use this for initialization
    void Start()
    {
        Vector3 t = transform.position;
        defaultFOV = camera.fieldOfView;
        //renderArc();

    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) camera.fieldOfView = 30f;
        else camera.fieldOfView = defaultFOV;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x + Input.mousePosition.normalized.y * Time.deltaTime * -30f, transform.eulerAngles.y, transform.eulerAngles.z), 0.4f);
        //transform.GetChild(0).transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -2f, 0f, 0f))  ;

        if (Time.fixedTime >= timestamp && Input.GetButtonDown("Fire1"))
        {
            float y = Input.mousePosition.y;
            float x = Input.mousePosition.x;
            Vector3 pos = new Vector3(x, y).normalized;
            //Debug.Log(pos.x + " " + pos.y);
            Shoot(pos.x, pos.y);
            //GameObject thisCannonBall = Instantiate(ball, transform.position, transform.rotation);
            //thisCannonBall.GetComponent<Ball>().Launch(_bullseye, _angle);

            timestamp = Time.fixedTime + 1 / projectilePerSecond;
        }

    }

    private void Shoot(float x, float y)
    {
        GameObject thisCannonBall = Instantiate(ball);
        thisCannonBall.transform.position = transform.position + (transform.forward * 2f);
        //thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(Camera.main.transform.forward*firepower, ForceMode.Impulse);
        thisCannonBall.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * firepower;
    }

    private void DrawTraject(Vector3 pos)
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(120);

        for (int i = 0; i < 120; i++)
        {
            Vector3 vpos = launchWithReturn(pos, _bullseye.position, _angle);
            line.SetPosition(i, pos);
            pos = pos + vpos;
            Debug.Log(pos);
        }
    }

    private Vector3 launchWithReturn(Vector3 pos, Vector3 target, float angle)
    {
        float dist = Vector3.Distance(pos, target);
        //Debug.Log(dist);

        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * angle * 2)));
        float Vy, Vz;


        transform.LookAt(target);
        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * angle);

        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        Vector3 globalVelocity = transform.TransformDirection(localVelocity);

        return globalVelocity;
    }

    private Vector3[] calculateVectorArray()
    {
        Vector3[] vArray = new Vector3[121];

        radiAngle = Mathf.Deg2Rad * _angle;

        float vz = launchWithReturn(transform.position, _bullseye.position, _angle).z;
        Debug.Log(vz);
        //float dist = Vector3.Distance(transform.position, _bullseye.position);
        float dist = (float)(vz * vz * Mathf.Sin(2 * radiAngle)) / -Physics.gravity.y;

        for (int i = 0; i < 120; i++)
        {
            float t = (float)i / 120f;
            vArray[i] = calculateArcPoint(t, dist);
        }

        return vArray;
    }

    private Vector3 calculateArcPoint(float t, float dist)
    {
        float x = t * dist;

        float vy = launchWithReturn(transform.position, _bullseye.position, _angle).y;
        float y = x * Mathf.Tan(radiAngle) - ((-Physics.gravity.y * x * x) / (float)(2f * vy * vy * Mathf.Cos(radiAngle) * Math.Cos(radiAngle)));

        return new Vector3(0f, y, x) + transform.position;
    }

    void renderArc()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(120);
        line.SetPositions(calculateVectorArray());
        Debug.Log(calculateVectorArray()[4]);

    }
}
