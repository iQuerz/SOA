import paho.mqtt.client as mqtt
import time


#mosquitto_sub -t senzorski_podaci -h localhost
broker_address = "mqtt-broker"
port = 1883
publish_topic = "Comand_edgex"
subscribe_topic = "senzorski_podaci_edgex"
print("----------------------------")

def send_sensor_data(client, sensor_data):
    for data in sensor_data:
        client.publish(topic, data)
        print(f"Poslani senzorski podatak: {data}")
        time.sleep(5)


def on_connect(client, userdata, flags, rc):
    print(f"Povezan s MQTT brokerom s rezultatom: {rc}")
    client.subscribe(subscribe_topic)
    print(f"Subscribed to topic: {subscribe_topic}")

def on_message(client, userdata, msg):
    payload = msg.payload.decode("utf-8")
    print(f"Received message on topic {msg.topic}: {payload}")


    if payload.temp < 21: #ovo ne radi ali je placeholder za kasnije
        client.publish("Comand_edgex", "Restart Sensor")

client = mqtt.Client("Monitoring")
client.on_connect = on_connect
client.on_message = on_message

print(broker_address)
client.username_pw_set("user1", "123")
client.connect(broker_address, port, 60)




client.loop_forever()

