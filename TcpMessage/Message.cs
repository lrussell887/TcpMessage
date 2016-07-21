using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpMessage
{
    public class Message : List<object>
    {
        public string Type { get; set; }

        public Message(string type, params object[] args)
        {
            this.Type = type;
            this.AddRange(args);
        }

        internal Message(MessageItem[] items)
        {
            this.Type = (string)items[0].ValueObject;
            this.AddRange(items.Skip(1).Select(i => i.ValueObject));
        }

        internal MessageItem[] ToMessageItems()
        {
            return new[] { MessageItem.CreateNew(this.Type) }
                .Concat(this.Select(i => MessageItem.CreateNew(i)))
                .ToArray();
        }

        public T Get<T>(int i)
        {
            return (T)this[i];
        }

        public override string ToString()
        {
            var sb = new StringBuilder()
            .Append("Type = ").Append(this.Type)
            .Append(", Count = ").Append(this.Count);

            for (var i = 0; i < this.Count; i++)
            {
                sb.AppendLine(",")
                    .Append("[").Append(i)
                    .Append("] = ").Append(this[i]);
            }

            return sb.ToString();
        }
    }
}
