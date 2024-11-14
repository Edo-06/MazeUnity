using UnityEngine;
using UnityEngine.UI;

public class MazeMap : MonoBehaviour
{
    public GameObject cell;
    public GameObject container;
    public Transform canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateUI(Transform gridParent)
    {
        Container1 container0 = container.GetComponent<Container1>();
        Debug.Log("ejecutao");
        Transform canvas0 = Instantiate(canvas);
        Transform grid = Instantiate(gridParent);
        grid.transform.SetParent(canvas0.transform,false);
        //grid.transform.localScale = new Vector3(0.02f,0.02f);
        /*GameObject cell0 = Instantiate(cell,new Vector3(40, 40, 0), Quaternion.identity);
        cell0.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
        cell0.transform.SetParent(grid, false);
        Image cellImage = cell0.GetComponent<Image>();
        cellImage.color = Color.black;*/
        for (int i = 0; i < container0.size; i++)
        {
            for (int j = 0; j < container0.size; j++)
            {
                GameObject cell0 = Instantiate(cell/*new Vector3(i, j, 0), Quaternion.identity*/);
                //cell0.transform.localScale = new Vector3(0.08f,0.08f,0.08f);
                cell0.transform.SetParent(grid, false);
                Image cellImage = cell0.GetComponent<Image>();

                if (container0.maze.mazee[i, j].category == Category.wall)
                {
                    cellImage.color = new Color(0f, 0f, 0f, 0.7f);
                }
                else
                {
                    cellImage.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        }
    }
}
