﻿/*
 * Program.cs
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

using Mono.Options;
using SwfReader;
using System;
using System.Collections.Generic;
using System.IO;

namespace Unflash {
  class MainClass {
    public static int Main(string[] args) {
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
        Console.Error.Write("unflash: ");
        Console.Error.WriteLine(e.Message);
        return 1;
      }

      // extras must have at least one instance: the file uri
      if (extras.Count == 0) {
        Console.Error.WriteLine("unflash: Please pass in the location of the SWF file as the first argument");
        return 1;
      }

      // check to see if the file uri exists
      string fileUri = extras[0];
      if (!File.Exists(fileUri)) {
        Console.Error.WriteLine(String.Format("unflash: Unable to find file {0}", fileUri));
        return 1;
      }

      Console.WriteLine("Beginning file read...");

      using (FileStream f = File.Open(fileUri, FileMode.Open))
        Console.WriteLine(SwfHeader.FromStream(f));

      return 0;
    }
  }
}
