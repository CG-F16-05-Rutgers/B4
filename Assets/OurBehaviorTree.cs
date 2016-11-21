using UnityEngine;
using System;
using System.Collections;
using TreeSharpPlus;

public class OurBehaviorTree : MonoBehaviour
{
    public Transform wander1;
    public Transform wander2;
    public Transform wander3;
    public GameObject participant1;
    public GameObject participant2;
    public GameObject participant3;
    public GameObject[] participants;
    public GameObject specialDaniel;

    public GameObject camera;
    //private ArrayList<GameObject> participants;

    public int winCount = 0;
    public GameObject door;
    public GameObject doorKnob;
    public GameObject police;
    private bool danielCheck = true;
    public bool doorOpen = false;

    private BehaviorAgent behaviorAgent1, behaviorAgent2, behaviorAgent3;
    private int index = 0;
    // Use this for initialization
    void Start()
    {
        
        Vector3 doorPosition = door.transform.position;
        Vector3 doorPositionNew = doorPosition + new Vector3(3.0F, 0, 0);

        door.transform.position = doorPositionNew;

        behaviorAgent1 = new BehaviorAgent(this.BuildTreeRoot()[0]);
        BehaviorManager.Instance.Register(behaviorAgent1);
        behaviorAgent2 = new BehaviorAgent(this.BuildTreeRoot()[1]);
        BehaviorManager.Instance.Register(behaviorAgent2);
       // behaviorAgent3 = new BehaviorAgent(this.BuildTreeRoot()[2]);
       // BehaviorManager.Instance.Register(behaviorAgent3);
        behaviorAgent1.StartBehavior();
        behaviorAgent2.StartBehavior();
      //  behaviorAgent3.StartBehavior();
    }

    // Update is called once per frame
    void Update()
    {
        
            if (winCount >= 3)
            {
                police.gameObject.SetActive(false);
                Time.timeScale = 0.0f;
            }
    }

