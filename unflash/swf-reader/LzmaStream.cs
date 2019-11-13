/*
 * swf-reader/LzmaStream.cs
 * unflash - Program to convert SWF objects to Canvas-based HTML programs 
 * 
 * Copyright 2019 not_a_seagull
 * 
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that 
 * the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the 
 * following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the 
 * following disclaimer in the documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or 
 * promote products derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED 
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
 * PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY 
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, 
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
 * DAMAGE.
 */

using SevenZip.Compression.LZMA;
using System;
using System.IO;

namespace SwfReader {
  public class LzmaStream : Stream {
    private Stream stream;
    private int length;
    private Decoder decoder;

    public LzmaStream(Stream wrappedStream, int expectedLength)
    {
      stream = wrappedStream;
      length = expectedLength;

      // setup 7zip
      byte[] buffer = new byte[5];
      stream.Read(buffer, 0, 5);
      decoder.SetDecoderProperties(buffer);
    }

    public override bool CanRead => throw new NotImplementedException();

    public override bool CanSeek => throw new NotImplementedException();

    public override bool CanWrite => throw new NotImplementedException();

    public override long Length => throw new NotImplementedException();

    public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void Flush()
    {
      throw new NotImplementedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      throw new NotImplementedException();
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotImplementedException();
    }

    public override void SetLength(long value)
    {
      throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      throw new NotImplementedException("Writing is currently not supported by LzmaStream");
    }
  }
}
