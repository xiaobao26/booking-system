namespace booking_system.Contracts;

public class NotFoundException: Exception
{
   public NotFoundException(string message) : base(message)
   {
      
   }
}