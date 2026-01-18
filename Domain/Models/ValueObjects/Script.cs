using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ValueObjects
{
    
        public sealed class Script
        {
            public string Text { get; }

            private Script(string text)
            {
                Text = text;
            }

            public static Script Create(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("Script cannot be empty");

                if (text.Length < 30)
                    throw new ArgumentException("Script is too short to generate a video");

                if (text.Length > 5000)
                    throw new ArgumentException("Script is too long");

                return new Script(text.Trim());
            }

            public int WordCount =>
                Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

            public override string ToString() => Text;

            public override bool Equals(object obj)
            {
                if (obj is not Script other) return false;
                return Text.Equals(other.Text, StringComparison.OrdinalIgnoreCase);
            }

            public override int GetHashCode() =>
                Text.ToLowerInvariant().GetHashCode();
        }
    }


