using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject m_SpeedBoostPrefab;
    public Transform m_MaxSpawnPoint;
    public Transform m_MinSpawnPoint;

    protected List<SpeedBoost> m_SpeedBoost;
    // Start is called before the first frame update
    void Start()
    {
        m_SpeedBoost = new List<SpeedBoost>();
    }

    public void StartSpamItem()
    {
        m_SpeedBoost.ForEach(item => Destroy(item.gameObject));
        m_SpeedBoost.Clear();
        StartCoroutine(SpamItem());
    }

    public void StopSpamItem()
    {
        StopCoroutine(SpamItem());
        m_SpeedBoost.ForEach(item =>
        {
            if (item)
                Destroy(item.gameObject);
        });
        m_SpeedBoost.Clear();
    }

    private IEnumerator SpamItem()
    {
        yield return new WaitForSeconds(5f);

        float x = 0;
        float z = 0;
        if (m_MinSpawnPoint.position.x > m_MaxSpawnPoint.position.x)
        {
            x = Random.Range(m_MaxSpawnPoint.position.x, m_MinSpawnPoint.position.x);
        }
        else
        {
            x = Random.Range(m_MinSpawnPoint.position.x, m_MaxSpawnPoint.position.x);
        }
        if (m_MinSpawnPoint.position.z > m_MaxSpawnPoint.position.z)
        {
            z = Random.Range(m_MaxSpawnPoint.position.z, m_MinSpawnPoint.position.z);
        }
        else
        {
            z = Random.Range(m_MinSpawnPoint.position.z, m_MaxSpawnPoint.position.z);
        }
        GameObject item = Instantiate(m_SpeedBoostPrefab, new Vector3(x, 0f, z), Quaternion.identity);
        m_SpeedBoost.Add(item.GetComponent<SpeedBoost>());

        // Wait for the specified length of time until yielding control back to the game loop.
        StartCoroutine(SpamItem());
    }
}
