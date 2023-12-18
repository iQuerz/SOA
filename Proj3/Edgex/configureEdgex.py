import requests
import json

# Function to send an HTTP POST request
def send_post_request(url, body):
    headers = {"Content-Type": "application/json"}
    response = requests.post(url, data=json.dumps(body), headers=headers)
    return response

# Task 1: Create ValueDescriptors
value_descriptor_url = "http://localhost:48080/api/v1/valuedescriptor"

# Define bodies for each ValueDescriptor
value_descriptor_bodies = [
    {
        "name": "timestamp",
        "description": "Time of taking mesurment",
        "type": "String",
        "uomLabel": "ppm",
        "defaultValue": "",
    },
    {
        "name": "device",
        "description": "Device mesurment is taken on",
        "type": "String",
        "uomLabel": "ppm",
        "defaultValue": "",
    },
    {
        "name": "co",
        "description": "Carbon Monoxide level",
        "type": "float64",
        "uomLabel": "ppm",
        "defaultValue": 0.0,
    },
    {
        "name": "humidity",
        "description": "Humidity level",
        "type": "float64",
        "uomLabel": "percent",
        "defaultValue": 0.0,
    },
    {
        "name": "light",
        "description": "light Ppresence",
        "type": "bool",
        "uomLabel": "lux",
        "defaultValue": False,
    },
    {
        "name": "lpg",
        "description": "Liquefied Petroleum Gas level",
        "type": "float64",
        "uomLabel": "ppm",
        "defaultValue": 0.0,
    },
    {
        "name": "motion",
        "description": "Motion detection",
        "type": "bool",
        "defaultValue": False
    },
    {
        "name": "smoke",
        "description": "Smoke level",
        "type": "float64",
        "uomLabel": "ppm",
        "defaultValue": 0.0,
    },
    {
        "name": "temp",
        "description": "Temperature",
        "type": "float64",
        "uomLabel": "celsius",
        "defaultValue": 0.0,
    }
]

# Send POST requests for each ValueDescriptor
for body in value_descriptor_bodies:
    response = send_post_request(value_descriptor_url, body)
    print(f"ValueDescriptor created. Status code: {response.status_code}")

# Task 2: Create Device Profile
device_profile_url = "http://localhost:48081/api/v1/deviceprofile/uploadfile"
device_profile_file_path = "device-profile.yaml"

files = {"file": ("device-profile.yaml", open(device_profile_file_path, "rb"))}
response = requests.post(device_profile_url, files=files)
print(f"Device Profile created. Status code: {response.status_code}")

# Task 3: Create Device
device_url = "http://localhost:48081/api/v1/device"

device_body = {
    "name": "SensorValueCluster2",
    "description": "Raspberry Pi sensor cluster",
    "adminState": "unlocked",
    "operatingState": "enabled",
    "protocols": {
        "HTTP": {
            "host": "localhost",
            "unitID": "1"
        }
    },
    "labels": ["Temperature sensor", "DHT11"],
    "location": "Nis",
    "service": {"name": "edgex-device-rest"},
    "profile": {"name": "SensorCluster2"}
}

response = send_post_request(device_url, device_body)
print(f"Device created. Status code: {response.status_code}")
