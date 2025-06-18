namespace Suitability.Consumer.Domain.Entities.Messages
{
    public class MessageLess
    {
        public string? AccountNumber { get; set; }
        public string? PositionDate { get; set; }
        public string? QueueId { get; set; }
        public string? CreateAd { get; set; }
        public Position? Position { get; set; }
        public string? Transaction { get; set; }
        public string? Cash { get; set; }


        public static MessageLess? TryBuildLessMessage(MessageLess? lessMessage)
        {
            if (lessMessage == null)
                return null;

            try
            {
                return new MessageLess
                {
                    AccountNumber = lessMessage.AccountNumber,
                    PositionDate = lessMessage.PositionDate,
                    QueueId = lessMessage.QueueId,
                    CreateAd = lessMessage.CreateAd,
                    Position = lessMessage.Position,
                    Transaction = lessMessage.Transaction,
                    Cash = lessMessage.Cash
                };
            }

            catch (Exception ex) 
            {
                return null;
            }
        }

        public static MessageLess? TryBuildLessMessage(Position? position)
        {
            if (position == null)
                return null;

            try
            {
                return new MessageLess
                {
                    AccountNumber = position.AccountNumber,
                    PositionDate = position.Date.ToString(),
                    QueueId = Guid.NewGuid().ToString(),
                    CreateAd = position.Date.ToString(),
                    Position = position,
                    Transaction = null,
                    Cash = null
                };
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
