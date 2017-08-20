A small and simple library to control your Yamaha MusicCast device 
using the Yamaha Extended Control API. 

Written in C# .NET standard. 
The code is tested on a Yamaha HTR-4068


1. Add a reference to the YamahaMusicCast.dll to your project
2. Use the IMusicCastClient interface to get started:

    var deviceIp = IPAddress.Parse("127.0.0.1");
    IMusicCastClient client = new MusicCastClient(deviceIp);

3. All the functions on the client should be self explanatory when using intellisense



This code is still under construction and it therefore only contains a small set of basic functions.
Missing something? Help me by collaborating. The Extended Control API reference is also present in the repository.