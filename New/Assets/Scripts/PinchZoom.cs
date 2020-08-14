using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float orthoZoomSpeed = 0.2f;
    public float movingSpeed = 0.01f;

    Camera camera;

    Vector2 prePos, nowPos, movePos;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        // 카메라 무빙
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                nowPos = touch.position - touch.deltaPosition;
                movePos = (Vector2)(prePos - nowPos) * movingSpeed;

                transform.Translate(movePos);
                
                MoveLimit();

                prePos = touch.position - touch.deltaPosition;
            }
        }
        // 카메라 줌인
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);    // 첫번째 손가락
            Touch touchOne = Input.GetTouch(1);     // 두번째 손가락

            // deltaPos = delttime과같이 delta 만큼 시간동안 움직인 거리
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 현재와 과거값의 움직임의 크기를 구한다.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 2f, 5f);
        }

        // z 축은 고정
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    void MoveLimit()
    {
        Vector2 temp;
        temp.x = Mathf.Clamp(transform.position.x, -7, 7);
        temp.y = Mathf.Clamp(transform.position.y, -4, 4);

        transform.position = temp;
    }
}
