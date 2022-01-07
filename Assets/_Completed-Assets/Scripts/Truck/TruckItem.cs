using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckItem : MonoBehaviour
{
    public float m_cooldownSpawnTime = 7f;
    public float m_currentCooldownSpawnTime = 7f;
    public GameObject m_itemPrefab;
    public Transform m_spawnPoint;

    protected List<SpeedBoost> m_speedBoosts;

    private void Awake()
    {
        m_speedBoosts = new List<SpeedBoost>();
    }

    public void Reset()
    {
        m_currentCooldownSpawnTime = m_cooldownSpawnTime;

        m_speedBoosts.ForEach(boost =>
        {
            if (boost != null)
                Destroy(boost.gameObject);
        });
        m_speedBoosts.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_cooldownSpawnTime > 0)
        {
            m_currentCooldownSpawnTime -= Time.deltaTime;
        }
    }

    public void SpawnItem()
    {
        m_currentCooldownSpawnTime = m_cooldownSpawnTime;

        GameObject item = Instantiate(m_itemPrefab, m_spawnPoint.position, Quaternion.identity);
        m_speedBoosts.Add(item.GetComponent<SpeedBoost>());
    }
}
