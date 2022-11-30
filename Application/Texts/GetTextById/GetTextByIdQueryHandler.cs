using Application.Common.Interfaces.Messaging;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Texts.GetTextById
{
    internal sealed class GetTextByIdQueryHandler : IQueryHandler<GetTextByIdQuery, TextResponse>
    {
        private readonly ITextRepository _textRepository;

        public GetTextByIdQueryHandler(ITextRepository textRepository)
        {
            _textRepository = textRepository;
        }

        public async Task<Result<TextResponse>> Handle(GetTextByIdQuery request, CancellationToken cancellationToken)
        {
            var text = await _textRepository.GetTextByIdAsync(request.textId, cancellationToken);

            if (text is null)
            {
                return Result.Failure<TextResponse>(new Error("Text.NotFound", $"The text with Id {request.textId} was not found."));
            }

            var response = new TextResponse(text.Content);

            return response;
        }
    }
}
