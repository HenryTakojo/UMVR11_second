using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepDistance : MonoBehaviour
{
    private List<GameObject> enemyAround = new List<GameObject>();

    private float targetVelocity = 10.0f;
    private int numofRay = 8;
    private float angle = 90;
    private float rayRange = 2;
    Vector3 hitVec;
    private SimpleFSM2 sfm;

    private float radius;
    private float yPos;

    LayerMask unwalkLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        unwalkLayerMask = LayerMask.GetMask("UnwalkLayer");
        Debug.Log("unwalkLayerMaskValue:  " +unwalkLayerMask);
        float fDist = 50.0f;
        GameObject[] allenemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (allenemy != null || allenemy.Length > 0)
        {
            foreach (GameObject go in allenemy)
            {
                Vector3 vDis = go.transform.position - transform.position;
                if(vDis.magnitude <= fDist)
                {
                    enemyAround.Add(go);
                }
            }
            enemyAround.Remove(this.gameObject);
        }
        sfm = this.GetComponent<SimpleFSM2>();
        radius = this.GetComponent<SimpleFSM2>().m_Data.m_fRadius;
        yPos = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < numofRay; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (((float)numofRay) - 1)) * angle * 2, this.transform.up);
            var direction = rotation * rotationMod * transform.forward;

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {

                if (1 << hitInfo.collider.gameObject.layer == unwalkLayerMask)
                {
                    hitVec = hitInfo.normal; // hitVec is normal vec
                    Vector3 vToCol = hitInfo.collider.gameObject.transform.position - transform.position; //vToCol ���I���餤��
                    vToCol.y = 0;
                    float fDis = vToCol.magnitude;
                    vToCol.Normalize();
                    Vector3 vFor = transform.forward;
                    float fColWay = Vector3.Dot(vFor, vToCol); //�P�_�O�_�¦V�I����
                    float fDot = Vector3.Dot(vFor, hitVec);
                    Vector3 vTurn = hitVec * fDot;
                    Vector3 vFinal = vFor + vTurn;
                    Vector3 vFinalMove = vFinal * Time.deltaTime;
                    transform.position -= vFinalMove;

                    //if (fDot > 0)
                    //{

                    //}
                    //else if (fDis > 0)
                    //{

                    //}

                }
            }
        }

        if (enemyAround != null && enemyAround.Count > 0)
        {
            foreach (var go in enemyAround)
            {

                Vector3 vDis = go.transform.position - transform.position;
                float fDis = vDis.magnitude;
                vDis.Normalize();
                float otherR = go.GetComponent<SimpleFSM2>().m_Data.m_fRadius;

                if (fDis < radius + otherR +0.1f)
                {
                    Vector3 keepPos = this.transform.position + vDis * (radius + otherR + 0.1f);
                    keepPos.y = yPos;
                    go.transform.position = keepPos;
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < numofRay; i++)
        {
            var rotation = this.transform.rotation;
            var rotationMod = Quaternion.AngleAxis((i / (((float)numofRay) - 1)) * angle * 2 , this.transform.up);
            var direction = rotation * rotationMod * transform.forward;
            Gizmos.DrawRay(this.transform.position, direction *2);

            var ray = new Ray(this.transform.position, direction);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, rayRange))
            {
                
                if (1<< hitInfo.collider.gameObject.layer == unwalkLayerMask)
                {
                    Debug.Log("hitInfolayer: " + hitInfo.collider.gameObject.layer);
                    Gizmos.DrawSphere(transform.position, rayRange);
                }
            }
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 2);

        if (hitVec != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, hitVec * 5);
        }
    }

}
