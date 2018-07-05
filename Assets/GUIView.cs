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
    public GameObject curAxisObject;
    public GameObject curObject;

    public Vector2 scrollPos;


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
            Ray portalRay = new Ray();
            if (curViewState == -1)
            {
                if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y > Screen.height / 2)
                {
                    curViewSelected = 0;
                    portalRay = frontViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2)) * frontViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2 - 1) * frontViewCamera.pixelHeight, Input.mousePosition.z));
                }
                else if (Input.mousePosition.x < Screen.height / 2 && Input.mousePosition.y < Screen.height / 2)
                {
                    curViewSelected = 1;
                    portalRay = topViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2)) * topViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2) * topViewCamera.pixelHeight, Input.mousePosition.z));
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y > Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 2;
                    portalRay = rightViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2) - 1) * rightViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2 - 1) * rightViewCamera.pixelHeight, Input.mousePosition.z));
                }
                else if (Input.mousePosition.x > Screen.height / 2 && Input.mousePosition.y < Screen.height / 2 && Input.mousePosition.x < Screen.height)
                {
                    curViewSelected = 3;
                    portalRay = perspectiveViewCamera.ScreenPointToRay(new Vector3(((Input.mousePosition.x / Screen.width) * Screen.width / (Screen.height / 2) - 1) * perspectiveViewCamera.pixelWidth, ((Input.mousePosition.y / Screen.height) * 2) * perspectiveViewCamera.pixelHeight, Input.mousePosition.z));
                }
            }
            else if (curViewState == 0)
            {
                portalRay = frontViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * frontViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * frontViewCamera.pixelHeight, Input.mousePosition.z));
            }
            else if (curViewState == 1)
            {
                portalRay = topViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * topViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * topViewCamera.pixelHeight, Input.mousePosition.z));
            }
            else if (curViewState == 2)
            {
                portalRay = rightViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * rightViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * rightViewCamera.pixelHeight, Input.mousePosition.z));
            }
            else if (curViewState == 3)
            {
                portalRay = perspectiveViewCamera.ScreenPointToRay(new Vector3((Input.mousePosition.x / Screen.height) * perspectiveViewCamera.pixelWidth, (Input.mousePosition.y / Screen.height) * perspectiveViewCamera.pixelHeight, Input.mousePosition.z));
            }
                RaycastHit portalHit;
            if (portalRay.direction != Vector3.zero)
                if (Physics.Raycast(portalRay, out portalHit))
                {
                    if (portalHit.collider.gameObject.CompareTag("BaseModel"))
                        curObject = portalHit.collider.gameObject;

                    if (portalHit.collider.gameObject.CompareTag("MoveAxes"))
                        curAxisObject = portalHit.collider.gameObject;
                    else
                        curAxisObject = null;
                    Debug.Log(portalHit.collider.gameObject);
                }
                else
                {
                    curObject = null;
                    curAxisObject = null;
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
                    else
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        perspectiveViewCamera.transform.position += perspectiveViewCamera.transform.right * (Time.deltaTime * perspectivemouseSens);
                    }
                    if (Input.GetAxis("Mouse Y") > 0)
                    {
                        perspectiveViewCamera.transform.position += -perspectiveViewCamera.transform.up * (Time.deltaTime * perspectivemouseSens);
                    }
                    else
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
            if (cube.GetComponent<MeshRenderer>().material.color.a == 0.7f && curObject != cube )
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
            color.a = 0.7f;
            curObject.GetComponent<MeshRenderer>().material.color = color;
            CubeProp cubeProp = curObject.GetComponent<CubeProp>();
            GUILayout.Label("Position:");
            GUILayout.BeginHorizontal();
            GUILayout.Label("X:");
            if(cubeProp.xPos.ToString() != "")
                cubeProp.xPos = float.Parse(GUILayout.TextField(cubeProp.xPos.ToString()));
            GUILayout.Label("Y:");
            if (cubeProp.yPos.ToString() != "")
                cubeProp.yPos = float.Parse(GUILayout.TextField(cubeProp.yPos.ToString()));
            GUILayout.Label("Z:");
            if (cubeProp.zPos.ToString() != "")
                cubeProp.zPos = float.Parse(GUILayout.TextField(cubeProp.zPos.ToString()));
            GUILayout.EndHorizontal();
            if(curObject.transform.position != new Vector3(cubeProp.xPos, cubeProp.yPos, cubeProp.zPos))
                curObject.transform.position = new Vector3(cubeProp.xPos, cubeProp.yPos, cubeProp.zPos);

            GUILayout.Label("Rotation:");
            GUILayout.BeginHorizontal();
            GUILayout.Label("X:");
            if (cubeProp.xRot.ToString() != "")
            {
                GUILayout.BeginVertical();
                cubeProp.xRot = float.Parse(GUILayout.TextField(cubeProp.xRot.ToString("000.000")));
                cubeProp.xRot = GUILayout.HorizontalSlider(cubeProp.xRot, 0, 360);
                GUILayout.EndVertical();
            }
            GUILayout.Label("Y:");
            if (cubeProp.yRot.ToString() != "")
            {
                GUILayout.BeginVertical();
                cubeProp.yRot = float.Parse(GUILayout.TextField(cubeProp.yRot.ToString("000.000")));
                cubeProp.yRot = GUILayout.HorizontalSlider(cubeProp.yRot, 0, 360);
                GUILayout.EndVertical();
            }
            GUILayout.Label("Z:");
            if (cubeProp.zRot.ToString() != "")
            {
                GUILayout.BeginVertical();
                cubeProp.zRot = float.Parse(GUILayout.TextField(cubeProp.zRot.ToString("000.000")));
                cubeProp.zRot = GUILayout.HorizontalSlider(cubeProp.zRot, 0, 360);
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            if (curObject.transform.rotation.eulerAngles != new Vector3(cubeProp.xRot, cubeProp.yRot, cubeProp.zRot))
                curObject.transform.rotation = Quaternion.Euler(cubeProp.xRot, cubeProp.yRot, cubeProp.zRot);

            GUILayout.Label("Scale:");
            GUILayout.BeginHorizontal();
            GUILayout.Label("X:");
            if (cubeProp.xScale.ToString() != "")
                cubeProp.xScale = float.Parse(GUILayout.TextField(cubeProp.xScale.ToString()));
            GUILayout.Label("Y:");
            if (cubeProp.yScale.ToString() != "")
                cubeProp.yScale = float.Parse(GUILayout.TextField(cubeProp.yScale.ToString()));
            GUILayout.Label("Z:");
            if (cubeProp.zScale.ToString() != "")
                cubeProp.zScale = float.Parse(GUILayout.TextField(cubeProp.zScale.ToString()));
            GUILayout.EndHorizontal();
            if (curObject.transform.localScale != new Vector3(cubeProp.xScale, cubeProp.yScale, cubeProp.zScale))
                curObject.transform.localScale = new Vector3(cubeProp.xScale, cubeProp.yScale, cubeProp.zScale);
        }
        else
        {
            transPosObj.SetActive(false);
        }

        GUILayout.EndArea();

    }
}
