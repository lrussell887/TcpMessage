using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace TcpMessage
{
    [ProtoContract]
    internal abstract partial class MessageItem
    {
        public abstract object ValueObject { get; set; }

        public static MessageItem CreateNew(object value)
        {
            var type = value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return new MessageItem<bool>((bool)value);
                case TypeCode.Byte:
                    return new MessageItem<byte>((byte)value);
                case TypeCode.Char:
                    return new MessageItem<char>((char)value);
                case TypeCode.DateTime:
                    return new MessageItem<DateTime>((DateTime)value);
                case TypeCode.Decimal:
                    return new MessageItem<decimal>((decimal)value);
                case TypeCode.Double:
                    return new MessageItem<double>((double)value);
                case TypeCode.Int16:
                    return new MessageItem<short>((short)value);
                case TypeCode.Int32:
                    return new MessageItem<int>((int)value);
                case TypeCode.Int64:
                    return new MessageItem<long>((long)value);
                case TypeCode.SByte:
                    return new MessageItem<sbyte>((sbyte)value);
                case TypeCode.Single:
                    return new MessageItem<float>((float)value);
                case TypeCode.String:
                    return new MessageItem<string>((string)value);
                case TypeCode.UInt16:
                    return new MessageItem<ushort>((ushort)value);
                case TypeCode.UInt32:
                    return new MessageItem<uint>((uint)value);
                case TypeCode.UInt64:
                    return new MessageItem<ulong>((ulong)value);
                case TypeCode.Empty:
                    throw new ArgumentNullException("value");

                default:
                    var param = (MessageItem)Activator.CreateInstance(
                        typeof(MessageItem<>).MakeGenericType(type));
                    param.ValueObject = value;
                    return param;
            }
        }
    }

    [ProtoContract]
    internal class MessageItem<T> : MessageItem
    {
        public MessageItem()
        {
        }

        public MessageItem(T value)
        {
            this.Value = value;
        }

        [ProtoMember(1)]
        public T Value { get; set; }

        public override object ValueObject
        {
            get
            {
                return this.Value;
            }
            set
            {
                this.Value = (T)value;
            }
        }
    }
}
