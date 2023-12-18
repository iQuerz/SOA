
import requests
import json
import random
import time


edgexip = '127.0.0.1'
humval = 40
tempval = 23
file_path = "iot_telemetry_data.csv"

def generateSensorData(humval, tempval):

    humval = random.randint(humval-5,humval+5)
    tempval = random.randint(tempval-1, tempval+1)

    print("Sending values: Humidity %s, Temperature %sC" % (humval, tempval))

    return (humval, tempval)

def read_sensor_data_from_file(file_path):
    with open(file_path, "r") as file:
        lines = file.readlines()[1:]
    return lines

if __name__ == "__main__":

    sensorTypes = ["co", "humidity", "light", "lpg", "motion", "smoke", "temp"]


    while True:
        # Read sensor data from the file
        sensor_data_lines = read_sensor_data_from_file(file_path)

        for line in sensor_data_lines:
            # Extract values from the line
            timestamp, device, co, humidity, light, lpg, motion, smoke, temp = line.strip().split(',')

            print(f"Sending values: CO {co}, Humidity {humidity}%, Light {light}, LPG {lpg}, Motion {motion}, Smoke {smoke}, Temperature {temp}C")

            # Send all data types
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/timestamp'
            payload = timestamp
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/device'
            payload = device
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/co'
            payload = float(co.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/humidity'
            payload = float(humidity.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/light'
            payload = bool(light.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/lpg'
            payload = float(lpg.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/motion'
            payload = bool(motion.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/smoke'
            payload = float(smoke.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)
            url = f'http://{edgexip}:49986/api/v1/resource/SensorValueCluster2/temp'
            payload = float(temp.strip('"'))
            headers = {'content-type': 'application/json'}
            response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)
            print(response)

            time.sleep(5)

            #old code inspiration
            # url = 'http://%s:49986/api/v1/resource/Temp_and_Humidity_sensor_cluster_01/temperature' % edgexip
            # payload = tempval
            # headers = {'content-type': 'application/json'}
            # response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

            # url = 'http://%s:49986/api/v1/resource/Temp_and_Humidity_sensor_cluster_01/humidity' % edgexip
            # payload = humval
            # headers = {'content-type': 'application/json'}
            # response = requests.post(url, data=json.dumps(payload), headers=headers, verify=False)

        time.sleep(5)