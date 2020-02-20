#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Introduce/Event")]
    public class Introduce_Event : DocsBase
    {
        public override void Render()
        {
            Title(new string[] { "Blueprint Event", "藍圖事件" });
            Space();
            Paragraph(new string[] { "Blueprint event use unity execute pipeline", "藍圖事件根據於 Unity 的事件機制" });
            Paragraph(new string[] { "Here is the execute order introduce made by unity", "以下連結是 Unity 官方所提供的事件機制的介紹" });
            WebLink("Unity event document", "https://docs.unity3d.com/Manual/ExecutionOrder.html", 300);
            Space();

            Paragraph(new string[] { "But this plugin only implement some necessary function", "但是這個插件只實現了幾個必要的事件" });
            Image("Assets/ETool/Icon/Docs/BPEvent.png", 400);
            Paragraph(new string[] { "As you could see above image", "你可以看到上方的圖片" });
            Paragraph(new string[] { "Compare to amounts of unity engine events", "去與 Unity 的事件機制做比較" });
            Paragraph(new string[] { "This plugin only select some of the events that often use", "我們只取出了必要使用的事件" });
            Space(12);

            Title2(new string[] { "Main Event", "主要事件" });
            Paragraph("Start", Color.blue);
            Paragraph(new string[] { "Start is called when game is start", "Start 會被呼叫當遊戲開始的時候" });
            Space();

            Paragraph("Update", Color.blue);
            Paragraph(new string[] { "Update is called every frame", "Update 會被呼叫每一幀" });
            Space();

            Paragraph("FixedUpdate", Color.blue);
            Paragraph(new string[] { "FixedUpdate has the frequency of the physics system", "FixedUpdate 會根據物理環境的頻率" });
            Paragraph(new string[] { "It is called every fixed frame-rate frame", "它會根據固定的時間差進行呼叫" });
            Space();

            Paragraph("Destroy", Color.blue);
            Paragraph(new string[] { "Destroy is called when object which being killed", "Destroy 會被呼叫當物件被刪除了時候" });
            Space(12);

            Title2("Physics Event");
            Paragraph("OnCollision", Color.blue);
            Paragraph(new string[] { "it is called when this collider/rigidbody has interact another rigidbody/collider", "當這個碰撞器/剛體與其他碰撞器/剛體接觸時會呼叫" });
            Space();
            Paragraph("OnTrigger", Color.blue);
            Paragraph(new string[] { "it happens on the FixedUpdate function when two GameObjects collide", "這個事件會在 FixedUpdate 下兩個物件碰撞時發生" });
            Paragraph(new string[] { "Both GameObjects must contain a Collider component. One must have Collider.isTrigger enabled", "兩個物件必須都要擁有碰撞器, 一個必須要有 isTrigger 勾選" });
            Paragraph(new string[] { "If both GameObjects have Collider.isTrigger enabled, no collision happens", "如果兩個物件的碰撞器 isTrigger 都勾選, 不會有事件發生" });
            Paragraph(new string[] { "The same applies when both GameObjects do not have a Rigidbody component", "相同的如果兩個物件都沒有剛體, 也不會有事件發生" });
            Space(6);
            Paragraph("Enter", Color.blue);
            Paragraph(new string[] { "When both objects begin the touch", "當兩個物件開始接觸的時候" });
            Space();

            Paragraph("Exit", Color.blue);
            Paragraph(new string[] { "When both objects end touch", "當兩個物件結束接觸的時候" });
            Space();

            Paragraph("Stay", Color.blue);
            Paragraph(new string[] { "When both objects are touching", "當兩個物件正在接觸的時候" });
            Space();
        }
    }
}
#endif