using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnemyLogicAgent : Agent
{
    [SerializeField]
    private GameObject boxManage;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(newEnemyManage.GetCoordinates()[0]); //Enemy X coordinate
        sensor.AddObservation(newEnemyManage.GetCoordinates()[1]); //Enemy Y coordinate

        sensor.AddObservation(newEnemyManage.GetDirection()); //Enemy direction

        sensor.AddObservation(NewPlayerManage.GetCoordinates()[0]); //Player X coordinate
        sensor.AddObservation(NewPlayerManage.GetCoordinates()[1]); //Player Y coordinate

        sensor.AddObservation(newEnemyManage.getDistanceToPlayer()); //Distance to player
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] == 0)
        {
            this.GetComponent<newEnemyManage>().GoUP();
        }
        else if (actions.DiscreteActions[0] == 1)
        {
            this.GetComponent<newEnemyManage>().GoLeft();
        }
        else if (actions.DiscreteActions[0] == 2)
        {
            this.GetComponent<newEnemyManage>().GoRight();
        }


        if (newEnemyManage.checkCurrentPosition() == 2)
        {
            playerPositionReward();
        }
        else
        {
            checkEnemyPositon();
        }
    }

    public override void OnEpisodeBegin()
    {
        boxManage.GetComponent<NewBoxManage>().ResetGame();
        player.GetComponent<NewPlayerManage>().ResetGame();
        enemy.GetComponent<newEnemyManage>().ResetGame();
    }

    private void playerPositionReward()
    {
        SetReward(+1);
        EndEpisode();
    }

    private void checkEnemyPositon()
    {
        int enemyPositionStatus = NewBoxManage.GetMatrix()[NewBox.GetBox(newEnemyManage.getEnemyPosition()).getCoordinates()[0], NewBox.GetBox(newEnemyManage.getEnemyPosition()).getCoordinates()[1]];

        if (enemyPositionStatus == 7)
        {
            SetReward(-1f);
            EndEpisode();
        }
    }
}
