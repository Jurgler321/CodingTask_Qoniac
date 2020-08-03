using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Service
{
    public class CurrencyConversion
    {
        private class TripletBlock
        {
            public int Index { get; private set; }
            public int Value { get; private set; }
            public TripletBlock(int index, int value)
            {
                Index = index;
                Value = value;
            }
        }


        private readonly Dictionary<int, string> singleDigitNames = new Dictionary<int, string>
        {
            { 0, "zero" },
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
        };
        private readonly Dictionary<int, string> doubleDigitNames = new Dictionary<int, string>
        {
            { 1, "ten" },
            { 2, "twenty" },
            { 3, "thirty" },
            { 4, "forty" },
            { 5, "fifty" },
            { 6, "sixty" },
            { 7, "seventy" },
            { 8, "eighty" },
            { 9, "ninety" }
        };
        private readonly Dictionary<int, string> irregulars = new Dictionary<int, string>
        {
            { 10, "ten" },
            { 11, "eleven" },
            { 12, "twelve" },
            { 13, "theirteen" },
            { 14, "forteen" },
            { 15, "fifteen" },
            { 16, "sixteen" },
            { 17, "seventeen" },
            { 18, "eighteen" },
            { 19, "nineteen" }
        };
        private readonly Dictionary<int, string> powersOfTen = new Dictionary<int, string>
        {
            {2, "thousand" },
            {3, "million" },
            {4, "billion" },
            {5, "trillion" }
        };
        
        
        public CurrencyConversion()
        {

        }

        public string Convert(decimal input)
        {
            int cents = GetCents(input);
            long dollars = (long)input;
            List<TripletBlock> hundredblocks = new List<TripletBlock>();
            int blockindex = 1;
            while (TrySplitAtPowerOf10(dollars, 3, out long left, out int right))
            {
                hundredblocks.Add(new TripletBlock(blockindex, right));
                dollars = left;
                blockindex++;
            }
            hundredblocks.Add(new TripletBlock(blockindex, (int)dollars));
            TripletBlock centsBlock = new TripletBlock(0, cents);
            return CombineBlocks(hundredblocks, centsBlock, dollars != 1, cents > 0, cents != 1);
        }

        private string CombineBlocks(List<TripletBlock> blocks, TripletBlock cents, bool usePluralForDollars, bool displayCents, bool usePluralForCents) 
        {
            StringBuilder builder = new StringBuilder();
            var blocksOrdered = blocks.OrderBy(x => -x.Index);
            List<string> stringvalues = new List<string>();
            foreach (var block in blocksOrdered)
            {
                stringvalues.AddRange(BlockToStrings(block.Value, includeZero: blocks.Count == 1));
                if(powersOfTen.ContainsKey(block.Index))
                    stringvalues.Add(powersOfTen[block.Index]);
            }

            if (usePluralForDollars)
                stringvalues.Add("dollars");
            else
                stringvalues.Add("dollar");

            if (displayCents)
            {
                stringvalues.Add("and");
                stringvalues.AddRange(BlockToStrings(cents.Value, includeZero: false));

                if (usePluralForCents)
                    stringvalues.Add("cents");
                else
                    stringvalues.Add("cent");
            }

            return string.Join(" ", stringvalues.ToArray());
        }

        public int GetCents(decimal input) => (int)((input - (long) input) * 100);

        private bool TrySplitAtPowerOf10(long sourceValue, int powersOf10, out long left, out int right)
        {
            left = sourceValue;
            right = 0;
            int blockSize = (int)Math.Pow(10, powersOf10);

            if (sourceValue < blockSize)
                return false;

            var block = sourceValue / blockSize * blockSize;

            left = block / blockSize;
            right = (int)(sourceValue - block);
            return true;
        }
        private bool HandleIrregular10To19(int source, out string name)
        {
            name = "";
            if(source > 9 && source < 20)
            {
                name = irregulars[source];
                return true;
            }
            else
                return false;
        }
        private List<string> BlockToStrings(int block, bool includeZero)
        {

            List<string> values = new List<string>();

            if (block == 0 && !includeZero)
                return values;

            bool blockHasNumber = false;
            if (TrySplitAtPowerOf10(block, 2, out long left, out int right))
            {
                values.Add(singleDigitNames[(int)left] + " hundred");
                blockHasNumber = true;
            }
            else
                right = block;


            if (HandleIrregular10To19(right, out string irregularNumber))
            {
                values.Add(irregularNumber);
                blockHasNumber = true;
            }
            else
            {
                string tensResult = "";
                if (TrySplitAtPowerOf10(right, 1, out left, out right))
                {
                    tensResult += doubleDigitNames[(int)left];
                    blockHasNumber = true;
                }
                else if (right != 0)
                    right = block;


                if (right == 0 && block > 0 && block < 10)
                    tensResult += singleDigitNames[block];
                else if (TrySplitAtPowerOf10(right, 0, out left, out right))
                    tensResult += "-" + singleDigitNames[(int)left];
                else if (right == 0 && blockHasNumber == false)
                    tensResult += singleDigitNames[0];


                if(!string.IsNullOrEmpty(tensResult))
                    values.Add(tensResult);
            }
            return values;
        }
    }
}
