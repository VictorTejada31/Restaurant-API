namespace Restaurant.Core.Application.Dtos.Account
{
    public abstract class Commons
    {
        public virtual bool HasError { get; set; }
        public virtual string Error { get; set; }
    }
}
