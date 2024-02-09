import paho.mqtt.client as mqtt
import time


broker_address = "mqtt-broker_edgex"
port = 1883
topic = "senzorski_podaci_edgex"
print("----------------------------")
def read_sensor_data_from_file(file_path):
    with open(file_path, "r") as file:
        lines = file.readlines()[1:]
    return lines

def send_sensor_data(client, sensor_data):
    for data in sensor_data:
        client.publish(topic, data)
        print(f"Poslani senzorski podatak: {data}")
        time.sleep(5)


def on_connect(client, userdata, flags, rc):
    print(f"Povezan s MQTT brokerom s rezultatom: {rc}")

client = mqtt.Client("SensorDummy")
client.on_connect = on_connect

print(broker_address)
client.username_pw_set("user1", "123")
client.connect(broker_address, port, 60)

file_path = "iot_telemetry_data.csv"
sensor_data = read_sensor_data_from_file(file_path)

send_sensor_data(client, sensor_data)

client.loop_forever()

