using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Services;
using Domain.Shared;

namespace Application.Texts.GetTextFromFile
{
    internal sealed class GetTextFromFileQueryHandler : IQueryHandler<GetTextFromFileQuery, TextResponse>
    {
        private readonly ITextFileService _textFileService;

        public GetTextFromFileQueryHandler(ITextFileService textFileService)
        {
            _textFileService = textFileService;
        }

        public async Task<Result<TextResponse>> Handle(GetTextFromFileQuery request, CancellationToken cancellationToken)
        {
            var text = await _textFileService.GetTextFromFileByPathAsync(request.path, cancellationToken);

            if (text is null)
            {
                return Result.Failure<TextResponse>(new Error("Text.NotFound", $"The text at path {request.path} was not found."));
            }

            var response = new TextResponse(text);

            return response;
        }
    }
}
