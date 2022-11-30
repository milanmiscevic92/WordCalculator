using Application.Common.Interfaces.Messaging;

namespace Application.Texts.GetTextFromFile
{
    public sealed record GetTextFromFileQuery(string path) : IQuery<TextResponse>;
}
