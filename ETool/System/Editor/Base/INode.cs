namespace ETool.ANode
{
    public interface INodeStyle
    {
        StyleType GetNodeStyle();
        StyleType GetNodeSelectStyle();
        StyleType GetInPointStyle();
        StyleType GetOutPointStyle();
        StyleType GetInPointArrayStyle();
        StyleType GetOutPointArrayStyle();
    }
}
