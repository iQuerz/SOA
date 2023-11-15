from flask import Flask, jsonify, request
from data import GetAll,Get,Create,Update,Delete
#from flask_swagger_ui import get_swaggerui_blueprint
#from flask_restplus import Api, Resource, fields
from flasgger import Swagger

app = Flask(__name__)
swagger = Swagger(app)


@app.route('/iots', methods=['GET'])
def get_iots():
    """
    Get all IoT readings
    ---
    responses:
      200:
        description: Return all IoT readings
    """
    return GetAll()

@app.route('/iot/<string:ts>/<string:device>', methods=['GET'])
def get_reading(ts, device):
    """
    Get a specific IoT reading by ID
    ---
    parameters:
      - in: path
        name: ts
        type: string
        required: true
        description: Timestamp
      - in: path
        name: device
        type: string
        required: true
        description: Device ID
    responses:
      200:
        description: Return the specified IoT reading
      404:
        description: IoT reading not found
    """
    # Use ts and device in your function logic
    return Get(ts, device)
        

#Route Crate Readings
@app.route('/iots', methods=['POST'])
def create_reading():
    """
    Create a new IoT reading
    ---
    parameters:
      - in: body
        name: body
        required: true
        schema:
          type: object
          properties:
            ts:
              type: string
            device:
              type: string
            co:
              type: number
            humidity:
              type: number
            light:
              type: boolean
            lpg:
              type: number
            motion:
              type: boolean
            smoke:
              type: number
            temp:
              type: number
        example:
          ts: "1.5945120943859746E9"
          device: "b8:27:eb:bf:9d:51"
          co: 0.00495593864839124
          humidity: 51.0
          light: true
          lpg: 0.00765082227055719
          motion: true
          smoke: 0.0204112701224129
          temp: 25.0
    responses:
      201:
        description: Successfully created a new IoT reading
      400:
        description: Bad Request - Invalid input data
    """
    new_reading = {
        'ts': request.json['ts'],
        'device': request.json['device'],
        'co': request.json['co'],
        'humidity': request.json['humidity'],
        'light': request.json['light'],
        'lpg': request.json['lpg'],
        'motion': request.json['motion'],
        'smoke': request.json['smoke'],
        'temp': request.json['temp']
    }
    return Create(new_reading)

#Route Update a Readings
@app.route('/iot/<string:ts>/<string:device>', methods=['PUT'])
def update_book(ts, device):
    """
    Update a specific IoT reading by ID
    ---
    parameters:
      - in: path
        name: ts
        type: string
        required: true
        description: Timestamp
      - in: path
        name: device
        type: string
        required: true
        description: Device ID
      - in: body
        name: body
        required: true
        schema:
          type: object
          properties:
            co:
              type: number
            humidity:
              type: number
            light:
              type: boolean
            lpg:
              type: number
            motion:
              type: boolean
            smoke:
              type: number
            temp:
              type: number
        example:
          co: 0.00495593864839124
          humidity: 51.0
          light: true
          lpg: 0.00765082227055719
          motion: true
          smoke: 0.0204112701224129
          temp: 25.0
    responses:
      200:
        description: Successfully updated the IoT reading
      404:
        description: IoT reading not found
      400:
        description: Bad Request - Invalid input data
    """
    reading = Get(ts, device)

    reading[0]=request.json['co']
    reading[1]=request.json['humidity']
    reading[2]=request.json['light']
    reading[3]=request.json['lpg']
    reading[4]=request.json['motion']
    reading[5]=request.json['smoke']
    reading[6]=request.json['temp']
    reading[7]=ts
    reading[8]=device
    return Update(reading) 
        
#Route Delete a Readings
@app.route('/iot/<string:ts>/<string:device>', methods=['DELETE'])
def delete_book(iot_id):
    """
    Delete a specific IoT reading by ID
    ---
    parameters:
      - in: path
        name: ts
        type: string
        required: true
        description: Timestamp
      - in: path
        name: device
        type: string
        required: true
        description: Device ID
    responses:
      204:
        description: Successfully deleted the IoT reading
      404:
        description: IoT reading not found
    """
    return Delete(iot_id)

#Runing
if __name__ == '__main__':
    app.run(debug=True)