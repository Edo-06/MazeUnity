public enum Category
{
    wall,
    floor,
    obstacle,
    key,
    tramp,
    final,
}
public class Cell
{
    public Category category;
    public string type;
    public string modo;

    public Cell(Category category, string type = " ", string modo = " ") //mod = pocision de las puertas

    {
        this.modo = modo;
        this.category = category;
        this.type = type;
    }

    /*public void Print(int x, int z)
    {
        switch (category)
        {
            case Category.wall:
                GameObject wall = Instantiate(WallObject, new Vector3(x, 0, z), Quaternion.identity);
                wall.transform.localScale = new Vector3(1, 1, 1); // Ajusta el tamaño si es necesario
                break;
            case Category.path:
                Console.Write("  ");
                break;
            case Category.obstacle:
                Console.Write("🔐");
                break;
            case Category.key:
                Console.Write("🗝️");
                break;
            case Category.tramp:
                Console.Write("💀");
                break;
            
            
        }*/
}
