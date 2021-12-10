using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackedMultiImageManager : MonoBehaviour
{
    [SerializeField] public GameObject[] trackedPrefabs; // 인식했을 때 출력 프리팹

    // 인식했을 때 출력되는 오브젝트 목록
    private Dictionary<string, GameObject> spawnObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        // AR Session Origin 오브젝트에 컴포넌트로 적용했을 때 사용하는 코드
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        foreach(GameObject prefab in trackedPrefabs)
        {
            GameObject clone = Instantiate(prefab); // 오브젝트 생성
            clone.name = prefab.name; // 생성 오브젝트 이름
            clone.SetActive(false); // 오브젝트 비활성화
            spawnObjects.Add(clone.name, clone); //  Dictionary 자료 구조에 오브젝트 저장
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
        // 카메라 이미지 인식
        foreach(var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        // 카메라 이미지 업데이트
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        // 카메라 이미지 사라짐
        foreach (var trackedImage in eventArgs.removed)
        {
            spawnObjects[trackedImage.name].SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject = spawnObjects[name];

        // 이미지 추적 중 상태
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
