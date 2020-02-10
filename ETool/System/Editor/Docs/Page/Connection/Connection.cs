using System;

namespace ETool.Docs
{
    [DocsPath("Home/Connection")]
    public class Connection : DocsBase
    {
        public override void Render()
        {
            Title("Connection");
            Space();
            Paragraph("Connection is define the relationshop between two nodes");
            Paragraph("When connection is create");
            Paragraph("After execute it will follow the pipeline to form as called: \"Chain reaction\"");
            Paragraph("User might spend most of time on building connection");
            Paragraph("Enough of talking, let's introduce how connection works");
            Space(12);

            Title2("Event Connection");
            Title2("Data Connection");
        }
    }
}
