using System;

namespace SwfReader {
  /// <summary>
  /// Describes whether an SWF file is uncompressed ("FWS"), or is compressed under the Zlib algorithm ("CWS"), 
  /// or is compressed under the LZMA algorithm ("LWS")
  /// </summary>
  public enum SwfCompression {
    /// <summary>
    /// Represents an SWF file being uncompressed
    /// </summary>
    Uncompressed = 0,

    /// <summary>
    /// Represents an SWF file being compressed via Zlib
    /// </summary>
    ZlibCompressed = 1,

    /// <summary>
    /// Represents an SWF file being compressed via LZMA
    /// </summary>
    LzmaCompressed = 2
  }
}
