#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace ETool.Docs
{
    [DocsPath("Home/Introduce/CustomVariable")]
    public class Introduce_Variable : DocsBase
    {
        public override void Render()
        {
            Title("Custom Variable");
            Space();
            Paragraph("Here is a image of custom variable editor screenshot");
            Image("Assets/ETool/Icon/Docs/BP_Variable.png", 200);
            Paragraph("This is when what happen ");
            Paragraph("When you click \"Add Custom Variable\"");
            Paragraph("As you can see above image");
            Paragraph("There are 5 field we can modify");
            Space(12);

            Paragraph("Container", Color.blue);
            Paragraph("The first bar that have two option");
            Paragraph("Define what type of container this variable is");
            Paragraph("Container will effect how blueprint store variable data");
            Paragraph("Object");
            Paragraph("Store varaible as single object");
            Paragraph("Array");
            Paragraph("Store variable as multiple object with same type");
            Image("Assets/ETool/Icon/Docs/BP_Array.jpg", 350);
            Paragraph("Such as above image, an array have indices and size");
            Paragraph("We input a index and get the value in specific position in array");
            Space();

            Paragraph("Access", Color.blue);
            Paragraph("Public");
            Paragraph("Every object once access this blueprint can access this vairalbe");
            Paragraph("Protected");
            Paragraph("Only inherit blueprint can access this vairable");
            Paragraph("Private");
            Paragraph("Only this blueprint itself can access this variable");
            Space();

            Paragraph("Label", Color.blue);
            Paragraph("Define what variable name it is");
            Space();

            Paragraph("Type", Color.blue);
            Paragraph("Define what vairalbe type it is");
            Space();

            Paragraph("Default Value / Size", Color.blue);
            Paragraph("If container type is object");
            Paragraph("The field will represent default value");
            Paragraph("If container type is array");
            Paragraph("The field will represent array size");
            Space();
        }
    }
}
#endif