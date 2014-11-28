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

    //Walking
    public float moveSpeed;
    public float rotateSpeed;

    private int index = 1;
    
    void Start()
    {
        useAstar = true;
        startPos = transform.position;
    }
    /**
     * Set the parameters for algorithm and excute it.
     * */
    List<GameObject> MakePad(Vector3 startPos, Vector3 endPos)
    {
        nodes = GameObject.Find("NodeNetwork").GetComponent<CreateNodeNetwork>().nodes;
        this.endNode = nodes.Closest(endPos);
        this.startNode = nodes.Closest(startPos);
        ReachableNodes(startNode, null);
        GameObject previousNode = startNode;
        GameObject CurrentNode;
        while (!ReachedEnd(closedList))
        {
            CurrentNode = LowestCost(openList);
            ReachableNodes(CurrentNode,previousNode);            
        }
        return ConstructPath(closedList);
    }

    /**
     * Checks whether the near by nodes are accesable and removes previous.
     * */
    void ReachableNodes(GameObject parentNode, GameObject previousNode)
    {
        closedList.Add(parentNode);
        openList.Remove(parentNode);
        Vector2 nodePos = NodeNetwork.Node2Vector(parentNode);
        for (int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if (nodes.IsNodeInNetwork((int)nodePos.x + i, (int)nodePos.y + j))
                {
                    GameObject CheckNode = nodes.getNode((int)nodePos.x + i, (int)nodePos.y + j);
                    if (!closedList.Contains(CheckNode) && !openList.Contains(CheckNode) && CheckNode.GetComponent<Node>().accesable)
                    {
                        CheckNode.GetComponent<Node>().parentNode[NPCNumber] = parentNode;
                        CheckNode.GetComponent<Node>().G[NPCNumber] = G(parentNode, CheckNode);
                        CheckNode.GetComponent<Node>().H[NPCNumber] = H(CheckNode);
                        CheckNode.GetComponent<Node>().F[NPCNumber] = G(parentNode, CheckNode) + H(CheckNode);
                        openList.Add(CheckNode);

                    }
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
     * Estimated movement cost
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
     * Constructs path out of the closed list.
     * */
    List<GameObject> ConstructPath(List<GameObject> closedList)
    {
        List<GameObject> path = new List<GameObject>();
        path.Add(endNode);
        GameObject parentNode = endNode.GetComponent<Node>().parentNode[NPCNumber];
        path.Add(parentNode);
        int i = 2;
        while(!parentNode.Equals(startNode))
        {
            path.Add(parentNode.GetComponent<Node>().parentNode[NPCNumber]);
            parentNode = path[i];
            i++;
        }
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
        transform.rotation = rotation;
        rigidbody.MovePosition(rigidbody.position + step);
    }


    //Use Astar to find a path.
    void WalkWithAStar(float moveSpeed, float rotateSpeed)
    {
        if (useAstar)
        {
            path = MakePad(startPos, endPos);
            useAstar = false;
        }

        if (path.Count > 0 && index < path.Count)
        {
            if (!Methods.ReachedPosWithBuffer(transform.position, path[index].transform.position, new Vector3(0.5f,9999999,0.5f)))
                NPCWalk(moveSpeed, rotateSpeed, path[index].transform.position);
        
            else
                index++;      
        }
        else if (index == path.Count)
        {
            useAstar = true;
            this.index = 1;
            path.Clear();
            openList.Clear();
            closedList.Clear();
            nodes.ClearCosts(NPCNumber);
            startPos = transform.position;
            endPos = PickRandomEndPos();
        }
        
    }

    Vector2 GridSize(Vector3 startPos, Vector3 endPos)
    {
        Vector3 distance = endPos - startPos;
        float x = Mathf.Round( Mathf.Abs(distance.x)*2 + 10);
        float y = Mathf.Round(Mathf.Abs(distance.z)*2 + 10);
        float size = Mathf.Max(x, y);
        return new Vector2(size, size);

    }

    Vector3 PickRandomEndPos()
    {
        float xPos = Random.Range(nodes.NetworkRange().x, nodes.NetworkRange().y);
        float zPos = Random.Range(nodes.NetworkRange().z, nodes.NetworkRange().w);
        Vector3 pos = new Vector3(xPos, 0, zPos);
        GameObject endNode = nodes.Closest(pos);
    

        while (!endNode.GetComponent<Node>().accesable)
        {
            xPos = Random.Range(nodes.NetworkRange().x, nodes.NetworkRange().y);
            zPos = Random.Range(nodes.NetworkRange().z, nodes.NetworkRange().w);
            pos = new Vector3(xPos, 0, zPos);
            endNode = nodes.Closest(pos);
        }
        return pos;
    }

    // Update is called once per frame
    void Update()
    {

        WalkWithAStar(moveSpeed, rotateSpeed);

    }

}
