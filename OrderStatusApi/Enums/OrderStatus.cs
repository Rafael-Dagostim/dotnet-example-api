namespace OrderStatusApi.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// Status de pedido fechado
        /// </summary>
        Closed, 
        /// <summary>
        /// Status de pedido aberto
        /// </summary>
        Open,
        /// <summary>
        /// Status de pedido bloqueado
        /// </summary>
        Blocked,
    }
}