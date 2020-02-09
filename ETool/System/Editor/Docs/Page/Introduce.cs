using System;
using UnityEditor;
using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Introduce")]
    public class Introduce : DocsBase
    {
        public override void Render()
        {
            Title("Introduce");
            Space();
            Paragraph("In this page");
            Paragraph("It will show you how this plugin works");
            Paragraph("And what can this plugin do");
            Paragraph("There are multiple different object i need to introduce first");
            Space(12);

            Title2("Blueprint");
            Image("Assets/ETool/Icon/icons8-document-64.png");
            Paragraph("Create this object you need to go");
            Paragraph("\"Assets=>Create=>ETool=>Blueprint\"");
            Space();
            Paragraph("What's the use of this object ? Blueprint ?", Color.blue);
            Paragraph("Now we click a blueprint, let's see what inside");
            Image("Assets/ETool/Icon/Docs/BPImage.png", 400);
            Space();
            Paragraph("There are five element in the blueprint menu");
            Paragraph("1.Blueprint Event", Color.blue);
            Paragraph("2.Custom Event", Color.blue);
            Paragraph("3.Variable Event", Color.blue);
            Paragraph("4.Inherit Blueprint", Color.blue);
            Paragraph("5.Total", Color.blue);
            Space(12);

            Title2("Blueprint Event");
            Paragraph("Here is what happen when you unfold the gui control");
            Image("Assets/ETool/Icon/Docs/BPEvent.png", 400);
            Paragraph("Here is the page that have more detail about event");
            Link("What's Event", "Home/Introduce/Event", 300);
        }
    }
}
