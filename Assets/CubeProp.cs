using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProp : MonoBehaviour {

    public float xPos;
    public float yPos;
    public float zPos;

    public float xRot;
    public float yRot;
    public float zRot;

    public float xScale;
    public float yScale;
    public float zScale;
    
	void Start () {
        xPos = this.gameObject.transform.position.x;
        yPos = this.gameObject.transform.position.y;
        zPos = this.gameObject.transform.position.z;

        xRot = this.gameObject.transform.rotation.x;
        yRot = this.gameObject.transform.rotation.y;
        zRot = this.gameObject.transform.rotation.z;

        xScale = this.gameObject.transform.localScale.x;
        yScale = this.gameObject.transform.localScale.y;
        zScale = this.gameObject.transform.localScale.z;
    }
	
	void Update () {
		
	}
}
