using Mono.Options;
using SwfReader;
using System;
using System.Collections.Generic;

namespace Unflash {
  class MainClass {
    public static void Main(string[] args) {
      bool preserveComments = false;

      // parse options passed in from command line
      OptionSet options = new OptionSet() {
        { "c|preserve-comments", "Try to preserve comments from ActionScript code in generated JavaScript code",
          v => preserveComments = v != null }
      };

      List<string> extras;
      try {
        extras = options.Parse(args);
      } catch (OptionException e) {
        Console.Write("unflash: ");
        Console.WriteLine(e.Message);
        return;
      }

      if (extras.Count == 0) {
        Console.WriteLine("unflash: Please pass in the location of the SWF file as the first argument");
        return;
      }
    }
  }
}
