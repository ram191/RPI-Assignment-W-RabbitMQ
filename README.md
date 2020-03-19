# Required docker images for this project

- docker push ram191/usersservice:latest
- docker push ram191/usersservice:latest

# Description

This is a RESTAPI project on testing how message broker service works as a bridge connection between each services.
The project is separated into two services namely "notificationservice" and "userservice". Both services are connected to each other through the RabbitMQ services contained in both service. For now, the usersservice holds the producer role and the notificationservice functions as the consumer.

## How does RabbitMQ work here?

In the previous version of this project, the post method of the userservice RESTAPI calls an HTTP method of Post, taking its data object of the user, serialize it to JSON, and send the request to notificationservice through the service's URL. The notificationservice's post method will accept the request and use its RESTAPI Post method to create a new notification log for the user and send an email to them. This concept works as long as both services are up and running at the same time. But a problem emerges when notificationservice is not running when usersservice sends a request data.

RabbitMQ service plays a role to solve the emerging problem by acting as a mediator between both services. Usersservice no longer sends a request directly to notificationservice, but it now sends a serialized message -- in this case a JSON, to the what's so called the exchange -- since this uses a pub/sub method to consume and produce the message. One important thing to note here is that RabbitMQ will only be able to send message in a form of bytes, so any kind of messages we're about to send must be serialized first somehow.

Usersservice does not care whether the destination service is running or not, it only sends the messages to the broker(exchange) and continues the job. The exchange will then keep the messages until it is being pulled by the consumer/subscriber. The messages kept by the broker can be seen in the RabbitMQ Management Client (in this case it's on http://localhost:8080). It will hold the status of 'ready' as long as the messages are not being pulled by the consumer/subscriber. At this point, the messages are no longer dependant on both services so it is safe now to stop both services.

Consuming the message is as easy as producing the message. We basically just have to tell the consumer which exchange are we going to consumer from, deserialize the message back to a string, and do anything we want with it. In this project, the message is converted into a HttpContent so it can be used as a body in a Http Post method.

## Where's the authentication?

The authentication for accessing the notificationservice is set on the Kong gateway service. The kong configuration file is available on this repository.
