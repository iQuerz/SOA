import paho.mqtt.client as mqtt
import time


#mosquitto_sub -t senzorski_podaci -h localhost
broker_address = "mqtt-broker"
port = 1883
topic = "senzorski_podaci"
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
client.username_pw_set("sensorDummyUser", "123")
client.connect(broker_address, port, 60)

file_path = "iot_telemetry_data.csv"
sensor_data = read_sensor_data_from_file(file_path)

send_sensor_data(client, sensor_data)

client.loop_forever()

#docker run -d --net=host 75ea6f770dd7
#e9ff006adf45
#9cba94ff37a4
#eca0daab4e8b
