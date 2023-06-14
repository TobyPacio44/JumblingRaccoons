using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crosshair : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player player; //player main script

    [Header("Action")]
    [SerializeField] private GameObject currentAction;

    [Header("UI")]
    [SerializeField] public Image reticle;
    public GameObject canvas;
    private Collider Rac_collider;
    public Slider Action;
    public Slider MagnitudeUI;

    private OpenDoor OpenDoorScript;
    private OpenPull OpenPullScript;
    private Lights Lights;

    [HideInInspector] public Transform FakeParent;
    [HideInInspector] public Transform up, down, whereLand;
    private int magnitude;
    private int Interactable;

    public void Awake()
    {
        Interactable = LayerMask.NameToLayer("Interact");
    }

    private void Start()    
    {
        reticle.color = new Color(1, 1, 1, 0.75f);
        Rac_collider = player.raccoon.GetComponent<Collider>();
        player.Camera = GetComponent<MouseLoop2>();
    }

    private void Update()
    {
        if (FakeParent != null)
        { 
            player.transform.position = FakeParent.transform.position;
            player.transform.rotation = FakeParent.transform.rotation;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                ResetAction();
                player.movement.enabled = true;
                FakeParent = null;
            }
        }

        if (Input.GetMouseButtonDown(1) && !player.movement.isClimbing)
        {
            player.Camera.PlayerCam.localPosition = new Vector3(0,7.35f,-33);
            //Camera.PlayerCam.localRotation = Quaternion.Euler(-8.7f, raccoon.transform.rotation.y, 0);
        }
        if (Input.GetMouseButton(1))
        {
            player.Camera.RotatingAroundPlayer = true;
        }
        else if(!FakeParent && !player.movement.isClimbing){ 
            player.Camera.RotatingAroundPlayer = false;
        }

        if (Input.GetMouseButtonUp(1) && !player.movement.isClimbing)
        {
            ResetCamera();
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.value = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            MagnitudeUI.transform.gameObject.SetActive(false);
            Action.transform.gameObject.SetActive(false);
        }


        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, out hit, 70))
        {            
            reticle.color = new Color(1, 1, 1, 0.75f);
            Action.value = 0;
            magnitude = 0;
            MagnitudeUI.transform.gameObject.SetActive(false);
            Action.transform.gameObject.SetActive(false);
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 70))
        {
            if (hit.transform.gameObject == currentAction)
            {
                reticle.color = new Color(1, 1, 1, 0.75f);
                return;
            }

            if (hit.transform.gameObject.layer != Interactable)
            {
                reticle.color = new Color(1, 1, 1, 0.75f);
            }
            else if (hit.transform.gameObject.layer == Interactable)
            {
                reticle.color = new Color(1, 0, 0, 0.75f);
            }

            if (hit.transform.CompareTag("PickUp"))
            {
                Pickup(hit);
            }
            if (hit.transform.CompareTag("OpenPull"))
            {
                FurniturePull(hit);
            }
            if (hit.transform.CompareTag("Open"))
            {
                OpenDoor(hit);
            }           
            if (hit.transform.CompareTag("Lights"))
            {
                LightOn(hit);
            }
            if (hit.transform.CompareTag("Key"))
            {
                KeyOn(hit);
            }
            if (hit.transform.CompareTag("Push"))
            {
                Push(hit);
            }
            if (hit.transform.CompareTag("Skateboard"))
            {
                Skateboard(hit);
            }
            if (hit.transform.CompareTag("Climb") && player.movement.isClimbing == false)
            {
                Climb(hit);
            }
        }
    }
    private void KeyOn(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 10f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);

            var item = hit.transform.GetComponent<Item>();
            if (item)
            {
                player.inventory.AddItem(item.item, 1);
                hit.transform.gameObject.GetComponent<Key>().Unlocking();
                Destroy(hit.transform.gameObject);
            }
        }
        else return;
    } 
    void Skateboard(RaycastHit hit)
    {       
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 2f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);

            player.movement.enabled = false;
            FakeParent = hit.transform;
            hit.transform.GetComponent<Skateboard>().enabled = true;
            hit.transform.GetComponent<Skateboard>().child = player.gameObject;
            currentAction = hit.transform.gameObject;
        }
        else return;          
    }
    private void LightOn(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 2f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);

            Lights = hit.transform.GetComponent<Lights>();
            if (Lights.LightsOn == false)
            {
                Lights.LightsOn = true;
                Lights.LightOn();
            }
            else
            {
                Lights.LightsOn = false;
                Lights.LightOn();
            }
        }
        else return;
    }
    private void Push(RaycastHit hit)
    {               
        hit.transform.gameObject.GetComponent<Rigidbody>();
        if (Input.GetKeyDown(KeyCode.E))
        {
            MagnitudeUI.value = 2000f;
            MagnitudeUI.transform.gameObject.SetActive(true);
            magnitude = 2000;
        }

        if (Input.GetKey(KeyCode.E) && magnitude < 40000 && MagnitudeUI.gameObject.activeSelf == true)
        {
            magnitude += 1000;
            MagnitudeUI.value += 1000f;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            MagnitudeUI.transform.gameObject.SetActive(false);
            hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * magnitude);
        }
    }
    private void Pickup(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 10f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);
            var item = hit.transform.GetComponent<Item>();
            if (item)
            {
                player.inventory.AddItem(item.item, 1);
                player.Mission.mission.Remove(item.gameObject);
                Destroy(hit.transform.gameObject);
                player.Mission.spawner.ListSave();
            }
        }
        else return;
    }
    public void Climb(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 3f;
        }
        if (Action.value == 100 && player.movement.isClimbing == false)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);
            down = hit.transform.GetComponent<ClimbSnap>().snap.transform;
            up = hit.transform.GetComponent<ClimbSnap>().Destination.transform;
            whereLand = hit.transform.GetComponent<ClimbSnap>().up.transform;

            Rac_collider.enabled = false;
            player.movement.isClimbing = true;
            player.raccoon.transform.position = hit.transform.GetComponent<ClimbSnap>().snap.transform.position + new Vector3(0, 10f, 0);
            player.Rig.transform.rotation = hit.transform.GetComponent<ClimbSnap>().snap.transform.rotation;
            //movement.body.transform.Rotate(-90, 0, 0);
            player.animations.state = Animations.Animation.climbing;
            currentAction = hit.transform.gameObject;
        }
        else return;
    }

    private void FurniturePull(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 1f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);
            OpenPullScript = hit.transform.GetComponent<OpenPull>();
            if (OpenPullScript.isOpened == false)
            {
                OpenPullScript.isOpened = true;
            }
            else OpenPullScript.isOpened = false;
        }
        else return;
    }
    private void OpenDoor(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Action.transform.gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.E) && Action.gameObject.activeSelf == true)
        {
            Action.value += 1f;
        }
        if (Action.value == 100)
        {
            Action.value = 0;
            Action.transform.gameObject.SetActive(false);
            OpenDoorScript = hit.transform.GetComponent<OpenDoor>();
            OpenDoorScript.Interact();
        }
        else return;
    }
    private void Interact(RaycastHit hit)
    {  
        if (Input.GetKeyDown(KeyCode.E))
        {


        }
        else return;
    }

    public void ResetAction()
    {
        currentAction = null;
    }
    public void ResetCamera()
    {
        transform.localPosition = new Vector3(0.40f, 16.81f, -22.26f);
    }

    private void OnApplicationQuit()
    {
        player.inventory.Container.Clear();
    }
}