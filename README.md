#This sample contains a Multi consumer with masstransit

##two samples
- sample msmq
- sample rabittMQ



##using rabbitmq

In this sample we have 3 consumers
- OldConsumer 0: Old Subscriber subsribes on articleupdate message: The article message only contains the article id
- Consumer 1: Subscribes on articleupdate message: The article message contains a article id and an CreationDate
- Consumer 2: Subscribes on articleupdate message: The article message contains a article id and an CreationDate
- Publisher: can publish messages
- Publisher2: can publish message
 
##Type of messages
- 1 a message that triggers an exception on consumer1
- 2 a message that triggers an exception on consumer2
- 3 a message that triggers an exception on both consumer1 and consumer2
- any other key trigger no exceptions

 
Each consumer has his own queue. see exchange section at rabitmq tutorial http://www.rabbitmq.com/tutorials/tutorial-three-python.html
 

###setup 
- install rabbitmq


###Warning
__When using rabbitmq and masstransit the error queues where not created so I created them myself.__





