using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotationScript : MonoBehaviour
{
    private Vector3 lastMousePosition;
    bool focused = false;

    private void OnMouseDown()
    {
        if (!focused)
        {
            focused = true;
            lastMousePosition = Input.mousePosition;
            EnterPlanetCutScene();
        }
    }

    private void OnMouseDrag()
    {
        if (focused)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;

            // Calculate the rotation angles based on mouse movement
            float rotationX = -mouseDelta.y;
            float rotationY = mouseDelta.x;

            // Apply rotation to the object
            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.World);

            lastMousePosition = currentMousePosition;
        }
    }

    [Header("Managable UI")]
    [SerializeField]
    GameObject planetDetailsUI;

    [SerializeField]
    GameObject mainMenUI;

    [Header("Cameras and POVs")]
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    GameObject planetCamera;

    [Header("Numbers and Values")]
    [SerializeField]
    float camMovementSpeed;
    [SerializeField]
    float camRotationSpeed;




    private Vector3 oldCamPosition;
    private Quaternion oldCamRotation;
    private Vector3 targetCamPosition;
    private Quaternion targetCamRotation;
    private float elapstedTime = 0f;

    public void EnterPlanetCutScene()
    {
        gameObject.GetComponent<CircularPathScript>().enabled = false;
        oldCamPosition = mainCamera.gameObject.transform.position;
        oldCamRotation = mainCamera.gameObject.transform.rotation;
        targetCamPosition = planetCamera.transform.position;
        targetCamRotation = planetCamera.transform.rotation;

        planetDetailsUI.SetActive(true);
        mainMenUI.SetActive(false);

        StartCoroutine(LerpCamera());
    }

    private IEnumerator LerpCamera()
    {
        while (mainCamera.transform.position != targetCamPosition || mainCamera.transform.rotation != targetCamRotation)
        {
            elapstedTime += Time.deltaTime;
            float lerpIncrement = elapstedTime / camMovementSpeed;

            if (mainCamera.transform.position != targetCamPosition)
                mainCamera.gameObject.transform.position = Vector3.Lerp(oldCamPosition, targetCamPosition, lerpIncrement);
            if (mainCamera.transform.rotation != targetCamRotation)
                mainCamera.gameObject.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, targetCamRotation, camRotationSpeed);

            yield return null;
        }

        elapstedTime = 0;
    }

    public void ExitPlanetCutScene()
    {
        planetDetailsUI.SetActive(false);
        mainMenUI.SetActive(true);
        focused = false;
        gameObject.GetComponent<CircularPathScript>().enabled = true;
        mainCamera.gameObject.transform.position = oldCamPosition;
        mainCamera.gameObject.transform.rotation = oldCamRotation;
        StopAllCoroutines();
    }

}