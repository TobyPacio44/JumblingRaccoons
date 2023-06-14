using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerHatLight : MonoBehaviour
{
    public Transform PlayerCam;
    
    public float MouseX;
    public float MouseY;
    public float sens;
    public bool ifR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        MouseX += Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        
          
        PlayerCam.rotation = Quaternion.Euler(0, MouseX, 0);
        

    }
}
