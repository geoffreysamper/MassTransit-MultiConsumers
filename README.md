#This sample contains a Multi consumer with masstransit

##two samples
- sample msmq
- sample rabittMQ



##using rabbitmq

In this sample we have 3 consumers
- OldConsumer 0: Old Subscriber subsribes on articleupdate message: The article message only contains the article id
- Consumer 1: Subscribes on articleupdate message: The article message contains a article id and an CreationDate
- Consumer 2: Subscribes on articleupdate message: The article message contains a article id and an CreationDate
 
 
Each consumer has his own queue. see exchange section at rabitmq tutorial http://www.rabbitmq.com/tutorials/tutorial-three-python.html
 

###setup 
- install rabbitmq




