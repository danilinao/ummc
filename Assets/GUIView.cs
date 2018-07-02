using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIView : MonoBehaviour {


    public Camera frontViewCamera;
    public Camera topViewCamera;
    public Camera rightViewCamera;
    public Camera perspectiveViewCamera;

    public GUIStyle viewStyle;
    public GUIStyle viewLabelStyle;
    public GUIStyle objectListStyle;

    public Texture wirframe;
    public Texture seletedTexture;
    public Texture wirframeOne;
    
    public RenderTexture frontView;
    public RenderTexture topView;
    public RenderTexture rightView;
    public RenderTexture perspectiveView;

    public float frontmouseSens;
    public float topmouseSens;
    public float rightmouseSens;
    public float perspectivemouseSens;

    public int curViewSelected;
    public int curViewState;

    public GameObject transPosPref;

    public GameObject transPosObj;


    public Vector2 scrollPos;


    public GameObject curObject;

    // Use this for initialization
    void Start () {
        curViewSelected = 0;
        curViewState = -1;
        transPosObj = Instantiate(transPosPref);
        transPosObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (curViewState == -1)
                {
                    curViewState = curViewSelected;
                }
                else
                {
                    curViewState = -1;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (curViewState == -1)
            {
                if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y > Screen.height / 2)
                {
                    curViewSelected = 0;
                    Ray portalRay = frontViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2)) * frontViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2 - 1) * frontViewCamera.pixelHeight, Input.mousePosition.z));
                    RaycastHit portalHit;
                    if (Physics.Raycast(portalRay, out portalHit))
                    {
                        curObject = portalHit.collider.gameObject;

                        Debug.Log(portalHit.collider.gameObject);
                    }
                    else
                    {
                        curObject = null;
                    }
                }
                else if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y < Screen.height / 2)
                {

                    curViewSelected = 1;
                    Ray portalRay = topViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2)) * topViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2) * topViewCamera.pixelHeight, Input.mousePosition.z));
                    RaycastHit portalHit;
                    if (Physics.Raycast(portalRay, out portalHit))
                    {
                        curObject = portalHit.collider.gameObject;
                        Debug.Log(portalHit.collider.gameObject);
                    }
                    else
                    {
                        curObject = null;
                    }
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y > Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 2;
                    Ray portalRay = rightViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2) - 1) * rightViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2 - 1) * rightViewCamera.pixelHeight, Input.mousePosition.z));
                    RaycastHit portalHit;
                    if (Physics.Raycast(portalRay, out portalHit))
                    {
                        curObject = portalHit.collider.gameObject;
                        Debug.Log(portalHit.collider.gameObject);
                    }
                    else
                    {
                        curObject = null;
                    }
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 3;
                    Ray portalRay = perspectiveViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2) - 1) * perspectiveViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2) * perspectiveViewCamera.pixelHeight, Input.mousePosition.z));
                    RaycastHit portalHit;
                    if (Physics.Raycast(portalRay, out portalHit))
                    {
                        curObject = portalHit.collider.gameObject;
                        Debug.Log(portalHit.collider.gameObject);
                    }
                    else
                    {
                        curObject = null;
                    }
                }
            }
            else if(curViewState == 0)
            {
                Ray portalRay = frontViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * frontViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * frontViewCamera.pixelHeight, Input.mousePosition.z));
                RaycastHit portalHit;
                if (Physics.Raycast(portalRay, out portalHit))
                {
                    curObject = portalHit.collider.gameObject;
                    Debug.Log(portalHit.collider.gameObject);
                }
                else
                {
                    curObject = null;
                }
            }
            else if (curViewState == 1)
            {
                Ray portalRay = topViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * topViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * topViewCamera.pixelHeight, Input.mousePosition.z));
                RaycastHit portalHit;
                if (Physics.Raycast(portalRay, out portalHit))
                {
                    curObject = portalHit.collider.gameObject;
                    Debug.Log(portalHit.collider.gameObject);
                }
                else
                {
                    curObject = null;
                }
            }
            else if (curViewState == 2)
            {
                Ray portalRay = rightViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * rightViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * rightViewCamera.pixelHeight, Input.mousePosition.z));
                RaycastHit portalHit;
                if (Physics.Raycast(portalRay, out portalHit))
                {
                    curObject = portalHit.collider.gameObject;
                    Debug.Log(portalHit.collider.gameObject);
                }
                else
                {
                    curObject = null;
                }
            }
            else if (curViewState == 3)
            {
                Ray portalRay = perspectiveViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * perspectiveViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * perspectiveViewCamera.pixelHeight, Input.mousePosition.z));
                RaycastHit portalHit;
                if (Physics.Raycast(portalRay, out portalHit))
                {
                    curObject = portalHit.collider.gameObject;
                    Debug.Log(portalHit.collider.gameObject);
                }
                else
                {
                    curObject = null;
                }
            }
        }
        else if (Input.GetMouseButton(2))
        {
            if (curViewState == -1)
            {
                if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y > Screen.height / 2)
                {
                    curViewSelected = 0;
                    frontViewCamera.transform.position = new Vector3(frontViewCamera.transform.position.x - Input.GetAxis("Mouse X") / frontmouseSens, frontViewCamera.transform.position.y - Input.GetAxis("Mouse Y") / frontmouseSens, frontViewCamera.transform.position.z);
                }
                else if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y < Screen.height / 2)
                {
                    curViewSelected = 1;
                    topViewCamera.transform.position = new Vector3(topViewCamera.transform.position.x - Input.GetAxis("Mouse X") / topmouseSens, topViewCamera.transform.position.y, topViewCamera.transform.position.z - Input.GetAxis("Mouse Y") / topmouseSens);
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y > Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 2;
                    rightViewCamera.transform.position = new Vector3(rightViewCamera.transform.position.x, rightViewCamera.transform.position.y - Input.GetAxis("Mouse Y") / rightmouseSens, rightViewCamera.transform.position.z - Input.GetAxis("Mouse X") / rightmouseSens);
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 3;

                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.right * (Time.deltaTime * perspectivemouseSens);
                    }
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.right * (Time.deltaTime * perspectivemouseSens);
                    }
                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.up * (Time.deltaTime * perspectivemouseSens);
                    }
                    if (Input.GetAxis("Mouse Y") < 0)
                    {
                        perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.up * (Time.deltaTime * perspectivemouseSens);
                    }
                }
            }
            else if (curViewState == 0)
            {
                frontViewCamera.transform.position = new Vector3(frontViewCamera.transform.position.x - Input.GetAxis("Mouse X") / frontmouseSens, frontViewCamera.transform.position.y - Input.GetAxis("Mouse Y") / frontmouseSens, frontViewCamera.transform.position.z);
            }
            else if (curViewState == 1)
            {
                topViewCamera.transform.position = new Vector3(topViewCamera.transform.position.x - Input.GetAxis("Mouse X") / topmouseSens, topViewCamera.transform.position.y, topViewCamera.transform.position.z - Input.GetAxis("Mouse Y") / topmouseSens);
            }
            else if (curViewState == 2)
            {
                rightViewCamera.transform.position = new Vector3(rightViewCamera.transform.position.x, rightViewCamera.transform.position.y - Input.GetAxis("Mouse Y") / rightmouseSens, rightViewCamera.transform.position.z - Input.GetAxis("Mouse X") / rightmouseSens);
            }
            else if (curViewState == 3)
            {
                if (Input.GetAxis("Mouse X") > 0)
                {
                    perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.right * (Time.deltaTime * perspectivemouseSens);
                }
                if (Input.GetAxis("Mouse X") < 0)
                {
                    perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.right * (Time.deltaTime * perspectivemouseSens);
                }
                if (Input.GetAxis("Mouse Y") > 0)
                {
                    perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.up * (Time.deltaTime * perspectivemouseSens);
                }
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.up * (Time.deltaTime * perspectivemouseSens);
                }
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (curViewState == -1)
            {
                if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y > Screen.height / 2)
                {
                    curViewSelected = 0;
                    if (frontViewCamera.orthographicSize > 1)
                    {
                        frontViewCamera.orthographicSize--;
                        frontmouseSens += 0.125f;
                    }
                }
                else if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y < Screen.height / 2)
                {
                    curViewSelected = 1;
                    if (topViewCamera.orthographicSize > 1)
                    {
                        topViewCamera.orthographicSize--;
                        topmouseSens += 0.125f;
                    }
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y > Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 2;
                    if (rightViewCamera.orthographicSize > 1)
                    {
                        rightViewCamera.orthographicSize--;
                        rightmouseSens += 0.125f;
                    }
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 3;
                    perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.forward * (Time.deltaTime * perspectivemouseSens * 5);
                }
            }
            else if (curViewState == 0)
            {
                if (frontViewCamera.orthographicSize > 1)
                {
                    frontViewCamera.orthographicSize--;
                    frontmouseSens += 0.125f;
                }
            }
            else if (curViewState == 1)
            {
                if (topViewCamera.orthographicSize > 1)
                {
                    topViewCamera.orthographicSize--;
                    topmouseSens += 0.125f;
                }
            }
            else if (curViewState == 2)
            {
                if (rightViewCamera.orthographicSize > 1)
                {
                    rightViewCamera.orthographicSize--;
                    rightmouseSens += 0.125f;
                }
            }
            else if (curViewState == 3)
            {
                perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.forward * (Time.deltaTime * perspectivemouseSens * 5);
            }
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (curViewState == -1)
            {
                if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y > Screen.height / 2)
                {
                    curViewSelected = 0;
                    if (frontmouseSens > 0.125f)
                        frontViewCamera.orthographicSize++;
                    if (frontmouseSens > 0.125f)
                        frontmouseSens -= 0.125f;
                }
                else if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y < Screen.height / 2)
                {
                    curViewSelected = 1;
                    if (topmouseSens > 0.125f)
                        topViewCamera.orthographicSize++;
                    if (topmouseSens > 0.125f)
                        topmouseSens -= 0.125f;
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y > Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 2;
                    if (rightmouseSens > 0.125f)
                        rightViewCamera.orthographicSize++;
                    if (rightmouseSens > 0.125f)
                        rightmouseSens -= 0.125f;
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 3;
                    perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.forward * (Time.deltaTime * perspectivemouseSens * 5);
                }
            }
            else if (curViewState == 0)
            {
                if (frontmouseSens > 0.125f)
                    frontViewCamera.orthographicSize++;
                if (frontmouseSens > 0.125f)
                    frontmouseSens -= 0.125f;
            }
            else if (curViewState == 1)
            {
                if (topmouseSens > 0.125f)
                    topViewCamera.orthographicSize++;
                if (topmouseSens > 0.125f)
                    topmouseSens -= 0.125f;
            }
            else if (curViewState == 2)
            {
                if (rightmouseSens > 0.125f)
                    rightViewCamera.orthographicSize++;
                if (rightmouseSens > 0.125f)
                    rightmouseSens -= 0.125f;
            }
            else if (curViewState == 3)
            {
                perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.forward * (Time.deltaTime * perspectivemouseSens * 5);
            }
        }


    }

    void OnGUI()
    {
        if (curViewState == -1)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.height / 2, Screen.height / 2), frontView);
            GUI.DrawTexture(new Rect(Screen.height / 2, 0, Screen.height / 2, Screen.height / 2), rightView);
            GUI.DrawTexture(new Rect(0, Screen.height / 2, Screen.height / 2, Screen.height / 2), topView);
            GUI.DrawTexture(new Rect(Screen.height / 2, Screen.height / 2, Screen.height / 2, Screen.height / 2), perspectiveView);

            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), wirframe);

            GUI.Label(new Rect(0, 0, Screen.height / 2, Screen.height / 2), "Front View", viewLabelStyle);
            GUI.Label(new Rect(Screen.height / 2, 0, Screen.height / 2, Screen.height / 2), "Right View", viewLabelStyle);
            GUI.Label(new Rect(0, Screen.height / 2, Screen.height / 2, Screen.height / 2), "Top View", viewLabelStyle);
            GUI.Label(new Rect(Screen.height / 2, Screen.height / 2, Screen.height / 2, Screen.height / 2), "Perspective View", viewLabelStyle);

            if (curViewSelected == 0)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.height / 2, Screen.height / 2), seletedTexture);
            }
            else if (curViewSelected == 1)
            {
                GUI.DrawTexture(new Rect(0, Screen.height / 2, Screen.height / 2, Screen.height / 2), seletedTexture);
            }
            else if (curViewSelected == 2)
            {
                GUI.DrawTexture(new Rect(Screen.height / 2, 0, Screen.height / 2, Screen.height / 2), seletedTexture);
            }
            else if (curViewSelected == 3)
            {
                GUI.DrawTexture(new Rect(Screen.height / 2, Screen.height / 2, Screen.height / 2, Screen.height / 2), seletedTexture);
            }
        }
        else if(curViewState == 0)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.height , Screen.height ), frontView);
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), wirframeOne);
            GUI.Label(new Rect(0, 0, Screen.height, Screen.height), "Front View", viewLabelStyle);
        }
        else if (curViewState == 1)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), topView);
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), wirframeOne);
            GUI.Label(new Rect(0, 0, Screen.height, Screen.height), "Top View", viewLabelStyle);
        }
        else if (curViewState == 2)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), rightView);
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), wirframeOne);
            GUI.Label(new Rect(0, 0, Screen.height, Screen.height), "Right View", viewLabelStyle);
        }
        else if (curViewState == 3)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), perspectiveView);
            GUI.DrawTexture(new Rect(0, 0, Screen.height, Screen.height), wirframeOne);
            GUI.Label(new Rect(0, 0, Screen.height, Screen.height), "Perspective View", viewLabelStyle);
        }

        GUILayout.BeginArea(new Rect(Screen.height,0,Screen.width-Screen.height,Screen.height));
        scrollPos = GUILayout.BeginScrollView(scrollPos,true, false,GUILayout.Width(Screen.width - Screen.height), GUILayout.Height(300));
        foreach (GameObject cube in GameObject.FindGameObjectsWithTag("BaseModel"))
        {
            if (cube.GetComponent<MeshRenderer>().material.color.a == 0.5f && curObject != cube )
            {
                Color color = cube.GetComponent<MeshRenderer>().material.color;
                color.a = 1.0f;
                cube.GetComponent<MeshRenderer>().material.color = color;
            }
            if (GUILayout.Button(cube.name,GUILayout.Width(Screen.width - Screen.height - (Screen.width - Screen.height) / 10)))
            {
                curObject = cube;
            }
        }
        GUILayout.EndScrollView();
        if(curObject != null)
        {
            transPosObj.transform.position = curObject.transform.position;
            transPosObj.SetActive(true);
            Color color = curObject.GetComponent<MeshRenderer>().material.color;
            color.a = 0.5f;
            curObject.GetComponent<MeshRenderer>().material.color = color;
            float x = curObject.transform.position.x, y = curObject.transform.position.y, z = curObject.transform.position.z;
            GUILayout.Label("Position:");
            GUILayout.BeginHorizontal();
            GUILayout.Label("X:");
            x = float.Parse(GUILayout.TextField(x.ToString()));
            GUILayout.Label("Y:");
            y = float.Parse(GUILayout.TextField(y.ToString()));
            GUILayout.Label("Z:");
            z = float.Parse(GUILayout.TextField(z.ToString()));
            GUILayout.EndHorizontal();
            curObject.transform.position = new Vector3(x, y, z);
        }
        else
        {

            transPosObj.SetActive(false);
        }

        GUILayout.EndArea();

    }
}
