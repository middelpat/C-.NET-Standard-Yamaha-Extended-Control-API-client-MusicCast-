using System;
using System.Collections.Generic;
using System.Text;

namespace YamahaMusicCast
{
    public class Constants
    {
        // this restricts the volume to a maximum of 0db (which is really loud).   
        public static readonly int MAX_VOLUME_STEP = 160;

        public static readonly string ZONE = "main";

        public static readonly string HEADER_APP_NAME = "MusicCast/1.0";
        public static readonly string HEADER_APP_PORT = "41100";
    }
}
