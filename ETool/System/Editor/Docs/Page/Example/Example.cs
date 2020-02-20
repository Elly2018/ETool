#if UNITY_EDITOR
namespace ETool.Docs
{
    [DocsPath("Home/Example")]
    public class Example : DocsBase
    {
        public override void Render()
        {
            Title("Example");
            Space();
        }
    }
}
#endif