using System;
using UnityEditor;
using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home")]
    public class Home : DocsBase
    {
        public override void Render()
        {
            Title("ETool documentation");
            Space();
            Paragraph("Welcome to ETool document");
            Paragraph("Here i want show how to use ETool plugin");
            Space();
            Title2("Basic");
            Space();
            Link("Introduce", "Home/Introduce", 300);
            Link("Node", "Home/Node", 300);
            Link("Connection", "Home/Connection", 300);
        }
    }
}
