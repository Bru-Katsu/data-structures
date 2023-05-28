using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tests.TestModels
{
    internal class CountryModel
    {
        public CountryModel(string key, int value)
        {
            Name = key;
            Rank = value;
        }

        public string Name { get; }
        public int Rank { get; }
    }
}
