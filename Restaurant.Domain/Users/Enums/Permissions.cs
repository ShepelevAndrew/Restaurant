namespace Restaurant.Domain.Users.Enums;

public enum Permissions
{
    Read = 1,
    Create = 2,
    Update = 3,
    Delete = 4,
    GetBoughtOrders = 5,
    BuyOrder = 6,
    GetVerifiedOrders = 7,
    VerifyOrder = 8,
    ShippedOrder = 9,
    GetPaidOrders = 10,
    PaidOrder = 11,
    GetCancelledOrders = 12,
    CancelledOrder = 13,
    Cart = 14,
    ReadUsers = 15,
    UpdateMyUser = 16,
    UpdateUsers = 17,
    DeleteUsers = 18,
    CreateRole = 19,
}