public enum Category
{
    wall,
    floor,
    obstacle,
    key,
    trap,
    final,
}

public class Cell
{
    public Category category;
    public string type;
    public string modo;

    public Cell(Category category, string type = " ", string modo = " ")

    {
        this.modo = modo;
        this.category = category;
        this.type = type;
    }
}
