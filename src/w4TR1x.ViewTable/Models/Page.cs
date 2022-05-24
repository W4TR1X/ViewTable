namespace w4TR1x.ViewTable.Models;

[Serializable]
public class Page
{
    public string PageName { get; set; }

    public int OrderBy { get; set; }

    public bool DescendingOrder { get; set; }

    public Page(string pageName, int orderBy = -1, bool descendingOrder = false)
    {
        PageName = pageName;
        OrderBy = orderBy;
        DescendingOrder = descendingOrder;
    }
}