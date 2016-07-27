using System;

namespace SubsonicSharp
{
    public interface IStreamingAudio : IDisposable
    {
        string StreamLocation { get; set; }
        int MaxBitRate { get; set; }
        //format
        int EstimatedContentLength { get; set; }

        long Position { get; set; }
        float Volume { get; set; }

        void Play();
        void Pause();
        void Stop();
        void Seek(long offset);
    }
}