    protected Node ST_ApproachAndWait(Transform target, Transform participant)
    {
        Debug.Log("Magical Function");
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_RunAndWait(Transform target, Transform participant)
    {
        Val<Vector3> position = Val.V(() => participant.position - target.position + participant.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node CompleteOpenDoor(Transform participant)
    {
        Val<Vector3> position = Val.V(() => door.transform.position);
        return
            new Sequence(

                         participant.GetComponent<BehaviorMecanim>().ST_TurnToFace(position),
                         //ST_ApproachAndWait(door.transform, participant),
                         participant.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 2),
                         participant.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("POINTING", 1000),
                         new LeafWait(1000),
                         OpenDoor(),
                         participant.GetComponent<BehaviorMecanim>().Node_GoTo(this.wander1.position),
                         
                         CloseDoor()
                         );

    }

    protected Node OpenDoor()
    {

        return new LeafInvoke(() =>
        {

            Vector3 doorPosition = door.transform.position;
            Vector3 doorPositionNew = doorPosition + new Vector3(3.0F, 0, 0);

            door.transform.position = doorPositionNew;
        });
    }

    protected Node CloseDoor()
    {
        return new LeafInvoke(() =>
        {
            Vector3 doorPosition = door.transform.position;
            Vector3 doorPositionNew = doorPosition + new Vector3(-3.0F, 0, 0);
            door.transform.position = doorPositionNew;
        });
    }

    private Vector3 calcMinDistance(Vector3 minDistance)
    {
        foreach (GameObject daniel in participants)
        {
            if ((daniel.transform.position - police.transform.position).sqrMagnitude < minDistance.sqrMagnitude)
                minDistance = daniel.transform.position - police.transform.position;

        }
        return minDistance;

    }

    protected Node ChaseDaniels(Transform target, Transform daniel)
    {
        Debug.Log("attempting to go to " + target.position + daniel.position);
        Val<Vector3> position = Val.V(() => (daniel.position - target.position) + target.position);
        return new Sequence(target.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node[] BuildTreeRoot()
    {
        
        
        ForEach<GameObject> returnValue = new ForEach<GameObject>((participant) =>
        {
            Func<bool> act = () => ((Math.Abs(police.transform.position.z - participant.transform.position.z) < 5) && (Math.Abs(police.transform.position.x - participant.transform.position.x) < 5));
            Func<bool> act2 = () => ((Math.Abs(door.transform.position.z - participant.transform.position.z) < 5) && (Math.Abs(door.transform.position.x - participant.transform.position.x) < 5));
            Func<bool> act4 = () => ((Math.Abs(police.transform.position.z - participant.transform.position.z) >= 5) && (Math.Abs(police.transform.position.x - participant.transform.position.x) >= 5));
            Node trigger = new DecoratorLoop(new LeafAssert(act));
            Node trigger4 = new DecoratorLoop(new LeafAssert(act4));
            Node roaming = 
                new SequenceParallel(
                   new DecoratorForceStatus(RunStatus.Success,ST_RunAndWait(police.transform, participant.transform)), new LeafInvoke(() => { participant.GetComponent<CanvasController>().enableSpeech(); participant.GetComponent<CanvasController>().setText("AHH, RUN!"); }))
                    ;
            Node clearText = new LeafInvoke(() => { participant.GetComponent<CanvasController>().disableSpeech(); });
            Node trigger2 = new DecoratorLoop(new LeafAssert(act2));
            
            Node open = new DecoratorLoop(
                new Sequence(CompleteOpenDoor(specialDaniel.transform)));
            Node root = new DecoratorLoop(new Sequence(new SequenceParallel(new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger, roaming)), 
                new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger4, clearText))), 
                new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger2, open))));
            return root;
        }, participants);

        Func<bool> act3 = () => (1 < 0);
        Node trigger3 = new LeafInvoke(() => RunStatus.Failure);
        Node temp =  new Sequence(new LeafInvoke(() => {
            if (index < 2)
        {
            index++;
        }
        else
        {
            index = 0;
        }
            Debug.Log("leafinvoke index: " + index);
            
        }), new LeafWait(5000));
        //Vector3 policeVector = new Vector3(0,0,0);

        Node chasing = new DecoratorLoop(
                new Sequence(
                     ChaseDaniels(police.transform, participants[index].transform)));
        Func<bool> danny0 =  () => (index == 0);
        Func<bool> danny1 =  () => (index == 1);
        Func<bool> danny2 =  () => (index == 2);
        Node dannyTest0 = new LeafAssert(danny0);
        Node dannyTest1 = new LeafAssert(danny1);
        Node dannyTest2 = new LeafAssert(danny2);
        
        Node check = new LeafInvoke(() => {
        foreach(GameObject participant in participants)
        {
             Func<bool> measure = () => ((Math.Abs(police.transform.position.z - participant.transform.position.z) < 1) && (Math.Abs(police.transform.position.x - participant.transform.position.x) < 1));
             Node trigger = new DecoratorLoop(new LeafAssert(measure));
             Node root = new Sequence(new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger, ChaseDaniels(police.transform, participant.transform))));
        } });
        Node change = new Sequence(new Race(new LeafWait(10000), ChaseDaniels(police.transform, participants[0].transform)), new Race(new LeafWait(10000), ChaseDaniels(police.transform, participants[1].transform)), new Race(new LeafWait(10000), ChaseDaniels(police.transform, participants[2].transform)));
       
        //Node trigger = new DecoratorLoop(new LeafAssert(measure));
        //Node change2 = 
        Node returnValue2 = new DecoratorLoop(change);

       // Node returnValue3 = new DecoratorLoop(new DecoratorForceStatus(RunStatus.Success, new LeafInvoke(() => { CompleteOpenDoor(specialDaniel.transform); })));

        Node[] rv = new Node[2];
        rv[0] = returnValue;
        rv[1] = returnValue2;
       // rv[2] = returnValue3;
        return rv;

        //Vector3 min = (participants[0].transform.position - police.transform.position);
        //int index = 0;

      
        /*
        Node chase =
                new Sequence(new LeafInvoke(() => {
                    //min = (participants[0].transform.position - police.transform.position);
                    //for (int i = 1; i < participants.Length; i++)
                    //{
                    //    if ((Math.Pow((police.transform.position.x - participants[i].transform.position.x), 2) + Math.Pow((police.transform.position.z - participants[i].transform.position.z), 2) < min.sqrMagnitude))
                    //    {
                    //        min = participants[i].transform.position - police.transform.position;
                    //        index = i;
                    //    }
                    //
                    if (index < 2)
                        index++;
                    else
                        index = 0;
                }),ST_ApproachAndWait(participants[index].transform, police.transform));
               // new Sequence(ChaseDaniels(police.transform, participants)));
        */
        //return new Sequence(new DecoratorLoop(new DecoratorForceStatus(RunStatus.Success, new SequenceParallel(trigger3, chasing))), returnValue);
        //return new Sequence(new DecoratorLoop(new SequenceParallel(new Sequence(chasing, new LeafWait(1000)),returnValue)));
        //return returnValue;
    }
}
