﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolsApi.Interfaces {
    public interface IGeneratorService {
        public string GetGuid(int length);
        public string GetString(int length, bool useNumbers, bool useLowercase, bool useUppercase, bool useSymbols);
        public string GetSentence(int length, string separator, bool randomCasing);
    }
}
