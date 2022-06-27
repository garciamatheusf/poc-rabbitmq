namespace producer_queue.Services {
    public class QueueService {
        static int priorityLastNumber = 0;
        static int nonPriorityLastNumber = 5000;

        public static int GetNextTicket(QueueObject queueObject) {
            return queueObject.Priority ? ++priorityLastNumber : ++nonPriorityLastNumber;
        }
    }
}
