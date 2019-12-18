using RndStrGen.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RndStrGen.Services {
    public class GeneratorService : IGeneratorService {
        public string GetGuid(int length) {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Empty);

            for (int i = 0; i < length; i++) {
                sb.Append(Guid.NewGuid());
                if (i < length - 1) {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public string GetString(int length, bool numbers, bool letters, bool symbols) {
            throw new NotImplementedException();
        }
    }
}
