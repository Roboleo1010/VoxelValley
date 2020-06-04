using UnityEditor;
using UnityEngine;
using VoxelValley.Enviroment;

namespace VoxelValley.Entity
{
    public class VoxelCollider : MonoBehaviour
    {
        GameObject colliderBottom;

        private void Start()
        {
            colliderBottom = CreateCollider();
        }

        private void Update()
        {
            Vector3Int posCurrentVoxel = new Vector3Int(Mathf.RoundToInt(transform.position.x), (int)transform.position.y, Mathf.RoundToInt(transform.position.z));
            Vector3Int posBelowVoxel = new Vector3Int(Mathf.RoundToInt(transform.position.x), (int)transform.position.y - 1, Mathf.RoundToInt(transform.position.z));

            ushort currentVoxel = World.Instance.GetVoxelFromWoldSpace(posCurrentVoxel); //The voxel which is inside the  
            ushort belowVoxel = World.Instance.GetVoxelFromWoldSpace(posBelowVoxel); //The voxel the player stands on

            if (currentVoxel != 0)
            {
                colliderBottom.SetActive(true);
                colliderBottom.transform.position = posCurrentVoxel;
            }
            else if (belowVoxel != 0)
            {
                colliderBottom.SetActive(true);
                colliderBottom.transform.position = posBelowVoxel;
            }
            else
                colliderBottom.SetActive(false);
        }

        void OnDrawGizmos()
        {
            if (colliderBottom != null && colliderBottom.activeSelf)
                DrawGizmoCollider(colliderBottom.transform.position);

            DrawGizmoCenterPos();
        }

        GameObject CreateCollider()
        {
            GameObject collider = new GameObject();
            collider.name = "VoxelCollider";
            collider.layer = 17;

            BoxCollider bc = collider.AddComponent<BoxCollider>();
            bc.center = new Vector3(0, 0.5f, 0);

            collider.SetActive(false);

            return collider;
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        void DrawGizmoCollider(Vector3 position)
        {
            Gizmos.color = new Color(0, 1, 0);
            Gizmos.DrawCube(new Vector3(position.x, position.y + 0.5f, position.z), new Vector3(1, 1, 1));
        }

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        void DrawGizmoCenterPos()
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawSphere(transform.position, 0.05f);
        }
    }
}