using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    
private Vector3 screenPoint;
private Vector3 offset;
private Vector3 startPosition;
private Quaternion startRotation;
private bool atStart = true;

 void Start (){
   startPosition = transform.position;
   startRotation = transform.rotation;
 }

 
 void Update (){
   if (Input.GetMouseButtonUp(0) && !atStart){
      this.transform.position = startPosition;
      this.transform.rotation = startRotation;
   }
}

 void OnMouseDown(){
    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
    offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
 }

 void OnMouseDrag(){
    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    transform.position = curPosition;
    atStart = false;
 }
}
