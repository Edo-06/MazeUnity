using UnityEngine;
using UnityEngine.UI;

public class MazeMap : MonoBehaviour
{
    public GameObject cell;
    public GameObject container;
    public Transform canvas;
    private Transform grid;
    public static MazeMap Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateUI(Transform gridParent)
    {
        Container1 container0 = container.GetComponent<Container1>();
        Transform canvas0 = Instantiate(canvas);
        grid = Instantiate(gridParent);
        grid.transform.SetParent(canvas0.transform,false);

        for (int i = 0; i < container0.size; i++)
        {
            for (int j = 0; j < container0.size; j++)
            {
                GameObject cell0 = Instantiate(cell);
                cell0.transform.SetParent(grid, false);
                Image cellImage = cell0.GetComponent<Image>();

                switch(Global.maze.mazee[i, j].category)
                {
                    case Category.wall: 
                        cellImage.color = new Color(0f, 0f, 0f, 0.7f);
                        break;
                    case Category.final:
                        cellImage.color = Color.yellow;
                        break;
                    default:
                        cellImage.color = new Color(1f, 1f, 1f, 0.5f);
                        break;
                }
            }
        }
    }

    public void Change(int index, Color color)
    {
        if(grid.transform.childCount > index)
        {
            Transform child = grid.transform.GetChild(index);
            child.GetComponent<Image>().color = color;
        }
    }
}
