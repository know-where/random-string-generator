﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ToolsApi.Interfaces;

namespace ToolsApi.Services {

    public class GeneratorService : IGeneratorService {

        public string GetGuid(int length) {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Empty);

            for (int i = 0; i < length; i++) {
                sb.Append(Guid.NewGuid());
                if (i < length - 1) {
                    sb.Append('-');
                }
            }

            return sb.ToString();
        }

        public string GetString(int length, bool useNumbers, bool useLowercase, bool useUppercase, bool useSymbols) {
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string symbols = "'!£$%^&*(? _,.";
            const string numbers = "0123456789";

            string chars = string.Empty;

            if (useNumbers) {
                chars += numbers;
            }
            if (useSymbols) {
                chars += symbols;
            }
            if (useLowercase) {
                chars += lowercase;
            }
            if (useUppercase) {
                chars += uppercase;
            }

            return new string(Enumerable.Repeat(chars, length).Select(x => x[RandomNumberGenerator.GetInt32(x.Length)]).ToArray());
        }

        public string GetSentence(int length, string separator, bool randomCasing) {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Empty);

            List<string> words = JsonConvert.DeserializeObject<List<string>>(System.IO.File.ReadAllText("Assets/words_dictionary.json"));

            for (int i = 0; i < length; i++) {
                bool uppercase = false;
                if (randomCasing) {
                    uppercase = RandomNumberGenerator.GetInt32(int.MaxValue) > (int.MaxValue / 2);
                }
                sb.Append(uppercase ? words[RandomNumberGenerator.GetInt32(words.Count)].ToUpper() : words[RandomNumberGenerator.GetInt32(words.Count)]);
                if (i < length - 1) {
                    sb.Append(separator);
                }
            }

            return sb.ToString();
        }
    }
}