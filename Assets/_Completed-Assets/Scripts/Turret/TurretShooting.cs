using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Rigidbody m_Shell;
    public AudioSource m_ShootingAudio;
    public Transform m_FireTransform;
    public float m_CurrentLaunchForce;
    public TurretAI m_TurretAI;
    public float m_coolDown;
    public float m_nextShot;
    private void Start()
    {
        m_nextShot = 0f;
    }
    public void Fire()
    {
        if (m_nextShot > 0) return;
        m_nextShot = m_coolDown;

        Collider[] colliders = Physics.OverlapSphere(m_FireTransform.position, m_TurretAI.m_shootingRange);
        List<Collider> players = new List<Collider>();

        if (colliders.Length < 1) return;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Players"))
            {
                players.Add(collider);
            }
        }

        if (players.Count < 1) return;
        int idxToShoot = Random.Range(0, players.Count);

        Vector3 targetDir = players[idxToShoot].transform.position - transform.position;

        // calculate rotation
        Vector3 shootDirectionWorldSpace = Vector3.RotateTowards(Vector3.up, targetDir, Mathf.PI / 4, 0f);
        Vector3 shootDirectionLocalSpace = transform.InverseTransformDirection(shootDirectionWorldSpace);
        Quaternion shootRotation = Quaternion.LookRotation(shootDirectionLocalSpace, Vector3.up);

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, shootRotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = Random.Range(0, m_CurrentLaunchForce) * shootDirectionLocalSpace;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.Play();
        return;
    }

    private void Update()
    {
        if (m_nextShot > 0)
        {
            m_nextShot -= Time.deltaTime;
        }
    }
}
