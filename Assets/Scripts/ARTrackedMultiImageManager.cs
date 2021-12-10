using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedMultiImageManager : MonoBehaviour
{
    [SerializeField] public GameObject[] trackedPrefabs; // �ν����� �� ��� ������

    // �ν����� �� ��µǴ� ������Ʈ ���
    private Dictionary<string, GameObject> spawnObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        // AR Session Origin ������Ʈ�� ������Ʈ�� �������� �� ����ϴ� �ڵ�
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach(GameObject prefab in trackedPrefabs)
        {
            GameObject clone = Instantiate(prefab); // ������Ʈ ����
            clone.name = prefab.name; // ���� ������Ʈ �̸�
            clone.SetActive(false); // ������Ʈ ��Ȱ��ȭ
            spawnObjects.Add(clone.name, clone); //  Dictionary �ڷ� ������ ������Ʈ ����
        }
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // ī�޶� �̹��� �ν�
        foreach(var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        // ī�޶� �̹��� ������Ʈ
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        // ī�޶� �̹��� �����
        foreach (var trackedImage in eventArgs.removed)
        {
            spawnObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject = spawnObjects[name];

        // �̹��� ���� �� ����
        if(trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            trackedObject.SetActive(true);
        }
        else
        {
            trackedObject.SetActive(false);
        }
    }
}
