using API.Contracts.Requests;
using AutoMapper;
using Core.Contracts.Dtos;

namespace API.Mapping.Resolvers
{
    public class ByteArrayResolver : IValueResolver<ImageFormRequest, ImageDto, byte[]>
    {
        public byte[] Resolve(ImageFormRequest source, ImageDto destination, byte[] destMember, ResolutionContext context)
        {
            if (source?.Image == null || source.Image.Length == 0)
                return null;

            using var memoryStream = new MemoryStream();
            source.Image.CopyTo(memoryStream); // Синхронное копирование
            return memoryStream.ToArray();
        }
    }
}