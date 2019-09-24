using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follw_Camera : MonoBehaviour
{

    public float speedX = 10.0F;
    public float speedY = 10.0F;
    public GameObject Camera2;
    public Rigidbody rd;
    public float SportSpeed2 = 5;
    public float RotateSpeed2 = 2;

    private Camera camera;
    // Use this for initialization
    private void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float X = Input.GetAxis("Mouse X") * RotateSpeed2;
        float Y = Input.GetAxis("Mouse Y") * RotateSpeed2;
        Vector3 speed = new Vector3(h, 0, Y);
        rd.velocity = speed * SportSpeed2;

        camera.transform.localRotation = camera.transform.localRotation * Quaternion.Euler(-Y, 0, 0);
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, X, 0);

        float translationY = Input.GetAxis("Vertical") * speedY;
        float translationX = Input.GetAxis("Horizontal") * speedX;
        translationY *= Time.deltaTime;
        translationX *= Time.deltaTime;
        //transform.Translate(0, translationY, 0);
        //transform.Translate(translationX, 0, 0);
        transform.Translate(translationX, translationY, 0);
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fired Pressed");
            Debug.Log(Input.mousePosition);

            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca = Camera.main; //cam.GetComponent<Camera> ();
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hit
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.transform.gameObject.name);
                if (hit.collider.gameObject.tag.Contains("Finish"))
                { //plane tag
                    Debug.Log("hit " + hit.collider.gameObject.name + "!");
                }
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
