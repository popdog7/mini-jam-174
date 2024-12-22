using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BaseEntities[] entity_prefabs;

    private List<BaseEntities> player_team_memebers = new List<BaseEntities>();
    private List<BaseEntities> enemy_team_memebers = new List<BaseEntities>();

    private void Start()
    {
        InstantiateEntity(entity_prefabs[0], 1, new Vector3(45,0, 0));
        InstantiateEntity(entity_prefabs[0], 1, new Vector3(45, 0, 2));
        InstantiateEntity(entity_prefabs[0], 1, new Vector3(45, 0, 4));
        InstantiateEntity(entity_prefabs[0], 2, new Vector3(-45, 0, 0));
        InstantiateEntity(entity_prefabs[0], 2, new Vector3(-45, 0, 2));
        InstantiateEntity(entity_prefabs[0], 2, new Vector3(-45, 0, 4));
    }

    private void InstantiateEntity(BaseEntities entity, int team, Vector3 position)
    {
        BaseEntities new_entitiy = Instantiate(entity, position, Quaternion.identity);
        new_entitiy.Setup(team, position, this);
        GetTeam(team).Add(new_entitiy);
    }

    public List<BaseEntities> GetTeam(int team)
    {
        if (team == 1)
        {
            return player_team_memebers;
        }
        else
        {
            return enemy_team_memebers;
        }
    }

    public List<BaseEntities> GetOpposingTeam(int team)
    {
        if (team == 1)
        {
            return enemy_team_memebers;
        }
        else
        {
            return player_team_memebers;
        }
    }

    public void UnitDeath(BaseEntities entity, int team)
    {
        GetTeam(team).Remove(entity);
        Destroy(entity.gameObject);
    }
}
