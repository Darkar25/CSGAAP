﻿using CSGAAP.Generics;
using System.Text;

namespace CSGAAP.Languages
{
    public class English : Language
    {
        public override string Name => "English";

        public override string Lang => "english";

        public override Encoding Charset => Encoding.Default;
    }
}
