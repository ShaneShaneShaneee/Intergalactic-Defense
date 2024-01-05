using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Quaternion originalRotation, fpsRotation;
    float panSpeed = 50f, panBorderThickness = 10f, scrollSpeed = 15f, minY = 15f, maxY = 80f, currentZoom = 0f, zoomRot = 2f, zoomSpeed = 100f,
        minZ = -35f, maxZ = 60f, minX = 14f, maxX = 70f;
    Vector3 camRotY, originalPosition;
    bool fpsView = false, canPan = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        fpsRotation = Quaternion.Euler(15f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (LifeMonitor.gameEnded)
        {
            this.enabled = false;
            return;
        }

        //Cam Movement
        #region 
        if (canPan)
        {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                if (fpsView)
                {
                    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.Self);
                }
                else
                {
                    transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
                }
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                if (fpsView)
                {
                    transform.Translate(Vector3.down * 5 * panSpeed * Time.deltaTime, Space.Self);
                }
                else
                {
                    transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
                }
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                if (fpsView)
                {
                    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.Self);
                }
                else
                {
                    transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
                }
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                if (fpsView)
                {
                    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.Self);
                }
                else
                {
                    transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
                }
            }
        }

        #endregion

        //Cam Rotation
        #region
        if (Input.GetKey("q"))
        {
            if (fpsView)
            {
                Vector3 rotation = Vector3.up * zoomRot * Time.deltaTime * panSpeed;
                transform.Rotate(rotation, Space.Self);
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                Vector3 rotation = Vector3.up * Time.deltaTime * panSpeed;
                transform.Rotate(rotation, Space.World);
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }

        if (Input.GetKey("e"))
        {
            if (fpsView)
            {
                Vector3 rotation = Vector3.down * zoomRot * Time.deltaTime * panSpeed;
                transform.Rotate(rotation, Space.Self);
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                Vector3 rotation = Vector3.down * Time.deltaTime * panSpeed;
                transform.Rotate(rotation, Space.World);
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
            }
        }
        #endregion

        //Cam Zoom
        #region
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;

        float scroll = Input.GetAxis("Mouse ScrollWheel");


        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

        if (pos.y <= minY && !fpsView)
        {
            transform.rotation = fpsRotation;
            fpsView = true;
        }
        else if (pos.y > minY && fpsView)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * zoomSpeed + 10);
            transform.position = originalPosition;
            fpsView = false;
        }

        if (pos.y >= maxY && transform.position != originalPosition)
        {
            transform.position = originalPosition;
        }

        if (pos.y < maxY)
        {
            canPan = true;
        }
        else
        {
            canPan = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.rotation = originalRotation;
            transform.position = originalPosition;
            fpsView = false;
        }
        #endregion
    }
}
