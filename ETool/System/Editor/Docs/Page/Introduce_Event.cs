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
            Title("Unity Event");
            Space();
            Paragraph("Blueprint event use unity execute pipeline");
            Paragraph("Here is the execute order introduce made by unity");
            WebLink("Unity document", "https://docs.unity3d.com/Manual/ExecutionOrder.html", 300);
            Space();

            Paragraph("But this plugin only implement some necessary function");
            Image("Assets/ETool/Icon/Docs/BPEvent.png", 400);
            Paragraph("As you could be above image");
            Paragraph("Compare to amounts of unity engine events");
            Paragraph("This plugin only select some of the events that often use");
            Space(12);

            Title2("Main Event");
            Paragraph("Start", Color.blue);
            Paragraph("Start is called when game is start");
            Space();

            Paragraph("Update", Color.blue);
            Paragraph("Update is called every frame");
            Space();

            Paragraph("FixedUpdate", Color.blue);
            Paragraph("FixedUpdate has the frequency of the physics system");
            Paragraph("It is called every fixed frame-rate frame");
            Space();

            Paragraph("Destroy", Color.blue);
            Paragraph("Destroy is called when object which being killed");
            Space(12);

            Title2("Physics Event");
            Paragraph("OnCollision", Color.blue);
            Paragraph("OnTrigger", Color.blue);
        }
    }
}
