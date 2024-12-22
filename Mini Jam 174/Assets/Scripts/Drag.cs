using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    private GameObject selected_object;
    private float y_offset;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selected_object == null)
            {
                RaycastHit hit = CastHit();
                if (hit.collider != null)
                {
                    if(!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }

                    selected_object = hit.collider.gameObject;
                    y_offset = selected_object.transform.position.y;
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selected_object.transform.position).z);
                Vector3 world_position = Camera.main.ScreenToWorldPoint(position);
                selected_object.transform.position = new Vector3(world_position.x, y_offset, world_position.z);

                selected_object = null;
                Cursor.visible = true;
            }
        }

        if(selected_object != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selected_object.transform.position).z);
            Vector3 world_position = Camera.main.ScreenToWorldPoint(position);
            selected_object.transform.position = new Vector3(world_position.x, y_offset  + .25f, world_position.z);
        }
    }

    private RaycastHit CastHit()
    {
        Vector3 mouse_position_far = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mouse_position_near = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mouse_world_position_far = Camera.main.ScreenToWorldPoint(mouse_position_far);
        Vector3 mouse_world_position_near = Camera.main.ScreenToWorldPoint(mouse_position_near);

        RaycastHit hit;

        Physics.Raycast(mouse_world_position_near, mouse_world_position_far - mouse_world_position_near, out hit);

        return hit;
    }
}
