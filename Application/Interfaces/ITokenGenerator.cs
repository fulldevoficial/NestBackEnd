namespace Application.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(string email, Guid userId);
    }
}
