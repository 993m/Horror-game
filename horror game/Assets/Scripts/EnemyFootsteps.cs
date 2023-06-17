using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public FootstepsOnMaterial[] footstepsSounds;

    public AudioSource enemySoundSource;

    private TerrainIdentifier terrainIdentifier;

    public int publicTerrainTextureIndex;

    public float stepTimer = 0.0f;

    private void Awake()
    {
        terrainIdentifier = new TerrainIdentifier();
    }

    void Update()
    {
        if(stepTimer > 0)
        {
            stepTimer -= Time.deltaTime;
        }
        else
        {
            int terrainTextureIndex = terrainIdentifier.GetActiveTerrainTextureIdx(transform.position);
            publicTerrainTextureIndex = terrainTextureIndex;
            int randomIndex = Random.Range(0, footstepsSounds[terrainTextureIndex].runningSounds.Length);
            AudioClip randomFootstep = footstepsSounds[terrainTextureIndex].runningSounds[randomIndex].clip;
            float randomVolume = footstepsSounds[terrainTextureIndex].runningSounds[randomIndex].volume;
            
            enemySoundSource.PlayOneShot(randomFootstep, randomVolume);
            stepTimer = randomFootstep.length;    
        }
    }
}
