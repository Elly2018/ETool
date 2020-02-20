#if UNITY_EDITOR
using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Node")]
    public class Node : DocsBase
    {
        public override void Render()
        {
            Title(new string[] { "Node", "節點" });
            Space();
            Title2(new string[] { "Overview", "大綱" });
            Space();
            Paragraph(new string[] { "Each node have their own method to solve problem", "每個節點都有自己解決問題的方式" });
            Paragraph(new string[] { "You can think as screwdriver", "你可以想像成螺絲起子" });
            Paragraph(new string[] { "There are different size of screwdriver", "這裡有許多種類的螺絲起子" });
            Paragraph(new string[] { "Each screwdriver design to solve some problem", "每把螺絲起子都有對特定的螺絲" });
            Space();
            Paragraph(new string[] { "Here is how i'm sort nodes", "這是我如何整理節點的方式" });
            Paragraph(new string[] { "I define what marjor category the node is", "我定義節點的大類是甚麼" });
            Paragraph(new string[] { "Then i analysis the type of node", "然後我分析這個節點的類型" });
            Paragraph(new string[] { "Here are some type that i define", "以下使我定義節點的類型" });
            Paragraph(new string[] { "Set", "賦予 (Set)" }, Color.blue);
            Paragraph(new string[] { "Get", "存取 (Get)" }, Color.blue);
            Paragraph(new string[] { "Method", "方法 (Method)" }, Color.blue);
            Paragraph(new string[] { "Constant", "常數 (Constant)" }, Color.blue);
            Space();
            Paragraph(new string[] { "Let's get start", "讓我們開始吧" });
            Space(12);

            Title2(new string[] { "Below is node list according to category", "下列為節點依照分類排序" });
            Space();
            Paragraph(new string[] { "Detail document page won't introduce every nodes", "詳細頁面並不會解釋所有的節點" });
            Paragraph(new string[] { "Because there are too many nodes", "因為實在是太龐大的量了" });
            Paragraph(new string[] { "So document only point out the key feature and how to use", "所有文件只點出功能與使用方式" });
            Space();

            Title3(new string[] { "Animator", "Animator 動畫" });
            Link(new string[] { "Animator", "Animator 動畫" }, "Home/Node/Animator", 200);
            Space(6);

            Title3(new string[] { "Array", "Array 陣列" });
            Link(new string[] { "Array", "Array 陣列" }, "Home/Node/Array", 200);
            Space(6);

            Title3(new string[] { "Audio", "Audio 音效" });
            Space(6);

            Title3(new string[] { "Casting", "Casting 轉型" });
            Space(6);

            Title3(new string[] { "Cursor", "Cursor 滑鼠" });
            Space(6);

            Title3(new string[] { "GameData", "GameData 遊戲資料庫" });
            Space(6);

            Title3(new string[] { "GameObject", "GameObject 場景物件" });
            Space(6);

            Title3(new string[] { "Input", "Input 輸入" });
            Space(6);

            Title3(new string[] { "IO", "IO 資料匯出匯入" });
            Space(6);

            Title3(new string[] { "Logic", "Logic 邏輯" });
            Space(6);

            Title3(new string[] { "Math", "Math 數學" });
            Space(6);

            Title3(new string[] { "Physics", "Physics 物理引擎" });
            Space(6);

            Title3(new string[] { "Render", "Render 渲染" });
            Space(6);

            Title3(new string[] { "String", "String 字串" });
            Space(6);

            Title3(new string[] { "Utility", "Utility 額外功能" });
            Space(6);
        }
    }
}
#endif