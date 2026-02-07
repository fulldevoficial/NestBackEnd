namespace Application.Ports.Output
{
    public interface ITokenGenerator
    {
        string GenerateToken(string email, Guid userId);
    }
}
