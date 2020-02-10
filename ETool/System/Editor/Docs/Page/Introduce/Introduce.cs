using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Introduce")]
    public class Introduce : DocsBase
    {
        public override void Render()
        {
            Title(new string[] { "Introduce" , "基本介紹"});
            Space();
            Paragraph(new string[] { "In this page", "在這個文件中" });
            Paragraph(new string[] { "It will show you the basic plugin mechanism", "我會介紹插件的基礎功能" });
            Paragraph(new string[] { "And show what can this plugin do", "然後介紹插件可以怎麼用" });
            Space(12);

            Title2(new string[] { "Blueprint", "藍圖" });
            Space();
            Image("Assets/ETool/Icon/icons8-document-64.png");
            Paragraph(new string[] { "Create this object you need to go", "建立藍圖你必須前往" });
            Paragraph("\"Assets=>Create=>ETool=>Blueprint\"");
            Space();
            Paragraph(new string[] { "What's the use of Blueprint ?", "藍圖可以做什麼?" }, Color.blue);
            Paragraph(new string[] { "Now we click a blueprint, let's see what inside", "現在我們點擊藍圖, 讓我們看一眼他的 GUI 介面" });
            Image("Assets/ETool/Icon/Docs/BPImage.png", 400);
            Space();
            Paragraph(new string[] { "There are five element in the blueprint menu", "藍圖中有五個大項" });
            Paragraph(new string[] { "1.Blueprint Event", "1.藍圖事件 (Blueprint Event)" }, Color.blue);
            Paragraph(new string[] { "2.Custom Event", "2.自定義事件 (Custom Event)" }, Color.blue);
            Paragraph(new string[] { "3.Custom Variable", "3.藍圖變數 (Custom Variable)" }, Color.blue);
            Paragraph(new string[] { "4.Inherit Blueprint", "4.藍圖繼承 (Inherit)" }, Color.blue);
            Paragraph(new string[] { "5.Total", "5.加總資料 (Total)" }, Color.blue);
            Space(12);

            Title3(new string[] { "Blueprint Event" , "藍圖事件" });
            Space();
            Paragraph(new string[] { "Here is the page that have more detail about event", "這裡有更多關於藍圖事件的細節" });
            Link(new string[] { "What's Event", "甚麼是事件?" }, "Home/Introduce/Event", 300);
            Paragraph(new string[] { "Event is link to unity event execution order", "事件是根據於 Unity 的事件所做的" });
            Paragraph(new string[] { "In order to understand when or how to add the event", "為了要理解甚麼時機加入事件" });
            Paragraph(new string[] { "User must understand first, how is unity event works", "我們必須先了解 Unity 的事件機制" });
            Space(12);

            Title3(new string[] { "Custom Event", "自定義事件" });
            Space();
            Paragraph(new string[] { "Although you can use unity event", "雖然我們可以使用 Unity 的事件" });
            Paragraph(new string[] { "But sometime you want to create your own event", "但是有時候我們想建立自己的事件" });
            Paragraph(new string[] { "Or your method to solve some of the problem", "或者可以說你的 \"方法\" 來解決問題" });
            Space();
            Paragraph(new string[] { "Here is the page that have more detail about custom event", "這裡有更多關於自定義事件的細節" });
            Link(new string[] { "Custom Event ?", "自定義事件 ?" }, "Home/Introduce/CustomEvent", 300);
            Space(12);

            Title3(new string[] { "Blueprint Variable", "藍圖變數" });
            Space();
            Paragraph(new string[] { "User can create variable for blueprint", "用戶能為了藍圖建立自己需要的變數" });
            Paragraph(new string[] { "The below link will take you to the document page", "下方的連結會帶你到變數的文件頁面" });
            Link(new string[] { "Custom Variable !", "自定義變數 !" }, "Home/Introduce/CustomVariable", 300);
            Space(12);

            Title3(new string[] { "Inherit Blueprint", "藍圖繼承" });
            Space();
            Paragraph(new string[] { "Blueprint can inherit another blueprint", "藍圖能繼承另一個藍圖" });
            Paragraph(new string[] { "Then child blueprint can access to all vairalbes and event within parent blueprint", "子藍圖能存取父藍圖的變數與事件" });
            Paragraph(new string[] { "It's a way to avoid user create repeat blueprint", "繼承是一種避免重複作業的方式" });
            Space(12);

            Title3(new string[] { "Total", "加總資料" });
            Space();
            Paragraph(new string[] { "It will calculate how many nodes and connection current select blueprint have", "它會計算當前藍圖總共有多少節點與連結" });
            Space(12);

            Title2(new string[] { "GameData", "遊戲資料庫" });
            Space();
            Image("Assets/ETool/Icon/icons8-database-64.png");
            Paragraph(new string[] { "Create this object you need to go", "建立資料庫你必須前往" });
            Paragraph("\"Assets=>Create=>ETool=>GameData\"");
            Space();
            Paragraph(new string[] { "Gamedata kinda like database, responsible for store data", "遊戲資料庫就像資料庫一樣, 負責儲存資料使用" });
            Paragraph(new string[] { "It contain import, export, encrypt, compress feature ", "它包含了匯入, 匯出, 加密, 壓縮的功能" });
            Paragraph(new string[] { "And it store in global environemnt, it's easier to access compare to blueprint", "而遊戲資料庫儲存在全域的環境中, 所以比藍圖更容易的存取" });
        }
    }
}
