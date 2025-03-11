using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerEyeLookAt : MonoBehaviour
{
    [Header ("Eye objects")]
    public GameObject objEyeL;
    public GameObject objEyeR;
    private Renderer renderEyeL, renderEyeR;

    Transform objPivotEye; // LookAt 참조 게임 오브젝트
    [Space (8f)]
    public Transform objPivotLookAt; // 바라보는 지점

    [Header("Eye Limit")]
    // 눈 왼쪽 이동범위
    public float eyeLMin;
    public float eyeLMax;
    [Space (8f)]
    // 눈 오른쪽 이동범위
    public float eyeRMin;
    public float eyeRMax;

    [Space (12f)]
    // 양눈 Y offset 이동범위
    public float eyeY_min;
    public float eyeY_max;

    void Start()
    {
        // LookAt 피봇 생성 (왼쪽 눈)
        if (objPivotEye == null)
        {
            GameObject pivotEyeL = new GameObject("pivotEyeL");
            pivotEyeL.transform.parent = objEyeL.transform;
            pivotEyeL.transform.localPosition = Vector3.zero;
            pivotEyeL.transform.localRotation = Quaternion.identity;
            objPivotEye = pivotEyeL.transform;
        }

        renderEyeL = objEyeL.GetComponent<Renderer>();
        renderEyeR = objEyeR.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        objPivotEye.LookAt(objPivotLookAt);

        Vector2 tempEyeRot = new Vector2(objPivotEye.localRotation.y, objPivotEye.localRotation.x);

        float tempEyeL_limit = Mathf.Clamp(tempEyeRot.x, eyeLMin, eyeLMax);
        float tempEyeR_limit = Mathf.Clamp(tempEyeRot.x, eyeRMin, eyeRMax);
        float tempEyeY_limit = Mathf.Clamp(tempEyeRot.y, eyeY_min, eyeY_max);

        // 참고 : 셰이더의 변수 확인 - default : URP Standard Shader
        renderEyeL.material.SetTextureOffset("_BaseMap", new Vector2 (tempEyeL_limit, tempEyeY_limit));
        renderEyeR.material.SetTextureOffset("_BaseMap", new Vector2 (tempEyeR_limit, tempEyeY_limit));

    }
}
