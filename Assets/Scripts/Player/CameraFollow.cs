//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CameraFollow : MonoBehaviour
//{
//    public float zDistance;
//    GameObject characterPos;

//    // Use this for initialization
//    void Start()
//    {
//        characterPos = GameObject.FindGameObjectWithTag("Player");
//        zDistance = transform.position.z - characterPos.transform.position.z;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.position = new Vector3(transform.position.x, transform.position.y, characterPos.transform.position.z + zDistance);
//    }
//}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float zDistance;
    GameObject characterPos;
    private Vector3 posCamera;
    private Vector3 angleCam;
    public Vector3 startPos = new Vector3(0, 2.6f, -5f);
    // Use this for initialization
    void Start()
    {
        characterPos = GameObject.FindGameObjectWithTag("Player");
        zDistance = transform.position.z - characterPos.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(characterPos.transform.position.x, transform.position.y, characterPos.transform.position.z + zDistance);
    }

    void LateUpdate()
    {
        posCamera.x = Mathf.Lerp(posCamera.x, characterPos.transform.position.x, 5 * Time.deltaTime);
        posCamera.y = Mathf.Lerp(posCamera.y, characterPos.transform.position.y + 4.82f, 5 * Time.deltaTime);
        posCamera.z = Mathf.Lerp(posCamera.z, characterPos.transform.position.z + zDistance, 10f);
        this.transform.position = posCamera;
        //angleCam.x = 20f;
        //angleCam.y = Mathf.Lerp(angleCam.y, 0, 1 * Time.deltaTime);
        //angleCam.z = transform.eulerAngles.z;
        //this.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, angleCam, 1 * Time.deltaTime);
    }
}
