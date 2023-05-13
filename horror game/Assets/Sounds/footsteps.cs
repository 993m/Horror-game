using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AudioClipAndVolume
{
    public AudioClip clip;
    [Range(0.0f, 1.0f)]
    public float volume;
}

[System.Serializable]
public class FootstepsOnMaterial
{
    public string materialName;
    public AudioClipAndVolume[] walkingSounds;
    public AudioClipAndVolume[] runningSounds;
}

public class footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    public FootstepsOnMaterial[] footstepsSounds;

    public AudioSource playerSoundSource;

    public PlayerMovement playerMovementComponent;

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
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                int terrainTextureIndex = terrainIdentifier.GetActiveTerrainTextureIdx(transform.position);
                publicTerrainTextureIndex = terrainTextureIndex;
                if(!playerMovementComponent.isSprinting)
                {
                    int randomIndex = Random.Range(0, footstepsSounds[terrainTextureIndex].walkingSounds.Length);
                    AudioClip randomFootstep = footstepsSounds[terrainTextureIndex].walkingSounds[randomIndex].clip;
                    float randomVolume = footstepsSounds[terrainTextureIndex].walkingSounds[randomIndex].volume;
                    
                    playerSoundSource.PlayOneShot(randomFootstep, randomVolume);
                    stepTimer = randomFootstep.length;
                }
                else
                {
                    int randomIndex = Random.Range(0, footstepsSounds[terrainTextureIndex].runningSounds.Length);
                    AudioClip randomFootstep = footstepsSounds[terrainTextureIndex].runningSounds[randomIndex].clip;
                    float randomVolume = footstepsSounds[terrainTextureIndex].runningSounds[randomIndex].volume;
                    
                    playerSoundSource.PlayOneShot(randomFootstep, randomVolume);
                    stepTimer = randomFootstep.length;
                }
            }    
        }
    }
}
