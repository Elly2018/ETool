using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Introduce/CustomEvent")]
    public class Introduce_CustomEvent : DocsBase
    {
        public override void Render()
        {
            Title(new string[] { "Custom Event", "自定義事件" });
            Space();
            Paragraph(new string[] { "Here is a image of custom event editor screenshot", "以下是自定義事件區域的截圖" });
            Image("Assets/ETool/Icon/Docs/BPCustomEvent.png", 200);
            Paragraph(new string[] { "There are 3 field, 5 button we notice", "我們看到這裡有 3 個項目 5 個按鈕" });
            Paragraph(new string[] { "This is what happen when we click \"Add Custom Event\"", "這是我們會看到的當我們按下 \"Add Custom Event\" 的按鈕" });
            Paragraph(new string[] { "A new custom event struct will popup", "新的事件結構會出現" });
            Space(12);

            Title2(new string[] { "Custom Event Button", "自定義事件按鈕" });
            Space(12);

            Title2(new string[] { "Custom Event Header", "自定義事件標頭" });
            Paragraph("Event Name", Color.blue);
            Paragraph(new string[]{ "The event title", "事件的名稱"});
            Space();
            Paragraph("Access Ability", Color.blue);
            Paragraph(new string[] { "Define the event access level", "定義事件的存取等級" });
            Space();
            Paragraph("Public", Color.magenta);
            Paragraph(new string[] { "All object is accessible", "所有的物件都可以存取" });
            Paragraph("Protected", Color.magenta);
            Paragraph(new string[] { "Only subclass blueprint can use this event", "只有子藍圖可以存取這個事件" });
            Paragraph("Private", Color.magenta);
            Paragraph(new string[] { "Only this blueprint itself can use this event", "只有當前藍圖能存取這個事件" });
            Space();
            Paragraph("Return Type", Color.blue);
            Paragraph(new string[] { "User can leave it \"Event\"", "用戶能設定成 \"Event\"" });
            Paragraph(new string[] { "It mean it doesn't have any return type", "表示這個事件不包含任何的回傳值" });
            Paragraph(new string[] { "Return type is when user want after event finish processing", "回傳值是當事件結束執行後" });
            Paragraph(new string[] { "It can send a data back to event itself", "它會傳回特定資料回到事件本身" });
            Paragraph(new string[] { "Kinda like make a phone call with someone asking for timing", "有點像是打電話詢問當前時間" });
            Paragraph(new string[] { "You tell it where do you live", "你告訴它你人住哪裡" });
            Paragraph(new string[] { "Then you want after this phone call it can give you back some specific information", "然後你希望透過這通電話後它會給你一些資訊" });
            Paragraph(new string[] { "Such as for this example, current time at where i live", "在這個舉例, 根據你的時區的標準時間" });
            Space(12);

            Title2(new string[] { "Custom Event Arugment", "自定義事件的引數" });
            Paragraph(new string[] { "Here is a image of custom event arugment editor screenshot", "這是事件引數的截圖" });
            Image("Assets/ETool/Icon/Docs/BP_CustomEvent_Ag.png", 100);
            Paragraph(new string[] { "This is what happen when you click \"Add Arugment\"", "這是當你按下 \"Add Arugment\" 出現的樣子" });
            Paragraph(new string[] { "The arugment has 3 fields", "引數介面有三個輸入值" });
            Space();

            Paragraph("Label", Color.blue);
            Paragraph(new string[] { "Arugment name", "引數名稱" });
            Space();

            Paragraph("Type", Color.blue);
            Paragraph(new string[] { "Arugment data type", "引數資料型態" });
            Space();

            Paragraph("Container", Color.blue);
            Paragraph(new string[] { "Arugment data container type", "引數封裝型態" });
            Space();
            Space(12);
        }
    }
}
