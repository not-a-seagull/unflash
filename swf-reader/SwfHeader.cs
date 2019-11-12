using System;

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
      get { return Version; }

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
  }
}
