/*
 * swf-reader/SwfHeader.cs
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

using System;
using System.IO;

namespace SwfReader {
  /// <summary>
  /// Represents the first 8 bytes of an SWF file that describe the header
  /// </summary>
  public sealed class SwfHeader {
    private byte version;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:swfreader.SwfHeader"/> class.
    /// </summary>
    /// <param name="compression">The compression of the SWF file</param>
    /// <param name="version">The version of the SWF file</param>
    /// <param name="fileLength">The length of the uncompressed file</param>
    public SwfHeader(SwfCompression compression, byte version, uint fileLength) {
      this.Compression = compression;
      this.Version = version;
      this.FileLength = fileLength;
    }

    /// <summary>
    /// The compression used for the SWF file
    /// </summary>
    /// <value>Compression (FWS, CWS, LWS)</value>
    public SwfCompression Compression { get; private set; }

    public static readonly byte MAX_VERSION = 42; // TODO: update if necessary

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    /// <value>A byte between 0 and MAX_VERSION</value>
    public byte Version {
      get { return version; }

      private set {
        // check to ensure it is not out of range
        if (value > MAX_VERSION) {
          throw new ArgumentOutOfRangeException(String.Format("Cannot set SwfHeader version to above {0}", MAX_VERSION));
        }

        version = value;
      }
    }

    /// <summary>
    /// Gets the length of the file.
    /// </summary>
    /// <value>The length of the file.</value>
    public uint FileLength { get; private set; }

    public static SwfHeader FromStream(Stream source) {
      byte[] buffer = new byte[4];

      source.Read(buffer, 0, 3);

      // Magic bytes are {}, 0x57, 0x53
      if (buffer[1] != 0x57 || buffer[2] != 0x53) {
        throw new SwfReadException("File is not a valid SWF file");
      }

      // Check for compression
      SwfCompression compression;
      switch (buffer[0]) {
        case 0x46: // "F"
          compression = SwfCompression.Uncompressed;
          break;
        case 0x43: // "C"
          compression = SwfCompression.ZlibCompressed;
          break;
        case 0x5A: // "L"
          compression = SwfCompression.LzmaCompressed;
          break;
        default:
          throw new SwfReadException("Unable to recognize compression of SWF file");
      }

      // Check for version
      source.Read(buffer, 0, 1);
      byte version = buffer[0];

      // Check for size
      source.Read(buffer, 0, 4);
      uint fileLength = BitConverter.ToUInt32(buffer, 0);

      // Resolve for SwfHeader object
      SwfHeader header = new SwfHeader(compression, version, fileLength);
      Console.WriteLine("Resolved for header {0}", header);
      return header;
    }

    public override string ToString() {
      return String.Format("Compression Method {0}, Version {1}, File Length {2}", Compression, Version, FileLength);
    }
  }
}
