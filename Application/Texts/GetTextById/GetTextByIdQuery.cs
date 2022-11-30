using Application.Common.Interfaces.Messaging;

namespace Application.Texts.GetTextById
{
    public sealed record GetTextByIdQuery(int textId) : IQuery<TextResponse>;
}
