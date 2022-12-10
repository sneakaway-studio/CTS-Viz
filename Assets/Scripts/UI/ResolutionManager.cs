using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using TMPro;
using SneakawayUtilities;

/**
 *  Manage game "resolution" - runs in play mode AND editor
 */

[ExecuteAlways]
public class ResolutionManager : MonoBehaviour
{

    [Space(2)]

    [Header("----- CANVAS -----")]
    // the canvas from the UI

    [SerializeField]
    private float cameraSize;

    [Tooltip("A RectTransform on (any) Canvas that fills the screen/UI")]
    public RectTransform mainCanvasRect;

    [Tooltip("The resolution")]
    public Vector2 mainCanvasResolution;



    [Space(2)]

    [Header("----- UNITY PLAYER -----")]
    // states of the Unity 'player' or 'game view' window

    [Tooltip("Resolution of the player window")]
    // - in full-screen it is also the current resolution
    // - unfortunately named using "Screen" https://docs.unity3d.com/ScriptReference/Screen-height.html
    public Vector2 playerResolution;

    //[Tooltip ("Resolution of the game view")]
    //// - set in Unity, stored in Application Support (Mac) (seems to always be same as above)
    //public Vector2 gameViewResolution;

    [Tooltip("The size of the player view (we will set) in unity units")]
    // depends on camera size
    public Vector3 playerViewSize;

    [Tooltip("Aspect ratio of the player")]
    // aspect ratio W:1 - relationship between resolution w:h (e.g. 1.33 (4:3) or 1.77 (16:9))
    public float playerAspectRatio;

    [Tooltip("Whether or not player is in fullscreen mode")]
    public bool playerFullScreen;



    [Space(2)]

    [Header("----- DEVICE -----")]
    // states of the screen / device display

    [Tooltip("Device resolution (px)")]
    // - the actual screen or "display" this project is running on
    // - if the player is running in window mode, this returns the current resolution of the desktop
    // - unfortunately named Screen.currentdeviceResolution (as opposed to another, less recent one?)
    public Vector2 deviceResolution;

    [Tooltip("Device aspect ratio")]
    // - W:1 - relationship between resolution w:h (e.g. 1.33 (4:3) or 1.77 (16:9))
    public float deviceAspectRatio;




    [Space(2)]

    [Header("----- WORLD -----")]
    // Object references Update if parameters change

    [Tooltip("Collider that defines the volume of the visible game world")]
    public BoxCollider worldContainer;
    [Tooltip("Collider that defines the volume where to create new game objects (these may be the same, or not)")]
    public BoxCollider instantiateContainer;
    [Tooltip("The longest side of the of world container, used to scale number of files that fill it")]
    public float instantiateContainerLongestSide;



    [Space(2)]

    [Header("----- REPORTING -----")]

    [Tooltip("Text for reporting")]
    public TMP_Text resolutionReport1Text;
    [Tooltip("Text for reporting")]
    public TMP_Text resolutionReport2Text;
    [Tooltip("Wait while the file is updating")]
    public bool coroutineRunning;


    private void OnValidate()
    {
        // for some reason using the collider on this object caused the editor to crash every time (owen: 2021)
        worldContainer = GetComponent<BoxCollider>();
    }

    private void Awake()
    {
        StartCoroutine(SendResolutionUpdatedEvent());
    }

    private void Update() => CheckIfPlayerResolutionUpdated();

    void CheckIfPlayerResolutionUpdated()
    {
        if (playerResolution.x != Screen.width || playerResolution.y != Screen.height)
        {
            StartCoroutine(SendResolutionUpdatedEvent());
        }
    }

    IEnumerator SendResolutionUpdatedEvent()
    {
        if (coroutineRunning) yield return null;
        coroutineRunning = true;

        //Debug.Log ("ResolutionManager.SendResolutionUpdatedEvent() - resolution has changed to " + playerResolution.ToString ());
        // if application is playing 
        if (Application.IsPlaying(gameObject))
        {
            //DebugManager.Instance.UpdateDisplay ("ResolutionManager.SendResolutionUpdatedEvent() change " + playerResolution.ToString ());
        }
        // update the parameters
        UpdateResolutionParams();

        // update collider
        UpdateColliderSize();

        UpdateReport();

        yield return new WaitForSeconds(.2f);


        // if application is playing 
        //if (Application.IsPlaying (gameObject)) {

        // trigger data updated event
        EventManager.TriggerEvent("ResolutionUpdated");
        //}

        coroutineRunning = false;
    }


    /**
     *  Update all the resolution parameters
     */
    public void UpdateResolutionParams()
    {
        //Debug.Log ("ResolutionManager.UpdateResolutionParams()");


        // CAMERA / CANVAS
        cameraSize = Camera.main.orthographicSize;
        mainCanvasResolution = new Vector2(mainCanvasRect.sizeDelta.x, mainCanvasRect.sizeDelta.y);

        // PLAYER PARAMS
        playerResolution = new Vector2(Screen.width, Screen.height);
        //gameViewResolution = GetMainGameViewSize ();
        playerViewSize = GetPlayerViewSize();
        playerAspectRatio = playerResolution.x / playerResolution.y;
        playerFullScreen = Screen.fullScreen;

        // DEVICE PARAMS
        deviceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        deviceAspectRatio = deviceResolution.x / deviceResolution.y;

        // WORLD PARAMS
        instantiateContainerLongestSide = (int)PhysicsTools.GetBoundsLongestSide(worldContainer.bounds);
    }

    /**
     *  Update the text in the UI
     */
    void UpdateReport()
    {
        // update UI text 
        resolutionReport1Text.text =
            //"canvasResolution (px): " + canvasResolution.ToString () + "\n" + 
            $"playerResolution (px): {playerResolution.ToString()} \n" +
            //"gameViewResolution (px): " + gameViewResolution.ToString () +
            $"playerViewSize (units): {playerViewSize.ToString()} \n"
            ;
        resolutionReport2Text.text =
            $"deviceResolution: {deviceResolution.ToString()}  \n" +
            $"playerAspectRatio: {(Mathf.Round(playerAspectRatio * 100) / 100).ToString()}:1"
            ;
    }

    void UpdateColliderSize()
    {
        //Debug.Log ("ResolutionManager.UpdateColliderSize() [1]");

        if (playerViewSize.x > 0 && playerViewSize.y > 0)
        {
            // set new size, making sure there isn't a negative number
            worldContainer.size = new Vector3(playerViewSize.x, playerViewSize.y, worldContainer.size.z);

            //Debug.Log ("ResolutionManager.UpdateColliderSize() [2]");
        }
        //Debug.Log ("ResolutionManager.UpdateColliderSize() playerViewSize = " + playerViewSize.ToString ());
        //Debug.Log ("ResolutionManager.UpdateColliderSize() worldContainer.size = " + worldContainer.size.ToString ());
        //Debug.Log ("ResolutionManager.UpdateColliderSize() worldContainer.bounds = " + worldContainer.bounds.ToString ());
    }

    /**
     *  Return the player viewing volume size (in Unity units)
     */
    public static Vector3 GetPlayerViewSize()
    {
        //Debug.Log ("ResolutionManager.GetPlayerViewSize()");

        // The orthographicSize is half of the vertical viewing volume size
        float height = Camera.main.orthographicSize * 2;
        // The horizontal size of the viewing volume depends on the aspect ratio so...
        // width = height * screen resolution aspect ratio
        float width = height * Screen.width / Screen.height;
        // return both
        return new Vector2(width, height);
    }

    /**
     *  Return the editor Game View window size - works in editor
     */
    static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }




















}



