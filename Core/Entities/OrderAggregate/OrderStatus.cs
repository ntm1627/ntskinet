using System.Runtime.Serialization;

namespace Core.Entities.OrderAggregate
{
    //simple status,flag, when we receive order from  the client
    public enum OrderStatus
    {
        [EnumMember(Value="Pending")]
        Pending,
         [EnumMember(Value="PaymentReceived")]
        PaymentReceived,
         [EnumMember(Value="PaymentFailed")]
        PaymentFailed
        
    }
}