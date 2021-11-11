using System.Collections.Generic;

namespace ExpressionTree
{
    public class Data
    {
        private static int ID = 1;
        private static readonly SortedDictionary<int, Data> data_base = new SortedDictionary<int, Data>();

        protected bool Sign { get; set; }
        protected int _id { get; set; }
        public string Value { get; set; }


        public Data(bool sign, string value = null)
        {
            _id = ID++;
            Sign = sign;
            Value = value;
        }

        public Data(string value)
        {
            Sign = false;
            _id = ID++;
            Value = value;
        }

        public override string ToString()
        {
            return (Sign) ? $"-{Value}" : $"{Value}";
        }
    }
}
