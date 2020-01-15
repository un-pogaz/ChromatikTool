using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class MusicMetaData
    {
        public long Length { get; }
        public double LengthKo { get; }
        public double LengthMo { get; }

        public string MimeType { get; }

        public TimeSpan Duration { get; }

        public int Biterate { get; }
        public int Samplerate { get; }
        
        public int VideoBiterate { get; }
        public System.Drawing.Size VideoSize { get; } = System.Drawing.Size.Empty;

        public MusicMetaData(string path)
        {
            using (TagLib.File file = TagLib.File.Create(path))
            {
                TagLib.Id3v2.Tag tag2;
                tag2 = file.GetTag(TagLib.TagTypes.Id3v2) as TagLib.Id3v2.Tag;


                Length = new System.IO.FileInfo(path).Length;
                LengthKo = Length / 1024d;
                LengthMo = LengthMo / 1024d;

                Duration = file.Properties.Duration;
                if (file.Properties.VideoHeight > 0)
                    VideoSize = new System.Drawing.Size(file.Properties.VideoWidth, file.Properties.VideoHeight);
                
            }
            
        }
    }
}
