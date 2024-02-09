import paho.mqtt.client as mqtt
import json
import time


#mosquitto_sub -t senzorski_podaci -h localhost
broker_address = "mqtt-broker_edgex"
port = 1883
publish_topic = "Comand_edgex"
subscribe_topic = "senzorski_podaci_edgex"
print("----------------------------")

def send_sensor_data(client, sensor_data):
    for data in sensor_data:
        client.publish(publish_topic, data)
        print(f"Poslani senzorski podatak: {data}")
        time.sleep(5)


def on_connect(client, userdata, flags, rc):
    print(f"Povezan s MQTT brokerom s rezultatom: {rc}")
    client.subscribe(subscribe_topic)
    print(f"Subscribed to topic: {subscribe_topic}")

def on_message(client, userdata, msg):
    payload = msg.payload.decode("utf-8")
    print(f"Received message on topic {msg.topic}: {payload}")
    temp_value = 0
    message_dict = json.loads(payload)
    temperature_value_str = message_dict['readings'][0]['value']
    temp_value = float(temperature_value_str.replace('\\', '').replace('"',''))
    if temp_value > 21:
        print("Restart Sensor")
        client.publish("Comand_edgex", "Restart Sensor") #ovo treba da bude simulirana poruka edgeX comanda za sada je samo drugi topic

client = mqtt.Client("Monitoring")
client.on_connect = on_connect
client.on_message = on_message

print(broker_address)
client.username_pw_set("user2", "123")
client.connect(broker_address, port, 60)



client.loop_forever()

# while True:
#     time.sleep(1)