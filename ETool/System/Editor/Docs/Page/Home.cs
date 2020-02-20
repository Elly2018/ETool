#if UNITY_EDITOR
namespace ETool.Docs
{
    [DocsPath("Home")]
    public class Home : DocsBase
    {
        public override void Render()
        {
            Title(new string[] { "ETool documentation" , "ETool 文件指南"});
            Space();
            Paragraph(new string[] { "Welcome to ETool document", "歡迎來到 ETool 文件指南" });
            Paragraph(new string[] { "Here i want show user how to use ETool plugin", "在這裡我會解釋給用戶說如何使用插件" });
            Space();
            Paragraph(new string[] { "ETool is a plugin that help you to build game logic", "ETool 是一個工具能幫助你建立遊戲邏輯" });
            Paragraph(new string[] { "Easy, intuitive, clean", "簡單, 直覺, 乾淨" });
            Paragraph(new string[] { "User don't need to understand programming to start build game logic", "用戶不需要知道怎麼寫程式就能建立遊戲邏輯" });
            Paragraph(new string[] { "Node editor is intuitive to use", "節點作業使作業上非常直覺" });
            Paragraph(new string[] { "It's editor design is clean, nothing fancy", "視窗的設計十分乾淨, 沒有複雜的花樣" });
            Space();

            Space();
            Title2(new string[] { "Basic", "基礎" });
            Space();

            Link(new string[] { "Introduce", "基本介紹" }, "Home/Introduce", 300);
            Link(new string[] { "Node", "節點" }, "Home/Node", 300);
            Link(new string[] { "Connection", "連結" }, "Home/Connection", 300);
            Link(new string[] { "Example", "範例" }, "Home/Example", 300);

            Space();
            Title2(new string[] { "Advance", "進階" });
            Space();

            Link(new string[] { "Algorithm", "演算法" }, "Home/Algorithm", 300);
            Link(new string[] { "Particle Modify", "粒子變化" }, "Home/ParticleModify", 300);
            Link(new string[] { "Webcam", "攝影機" }, "Home/Webcam", 300);
            Link("VR/AR", "Home/VRAR", 300);
        }
    }
}
#endif