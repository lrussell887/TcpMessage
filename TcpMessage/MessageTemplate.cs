using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace TcpMessage
{
    [
    ProtoInclude(1, typeof(MessageItem<string>)),
    ProtoInclude(2, typeof(MessageItem<int>)),
    ProtoInclude(3, typeof(MessageItem<bool>)),
    ProtoInclude(4, typeof(MessageItem<double>)),
    ProtoInclude(5, typeof(MessageItem<uint>)),
    ProtoInclude(6, typeof(MessageItem<short>)),
    ProtoInclude(7, typeof(MessageItem<ushort>)),
    ProtoInclude(8, typeof(MessageItem<long>)),
    ProtoInclude(9, typeof(MessageItem<ulong>)),
    ProtoInclude(10, typeof(MessageItem<byte>)),
    ProtoInclude(11, typeof(MessageItem<sbyte>)),
    ProtoInclude(12, typeof(MessageItem<char>)),
    ProtoInclude(13, typeof(MessageItem<float>)),
    ProtoInclude(14, typeof(MessageItem<decimal>)),
    ProtoInclude(15, typeof(MessageItem<DateTime>)),

    ProtoInclude(16, typeof(MessageItem<string[]>)),
    ProtoInclude(17, typeof(MessageItem<int[]>)),
    ProtoInclude(18, typeof(MessageItem<bool[]>)),
    ProtoInclude(19, typeof(MessageItem<double[]>)),
    ProtoInclude(20, typeof(MessageItem<uint[]>)),
    ProtoInclude(21, typeof(MessageItem<short[]>)),
    ProtoInclude(22, typeof(MessageItem<ushort[]>)),
    ProtoInclude(23, typeof(MessageItem<long[]>)),
    ProtoInclude(24, typeof(MessageItem<ulong[]>)),
    ProtoInclude(25, typeof(MessageItem<byte[]>)),
    ProtoInclude(26, typeof(MessageItem<sbyte[]>)),
    ProtoInclude(27, typeof(MessageItem<char[]>)),
    ProtoInclude(28, typeof(MessageItem<float[]>)),
    ProtoInclude(29, typeof(MessageItem<decimal[]>)),
    ProtoInclude(30, typeof(MessageItem<DateTime[]>)),
    ]

    partial class MessageItem
    {
    }
}
