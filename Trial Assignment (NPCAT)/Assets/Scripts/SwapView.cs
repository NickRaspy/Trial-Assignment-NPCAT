using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwapView : MonoBehaviour
{
    [SerializeField] private Transform mainPointOfRotation;
    [SerializeField] private Transform reductor;
    [SerializeField] private Button reductorButton;
    [SerializeField] private float swapSpeed = 1f;
    private GameObject componentButton;
    public GameObject ComponentButton
    {
        get {  return componentButton; } set {  componentButton = value; }
    }
    public bool isInComponent;
    public Transform camera;
    private Coroutine currentCoroutine;
    private const float epsilon = 0.00000001f;
    public void FlyToObject(Transform component)
    {
        if (!isInComponent)
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(Fly(component.Find("Point")));
            foreach (Transform t in reductor)
            {
                if (t != component) t.gameObject.SetActive(false);
            }
            foreach (Transform t in transform)
            {
                if (t.gameObject != componentButton) t.gameObject.GetComponent<Button>().interactable = false;
            }
            reductorButton.interactable = false;
        }
        else
        {
            mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius = 5f;
            mainPointOfRotation.GetComponent<RotationScript>().minColliderRadius = 4f;
            mainPointOfRotation.GetComponent<RotationScript>().maxColliderRadius = 7f;
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(Fly(GameObject.Find("Main Point").transform));
            foreach (Transform t in reductor)
            {
                t.gameObject.SetActive(true);
            }
            foreach (Transform t in transform)
            {
                t.gameObject.GetComponent<Button>().interactable = true;
            }
            reductorButton.interactable = true;
        }
        isInComponent = !isInComponent;
    }
    IEnumerator Fly(Transform parent)
    {
        mainPointOfRotation.SetParent(parent);
        mainPointOfRotation.localPosition = Vector3.zero;
        if(mainPointOfRotation.GetComponent<SphereCollider>().radius > mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius)
            while (mainPointOfRotation.GetComponent<SphereCollider>().radius > mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius)
            {
                mainPointOfRotation.GetComponent<SphereCollider>().radius -= Time.deltaTime * swapSpeed;
                camera.localPosition = new(camera.localPosition.x, camera.localPosition.y, camera.localPosition.z + Time.deltaTime * swapSpeed);
                yield return new WaitForEndOfFrame();
            }
        else
            while (mainPointOfRotation.GetComponent<SphereCollider>().radius < mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius)
            {
                mainPointOfRotation.GetComponent<SphereCollider>().radius += Time.deltaTime * swapSpeed;
                camera.localPosition = new(camera.localPosition.x, camera.localPosition.y, camera.localPosition.z - Time.deltaTime * swapSpeed);
                yield return new WaitForEndOfFrame();
            }
        mainPointOfRotation.GetComponent<SphereCollider>().radius = mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius;
        camera.localPosition = new(camera.localPosition.x, camera.localPosition.y, -mainPointOfRotation.GetComponent<RotationScript>().standartColliderRadius - 1f);
    }
}
