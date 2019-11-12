/*
 * unit-tests/SwfHeaderTests.cs
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

using NUnit.Framework;
using SwfReader;
using System;
using System.IO;

namespace UnitTests
{
  [TestFixture]
  public class SwfHeaderTests
  {
    public void RunHeaderTest(byte[] source, SwfCompression expectedCompression, byte expectedVersion, int expectedLength) {
      // use a memory stream for testing
      using (MemoryStream stream = new MemoryStream(source))
      {
        SwfHeader header = SwfHeader.FromStream(stream);

        Assert.AreEqual(expectedCompression, header.Compression);
        Assert.AreEqual(expectedVersion, header.Version);
        Assert.AreEqual(expectedLength, header.FileLength);
      }
    }

    [Test]
    public void ReadTest() {
      byte[] source1 = { 0x46, 0x57, 0x53, 0x09, 0x0A, 0x00, 0x00, 0x00 };
      RunHeaderTest(source1, SwfCompression.Uncompressed, 9, 10);

      byte[] source2 = { 0x43, 0x57, 0x53, 0x12, 0x10, 0x00, 0x00, 0x00 };
      RunHeaderTest(source2, SwfCompression.ZlibCompressed, 18, 16);

      byte[] source3 = { 0x5A, 0x57, 0x53, 0x21, 0x0B, 0x00, 0x00, 0x00 };
      RunHeaderTest(source3, SwfCompression.LzmaCompressed, 33, 11);
    }
  }
}
