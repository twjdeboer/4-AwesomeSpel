using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Astar : MonoBehaviour{

    //Astar
	public GameObject startNode;
    public GameObject endNode;
    public List<GameObject> openList = new List<GameObject>();
    public List<GameObject> closedList = new List<GameObject>();
    private bool useAstar;
    public List<GameObject> path;
    public Vector3 startPos;
    public Vector3 endPos;
    public NodeNetwork nodes;
    public int NPCNumber;
    private float time = 0;

    //Walking
    public float moveSpeed;
    public float rotateSpeed;
    public bool stopWalking;

    private int index = 1;

    //Methods
    
    /**
     * Set the parameters for algorithm and excute it.
     * */
    List<GameObject> MakePad(Vector3 startPos, Vector3 endPos)
    {
        this.endNode = nodes.Closest(endPos);
        this.startNode = nodes.Closest(startPos);
        ReachableNodes(startNode, null);
        GameObject previousNode = startNode;
        GameObject CurrentNode;
        while (!ReachedEnd(closedList))
        {
            if (openList.Count != 0)
            {
                CurrentNode = LowestCost(openList);
                ReachableNodes(CurrentNode, previousNode);
            }
            else
            {
                List<GameObject> returnThis = new List<GameObject>();
                returnThis.Add(startNode);
                return returnThis;
            }
        }
        return ConstructPath(closedList);
    }

    /**
     * Checks whether the adjacent by nodes are accesable and update their costs.
     * */
    void ReachableNodes(GameObject parentNode, GameObject previousNode)
    {
        closedList.Add(parentNode);
        openList.Remove(parentNode);
        Vector2 nodePos = NodeNetwork.Node2Vector(parentNode);

        //Checking adjacent nodes
        for (int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if (nodes.IsNodeInNetwork((int)nodePos.x + i, (int)nodePos.y + j))
                {
                    GameObject CheckNode = nodes.getNode((int)nodePos.x + i, (int)nodePos.y + j);

                    //set costs if node is accesable and not on open/closed list.
                    if (!closedList.Contains(CheckNode) && !openList.Contains(CheckNode) && CheckNode.GetComponent<Node>().accesable)
                    {
                        CheckNode.GetComponent<Node>().parentNode[NPCNumber] = parentNode;
                        CheckNode.GetComponent<Node>().G[NPCNumber] = G(parentNode, CheckNode);
                        CheckNode.GetComponent<Node>().H[NPCNumber] = H(CheckNode);
                        CheckNode.GetComponent<Node>().F[NPCNumber] = G(parentNode, CheckNode) + H(CheckNode);
                        openList.Add(CheckNode);

                    }

                        //update G if node in open list and G lower for new parent.
                    else if (openList.Contains(CheckNode))
                    {

                        if (G(parentNode, CheckNode) < CheckNode.GetComponent<Node>().G[NPCNumber])
                        {
                            int index = openList.IndexOf(CheckNode);
                            CheckNode.GetComponent<Node>().G[NPCNumber] = G(parentNode, CheckNode);
                            CheckNode.GetComponent<Node>().H[NPCNumber] = H(CheckNode);
                            CheckNode.GetComponent<Node>().F[NPCNumber] = G(parentNode, CheckNode) + H(CheckNode);
                            openList[index] = CheckNode;

                        }
                    }
                }
            }
        }

    }

    /**
     * Checks if the TargetNode is in the closedList.
     * */
    bool ReachedEnd(List<GameObject> closedList)
    {
        return closedList.Contains(endNode);
    }

    /**
     * The Movement cost.
     * */
    int G(GameObject thisOne, GameObject other)
    {
        int GthisOne = thisOne.GetComponent<Node>().G[NPCNumber];
        Vector3 thisPos = new Vector3(thisOne.transform.position.x, 0, thisOne.transform.position.z);
        Vector3 otherPos = new Vector3(other.transform.position.x, 0, other.transform.position.z); 
        return Mathf.RoundToInt(Mathf.Abs(Vector3.Distance(thisPos, otherPos) * 10)+ GthisOne);
    }

    /**
     * Estimated movement cost. The Heuristic part.
     * */
    int H(GameObject thisNode)
    {
        return Mathf.RoundToInt(10 * (Mathf.Abs(thisNode.transform.position.x - endNode.transform.position.x) + Mathf.Abs(thisNode.transform.position.z - endNode.transform.position.z)));
    }

    /**
     * Determines the node with the lowest cost.
     * */
    GameObject LowestCost(List<GameObject> openList)
    {
        GameObject BestNode = openList[0];
        int bestCost = BestNode.GetComponent<Node>().F[NPCNumber];
        for (int i = 1; i < openList.Count; i++)
        {
            if (openList[i].GetComponent<Node>().F[NPCNumber] < bestCost)
            {
                bestCost = openList[i].GetComponent<Node>().F[NPCNumber];
                BestNode = openList[i];
            }
        }
        return BestNode;
    }

    /**
     * Constructs path out of the closed list by taking parent nodes.
     * */
    List<GameObject> ConstructPath(List<GameObject> closedList)
    {
        //Set first and second node in path: the node at the target position and its parent.
        List<GameObject> path = new List<GameObject>();
        path.Add(endNode);
        GameObject parentNode = endNode.GetComponent<Node>().parentNode[NPCNumber];
        path.Add(parentNode);

        //Taking parent of previous node.
        int i = 2;
        while(!parentNode.Equals(startNode))
        {
            path.Add(parentNode.GetComponent<Node>().parentNode[NPCNumber]);
            parentNode = path[i];
            i++;
        }

        //Path begin at end node. So it needs to be reversed.
        path.Reverse();
        return path;
    }

    //Makes the NPC Walk.
    void NPCWalk(float moveSpeed, float rotateSpeed, Vector3 targetPos)
    {
        
        Vector3 moveDirection = targetPos - transform.position;
        moveDirection.y = 0;
        Vector3 step = moveDirection.normalized * moveSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.LookRotation(moveDirection);
        if (!rigidbody.velocity.Equals(Vector3.zero))
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0);
        }
        rigidbody.rotation = rotation;
        rigidbody.MovePosition(rigidbody.position + step);
    }

    /**
     * Timer for pausing the player.
     * */
    bool Timer()
    {
        time += Time.deltaTime;
        return time > Random.Range(0, 20);
    }


    /**
     * Makes the NPC walk with Astar.
     * */
    void WalkWithAStar(float moveSpeed, float rotateSpeed)
    {
        //Makes a pad for NPC if nodenetwork is made and it is indicated Astar needs to make a new path.
        if (useAstar && ResourceManager.networkReady)
        {
            nodes = GameObject.Find("NodeNetwork").GetComponent<CreateNodeNetwork>().nodes;
            startPos = transform.position;
            endPos = PickRandomEndPos(nodes.NetworkRange());
            path = MakePad(startPos, endPos);
            useAstar = false;
        }
        //Walks to next node.
        if (path.Count > 0 && index < path.Count && !stopWalking)
        {
            if (!Methods.ReachedPosWithBuffer(transform.position, path[index].transform.position, new Vector3(0.5f,9999999,0.5f)))
                NPCWalk(moveSpeed, rotateSpeed, path[index].transform.position);
        
            else
                index++;      
        }

        //Makes a new path id end reached.
        else if (index == path.Count && !stopWalking)
        {
            if (Timer())
            {
                time = 0;
                useAstar = true;
                this.index = 1;
                path.Clear();
                openList.Clear();
                closedList.Clear();
                nodes.ClearCosts(NPCNumber);
            }
        }

        if(gameObject.GetComponent<Astar>().stopWalking)
        {
            transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 0);
        }
        
    }

    /**
     * Generate a new target position within the NodeNetwork and with an accesable node. 
     * */
    Vector3 PickRandomEndPos(Vector4 range)
    {
        float xPos = Random.Range(range.x, range.y);
        float zPos = Random.Range(range.z, range.w);
        Vector3 pos = new Vector3(xPos, 0, zPos);
        GameObject endNode = nodes.Closest(pos);        

        int i = 0;
        while (!endNode.GetComponent<Node>().accesable && i < 1000)
        {
            xPos = Random.Range(nodes.NetworkRange().x, nodes.NetworkRange().y);
            zPos = Random.Range(nodes.NetworkRange().z, nodes.NetworkRange().w);
            pos = new Vector3(xPos, 0, zPos);
            endNode = nodes.Closest(pos);
            i++;
        }
       
        return pos;
    }

    //Actions

    void Start()
    {
        useAstar = true;
    }

    // Update is called once per frame
    void Update()
    {

        WalkWithAStar(moveSpeed, rotateSpeed);

    }

}
